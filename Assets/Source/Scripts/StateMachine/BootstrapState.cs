using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Config;
using Source.Scripts.Config.Core;
using Source.Scripts.Loading;
using Source.Scripts.SceneManagement;
using Zenject;

namespace Source.Scripts.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly ISceneManager _sceneManager;
        private readonly SceneKeys _sceneKeys;
        private readonly IConfigService _configService;
        private readonly ILoadingScreenService _loadingScreenService;
        private readonly DiContainer _container;

        public BootstrapState(ISceneManager sceneManager, SceneKeys sceneKeys, IConfigService configService,
            DiContainer container, ILoadingScreenService loadingScreenService)
        {
            _sceneManager = sceneManager;
            _sceneKeys = sceneKeys;
            _configService = configService;
            _container = container;
            _loadingScreenService = loadingScreenService;
        }

        public async UniTask Enter(CancellationToken token)
        {
            _loadingScreenService.ShowLoadingScreen();
            
            var gameplayConfig = await _configService.GetConfig<GameplayConfig>();

            _container.BindInstance(gameplayConfig).AsSingle();

            _sceneManager.LoadScene(_sceneKeys.MainMenu, token);
        }

        public void Exit()
        {
            _loadingScreenService.HideLoadingScreen();
        }
    }
}