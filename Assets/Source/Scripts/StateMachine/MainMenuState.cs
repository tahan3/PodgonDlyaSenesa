using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Loading;
using Source.Scripts.Mediators;
using Source.Scripts.SceneManagement;
using Source.Scripts.Widget;

namespace Source.Scripts.StateMachine
{
    public class MainMenuState : IState
    {
        private readonly IUIService _uiService;
        private readonly IStateMachine _stateMachine;
        private readonly ILoadingScreenService _loadingScreenService;

        public MainMenuState(IUIService uiService, IStateMachine stateMachine,
            ILoadingScreenService loadingScreenService)
        {
            _uiService = uiService;
            _stateMachine = stateMachine;
            _loadingScreenService = loadingScreenService;
        }

        public async UniTask Enter(CancellationToken token)
        {
            _loadingScreenService.ShowLoadingScreen();
            var mediator = new MainMenuMediator(_uiService, _stateMachine);
            await mediator.ExecuteMediation();
            _loadingScreenService.HideLoadingScreen();
        }

        public void Exit()
        {
            
        }
    }
}