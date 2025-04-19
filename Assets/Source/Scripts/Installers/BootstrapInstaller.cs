using Source.Scripts.Config.Core;
using Source.Scripts.Config.Core.Local;
using Source.Scripts.Config.Core.Remote;
using Source.Scripts.Loading;
using Source.Scripts.SceneManagement;
using Source.Scripts.StateMachine;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [Header("Config")]
        [SerializeField] private string localConfigPath;
        [SerializeField] private string remoteConfigKey;

        [Header("Scene management")]
        [SerializeField] private SceneKeys sceneKeys;

        [Header("UI")]
        [SerializeField] private GameObject loadingScreen;
        
        public override async void InstallBindings()
        {
            var configService = new ConfigService(
                new LocalConfigService(localConfigPath),
                new RemoteConfigService(remoteConfigKey));

            var stateMachine = Container.Instantiate<GameStateMachine>();
            var sceneManager = new SceneManager();
            var loadingScreenService = new LoadingScreenService(loadingScreen);
            
            Container.BindInstance(sceneKeys).AsSingle();
            Container.Bind<ILoadingScreenService>().FromInstance(loadingScreenService).AsSingle();
            Container.Bind<ISceneManager>().FromInstance(sceneManager).AsSingle();
            Container.Bind<IConfigService>().FromInstance(configService).AsSingle();
            Container.Bind<IStateMachine>().FromInstance(stateMachine).AsSingle();

            await stateMachine.ChangeState<BootstrapState>();
        }
    }
}