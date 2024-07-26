using System;
using Events;
using Settings;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Action OnDisposed;
        
        public EnemyHealthController EnemyHealth;
        
        private AsyncMessageBus _messageBus;
        private GameSettings _gameSettings;

        [Inject]
        public void Construct(AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        private void OnDisable()
        {
            OnDisposed = null;
            EnemyHealth.OnDied -= Die;
        }

        public void Initialize(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            EnemyHealth.Initialize(gameSettings.EnemyHealth);
            EnemyHealth.OnDied += Die;
        }
        
        void Update()
        {
            Move();
        }

        private void Move()
        {
            var moveSpeed = Random.Range(_gameSettings.MinEnemySpeed, _gameSettings.MaxEnemySpeed);
            transform.position += Vector3.down * (moveSpeed * Time.deltaTime);
        }

        public void Die()
        {
            _messageBus.Publish(new EnemyDiedEvent(this));
            OnDisposed?.Invoke();
        }
    }
}