using Events;
using Services;
using Settings;
using UniTaskPubSub;
using UnityEngine;
using VContainer;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerGun _playerGun;
        [SerializeField] private PlayerEnemyDetector _enemyDetector;

        private IAsyncPublisher _publisher;
        private GameSettings _gameSettings;
        private InputHandler _inputHandler;
        private Bullet _bullet;
        private Boundary _boundary;
        
        private int _health;
        private bool _isNeedDetectEnemy;

        [Inject]
        public void Construct(
            AsyncMessageBus messageBus,
            GameSettings gameSettings,
            InputHandler inputHandler,
            Bullet bullet,
            Boundary boundary)
        {
            _publisher = messageBus;
            _gameSettings = gameSettings;
            _inputHandler = inputHandler;
            _bullet = bullet;
            _boundary = boundary;
        }

        public void Initialize()
        {
            _health = _gameSettings.PlayerInitialHealth;
            _boundary.OnEnemyReachBoundary += EnemyReachBoundaryEventHandler;
            _playerMovement.Initialize(_inputHandler, _boundary.BoundaryPointsProvider, _gameSettings.PlayerMoveSpeed);
            _enemyDetector.Initialize(_gameSettings);
            _playerGun.Initialize(_gameSettings, _bullet);
            _isNeedDetectEnemy = true;
        }

        public void Dispose()
        {
            _isNeedDetectEnemy = false;
            _boundary.OnEnemyReachBoundary -= EnemyReachBoundaryEventHandler;
            _playerMovement.Dispose();
        }

        private void Update()
        {
            if(!_isNeedDetectEnemy) return;
            var closestEnemy = _enemyDetector.FindClosestEnemy();
            if (closestEnemy != null)
            {
                _playerGun.ShootAt(closestEnemy.transform.position);
            }
        }

        private void EnemyReachBoundaryEventHandler()
        {
            _health--;
            _publisher.Publish(new PlayerHealthWasChangedEvent(_health));
        }
    }
}