using System;
using Common.StateMachine;
using Cysharp.Threading.Tasks;
using Enemy;
using Settings;
using UI;
using UI.HealthBar;

namespace GameStates
{
    public class GameRunningState : IState
    {
        private readonly UIController _uiController;
        private readonly GameSettings _gameSettings;
        private readonly EnemySpawnController _enemySpawnController;
        private readonly GameController _gameController;
        private readonly LastGameResultProvider _lastGameResultProvider;
        private readonly Player.Player _player;
        private readonly HealthBarManager _healthBarManager;

        private IDisposable _subscription;
        private StateMachine _stateMachine;

        public GameRunningState(UIController uiController,
            EnemySpawnController enemySpawnController,
            GameController gameController,
            LastGameResultProvider lastGameResultProvider,
            Player.Player player,
            HealthBarManager healthBarManager)
        {
            _uiController = uiController;
            _enemySpawnController = enemySpawnController;
            _gameController = gameController;
            _lastGameResultProvider = lastGameResultProvider;
            _player = player;
            _healthBarManager = healthBarManager;
        }

        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public UniTask Enter()
        {
            _uiController.InitGameUI();
            _enemySpawnController.Initialize();
            _gameController.Initialize(OnGameEndHandler);
            _player.Initialize();
            _healthBarManager.Initialize();
            return UniTask.CompletedTask;
        }

        private async void OnGameEndHandler(bool isWin)
        {
            _lastGameResultProvider.IsWin = isWin;
            await _stateMachine.Enter<EndGameState>();
        }

        public UniTask Exit()
        {
           _enemySpawnController.Dispose();
           _gameController.Dispose();
           _player.Dispose();
           _healthBarManager.Dispose();
           return UniTask.CompletedTask;
        }
    }
}