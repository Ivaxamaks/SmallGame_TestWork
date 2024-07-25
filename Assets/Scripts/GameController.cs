using System;
using Common.Extentions;
using Enemy;
using Events;
using UI;
using UniTaskPubSub;

public class GameController : IDisposable
{
    private readonly AsyncMessageBus _messageBus;
    private readonly UIController _uiController;
    private readonly EnemySpawnController _enemySpawnController;
        
    private CompositeDisposable _subscriptions;
    private Action<bool> _callback;
    private int _diedEnemyCount;

    public GameController(
        AsyncMessageBus messageBus,
        EnemySpawnController enemySpawnController)
    {
        _messageBus = messageBus;
        _enemySpawnController = enemySpawnController;
    }
        
    public void Initialize(Action<bool> callback)
    {
        _diedEnemyCount = 0;
        _callback = callback;
        _subscriptions = new CompositeDisposable
        {
            _messageBus.Subscribe<PlayerHealthWasChangedEvent>(PlayerHealthWasChangedHandler),
            _messageBus.Subscribe<EnemyDiedEvent>(EnemyDiedHandler)
        };
    }

    private void EnemyDiedHandler(EnemyDiedEvent eventData)
    {
        _diedEnemyCount++;
        if(_enemySpawnController.AllEnemiesWasSpawned && _diedEnemyCount == _enemySpawnController.SpawnedEnemyAmount)
            EndGame(true);
    }

    private void PlayerHealthWasChangedHandler(PlayerHealthWasChangedEvent eventData)
    {
        if(eventData.CurrentPlayerHealth != 0) return;
        EndGame(false);
    }

    private void EndGame(bool isWin)
    {
        _callback?.Invoke(isWin);
    }

    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}