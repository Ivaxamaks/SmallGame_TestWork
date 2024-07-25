using Common.StateMachine;
using Cysharp.Threading.Tasks;
using UI;
using VContainer;

namespace GameStates
{
    public class BootstrapState : IState
    {
        private readonly IObjectResolver _container;
        private readonly UIController _uiController;
        private readonly Player.Player _player;

        private StateMachine _stateMachine;

        public BootstrapState(IObjectResolver container,
            UIController uiController,
            Player.Player player)
        {
            _container = container;
            _uiController = uiController;
            _player = player;
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _uiController.Initialize(); 
            _container.Inject(_player);
            await _stateMachine.Enter<GameRunningState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}