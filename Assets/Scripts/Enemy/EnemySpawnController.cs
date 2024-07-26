using System;
using System.Threading;
using Common.Pool;
using Cysharp.Threading.Tasks;
using Events;
using Settings;
using UniTaskPubSub;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawnController
    {
        public bool AllEnemiesWasSpawned => _playerAmountForWin <= _spawnedEnemies;
        public int SpawnedEnemyAmount => _spawnedEnemies;
        
        private readonly GameSettings _gameSettings;
        private readonly EnemySpawnPositionsProvider _spawnPositionsProvider;
        private readonly AsyncMessageBus _asyncMessageBus;

        private int _spawnedEnemies;
        private int _playerAmountForWin;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly MonoBehaviourPool<Enemy> _enemyPool;

        public EnemySpawnController(
            GameSettings gameSettings,
            EnemyFactory enemyFactory,
            EnemySpawnPositionsProvider spawnPositionsProvider,
            AsyncMessageBus asyncMessageBus)
        {
            _gameSettings = gameSettings;
            _spawnPositionsProvider = spawnPositionsProvider;
            _asyncMessageBus = asyncMessageBus;
            _enemyPool = new MonoBehaviourPool<Enemy>(enemyFactory.Create, spawnPositionsProvider.transform);
        }
        
        public void Initialize()
        {
            _spawnedEnemies = 0;
            _playerAmountForWin = Random.Range(_gameSettings.MinEnemiesToKill, _gameSettings.MaxEnemiesToKill);
            _cancellationTokenSource = new CancellationTokenSource();
            StartSpawningEnemies(_cancellationTokenSource.Token).Forget();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _enemyPool.ReleaseAll();
        }

        private async UniTask StartSpawningEnemies(CancellationToken cancellationToken)
        {
            while (_spawnedEnemies < _playerAmountForWin)
            {
                var waitTime = Random.Range(_gameSettings.MinEnemySpawnTime, _gameSettings.MaxEnemySpawnTime);
                
                try
                {
                    await UniTask.Delay((int)(waitTime * 1000), cancellationToken: cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var spawnPositions = _spawnPositionsProvider.EnemySpawnPositions;
            var spawnIndex = Random.Range(0, spawnPositions.Length);
            var enemy = _enemyPool.Take();
            enemy.Initialize(_gameSettings);
            _asyncMessageBus.Publish(new EnemySpawnEvent(enemy));
            enemy.OnDisposed += () => _enemyPool.Release(enemy);
            enemy.transform.position = spawnPositions[spawnIndex].position;
            _spawnedEnemies++;
        }
    }
}