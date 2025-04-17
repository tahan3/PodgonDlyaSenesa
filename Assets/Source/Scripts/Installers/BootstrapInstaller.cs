using Source.Scripts.Config;
using Source.Scripts.Config.Core;
using Source.Scripts.Config.Core.Local;
using Source.Scripts.Config.Core.Remote;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Source.Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private string localConfigPath;
        [SerializeField] private string remoteConfigKey;
        
        public override async void InstallBindings()
        {
            var configService = new ConfigService(
                new LocalConfigService(localConfigPath),
                new RemoteConfigService(remoteConfigKey));

            var gameplayConfig = await configService.GetConfig<GameplayConfig>();
            
            Container.BindInstance(gameplayConfig).AsSingle();

            SceneManager.LoadScene(1);
        }
    }
}