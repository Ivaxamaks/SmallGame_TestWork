using Common.StateMachine;
using GameStates;
using Services;
using VContainer.Unity;

public class GameStateMachine : StateMachine, IStartable, ITickable
{
    private readonly InputHandler _inputHandler;

    public GameStateMachine(BootstrapState bootstrapState,
        GameRunningState gameRunningState,
        EndGameState endGameState,
        InputHandler inputHandler)
        : base(bootstrapState, gameRunningState, endGameState)
    {
        _inputHandler = inputHandler;
    }

    
    public async void Start()
    {
        await Enter<BootstrapState>();
    }
    
    public void Tick()
    {
        _inputHandler.Tick();
    }
}