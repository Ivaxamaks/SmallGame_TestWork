using System;
using System.Collections.Generic;
using Common.Extentions;
using Common.Pool;
using Events;
using UniTaskPubSub;
using Object = UnityEngine.Object;

namespace UI.HealthBar
{
    public class HealthBarManager : IDisposable
    {
        private readonly AsyncMessageBus _subscriber;
        private readonly MonoBehaviourPool<HealthBar> _healthBarPool;
        
        private readonly Dictionary<Enemy.Enemy, HealthBar> _healthBars = new();

        private CompositeDisposable _subscriptions;

        public HealthBarManager(HealthBar healthBarPrefab, AsyncMessageBus subscriber)
        {
            _subscriber = subscriber;
            _healthBarPool = new MonoBehaviourPool<HealthBar>(healthBarPrefab, healthBarPrefab.transform.parent);
        }

        public void Initialize()
        {
            _subscriptions?.Dispose();

            _subscriptions = new CompositeDisposable
            {
                _subscriber.Subscribe<EnemySpawnEvent>(eventData => SpawnHealthBar(eventData.Enemy)),
                _subscriber.Subscribe<EnemyDiedEvent>(eventData => DestroyHealthBar(eventData.Enemy)),
            };
        }

        private void SpawnHealthBar(Enemy.Enemy enemy)
        {
            var healthController = enemy.EnemyHealth;

            var healthBar = _healthBarPool.Take();
            healthBar.Initialize(healthController);

            _healthBars[enemy] = healthBar;
        }

        private void DestroyHealthBar(Enemy.Enemy enemy)
        {
            if (!_healthBars.ContainsKey(enemy))
            {
                return;
            }

            _healthBars.Remove(enemy, out var healthBar);
            Object.Destroy(healthBar.gameObject);
        }

        public void Dispose()
        {
            _healthBarPool.ReleaseAll();
            _subscriptions?.Dispose();
        }
    }
}