using Common.Pool;
using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerGun : MonoBehaviour
    {
        private GameSettings _gameSettings;
        private float _nextShootTime;
        private MonoBehaviourPool<Bullet> _bulletPool;
        
        public void Initialize(
            GameSettings gameSettings,
            Bullet bullet)
        {
            _gameSettings = gameSettings;
            _bulletPool ??= new MonoBehaviourPool<Bullet>(bullet, transform);
        }
        
        public void ShootAt(Vector3 targetPosition)
        {
            if (Time.time >= _nextShootTime)
            {
                _nextShootTime = Time.time + _gameSettings.PlayerShootingRate;
                FireBullet(targetPosition);
            }
        }

        private void FireBullet(Vector3 targetPosition)
        {
            var direction = (targetPosition - transform.position).normalized;
            var bullet = _bulletPool.Take();
            bullet.transform.position = transform.position;
            bullet.Initialize(direction, _gameSettings.PlayerDamage, _gameSettings.BulletSpeed);
            bullet.OnBulletDisposed += () => _bulletPool.Release(bullet);
        }
    }
}