using System;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        private const float BULLET_LIFE_TIME = 5f;
        private float _bulletSpeed;

        private int _damage;
        private Vector3 _direction;
        private float _timeSinceSpawn;

        public Action OnBulletDisposed;

        private void Update()
        {
            transform.position += _direction * _bulletSpeed * Time.deltaTime;
            _timeSinceSpawn += Time.deltaTime;

            if (_timeSinceSpawn >= BULLET_LIFE_TIME) BulletDispose();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy")) return;
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy == null) return;
            enemy.TakeDamage(_damage);
            BulletDispose();
        }

        public void Initialize(Vector3 direction, int damage, float bulletSpeed)
        {
            _bulletSpeed = bulletSpeed;
            _direction = direction;
            _damage = damage;
            _timeSinceSpawn = 0f;
        }

        private void BulletDispose()
        {
            OnBulletDisposed?.Invoke();
        }
    }
}