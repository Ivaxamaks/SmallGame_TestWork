using Common.StateMachine;
using Cysharp.Threading.Tasks;
using UI;

namespace GameStates
{
    public class EndGameState : IState
    {
        private readonly UIController _uiController;
        private readonly LastGameResultProvider _lastGameResult;

        private StateMachine _stateMachine;

        public EndGameState(UIController uiController,
            LastGameResultProvider lastGameResult)
        {
            _uiController = uiController;
            _lastGameResult = lastGameResult;
        }
        
        public void Initialize(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public UniTask Enter()
        {
            _uiController.ShowEndBattlePanelHandler(_lastGameResult, EndScreenPanelWasClosed);
            return UniTask.CompletedTask;
        }

        private async void EndScreenPanelWasClosed()
        {
            await _stateMachine.Enter<GameRunningState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}