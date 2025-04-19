using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Loading;
using Source.Scripts.SceneManagement;

namespace Source.Scripts.StateMachine
{
    public class MainMenuLoadingState : IState
    {
        private readonly ISceneManager _sceneManager;
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly SceneKeys _sceneKeys;

        public MainMenuLoadingState(ISceneManager sceneManager, ILoadingScreenService loadingScreenService, SceneKeys sceneKeys)
        {
            _sceneManager = sceneManager;
            _loadingScreenService = loadingScreenService;
            _sceneKeys = sceneKeys;
        }

        public async UniTask Enter(CancellationToken token)
        {
            _loadingScreenService.ShowLoadingScreen();
            await _sceneManager.LoadScene(_sceneKeys.MainMenu, token);
        }

        public void Exit()
        {
            _loadingScreenService.HideLoadingScreen();
        }
    }
}