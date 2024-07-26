using Common.Pool;
using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerGun : MonoBehaviour
    {
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _barrel;

        private GameSettings _gameSettings;
        private float _nextShootTime;
        private MonoBehaviourPool<Bullet> _bulletPool;

        public void Initialize(GameSettings gameSettings, Bullet bullet)
        {
            _gameSettings = gameSettings;
            _bulletPool ??= new MonoBehaviourPool<Bullet>(bullet, bullet.transform.parent);
        }

        public void ShootAt(Vector3 targetPosition)
        {
            if (Time.time >= _nextShootTime)
            {
                _nextShootTime = Time.time + _gameSettings.PlayerShootingRate;
                _animator.SetTrigger(Shoot);
                FireBullet(targetPosition);
            }
        }

        private void FireBullet(Vector3 targetPosition)
        {
            var direction = (targetPosition - _barrel.position).normalized;
            var bullet = _bulletPool.Take();
            bullet.transform.position = _barrel.position; 
            bullet.Initialize(direction, _gameSettings.PlayerDamage, _gameSettings.BulletSpeed);
            bullet.OnBulletDisposed += () => _bulletPool.Release(bullet);
        }
    }
}