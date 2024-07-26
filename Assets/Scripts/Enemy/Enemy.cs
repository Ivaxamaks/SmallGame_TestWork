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
        
        private AsyncMessageBus _messageBus;
        private GameSettings _gameSettings;
        
        private int _health;

        [Inject]
        public void Construct(AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        private void OnDisable()
        {
            OnDisposed = null;
        }

        public void Initialize(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _health = gameSettings.EnemyHealth;
        }
        
        void Update()
        {
            Move();
        }

        private void Move()
        {
            var moveSpeed = Random.Range(_gameSettings.MinEnemySpeed, _gameSettings.MaxEnemySpeed);
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _messageBus.Publish(new EnemyDiedEvent());
            OnDisposed?.Invoke();
        }
    }
}