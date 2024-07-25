using Enemy;
using GameStates;
using Player;
using Services;
using Settings;
using UI;
using UI.EndBattlePanel;
using UI.TopHUDPanel;
using UniTaskPubSub;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettings _gameSettings;
    
    [SerializeField] private Player.Player _player;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Boundary _boundary;

    [SerializeField] private Enemy.Enemy _enemyPrefab;
    [SerializeField] private EnemySpawnPositionsProvider _enemySpawnPositionsProvider;

    [SerializeField] private TopHUDModel topHUD;
    [SerializeField] private EndBattlePanelModel _endPanelModel;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameSettings);
        builder.RegisterInstance(_player);
        builder.RegisterInstance(topHUD);
        builder.RegisterInstance(_endPanelModel);
        builder.RegisterInstance(_enemySpawnPositionsProvider);
        builder.RegisterInstance(_boundary);
        builder.RegisterInstance(_enemyPrefab);
        builder.RegisterInstance(_bulletPrefab);

        builder.Register<BootstrapState>(Lifetime.Singleton);
        builder.Register<GameRunningState>(Lifetime.Singleton);
        builder.Register<EndGameState>(Lifetime.Singleton);

        builder.Register<AsyncMessageBus>(Lifetime.Singleton);
        builder.Register<InputHandler>(Lifetime.Singleton);
        builder.Register<GameController>(Lifetime.Singleton);
        builder.Register<LastGameResultProvider>(Lifetime.Singleton);
        
        builder.Register<UIController>(Lifetime.Singleton);

        builder.Register<EnemySpawnController>(Lifetime.Singleton);
        builder.Register<EnemyFactory>(Lifetime.Singleton);
        
        builder.RegisterEntryPoint<GameStateMachine>();
    }
}