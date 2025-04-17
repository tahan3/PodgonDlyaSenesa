using Cysharp.Threading.Tasks;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Source.Scripts.Config.Core
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigService _localConfigService;
        private readonly IConfigService _remoteConfigService;

        public ConfigService(IConfigService localConfigService, IConfigService remoteConfigService)
        {
            _localConfigService = localConfigService;
            _remoteConfigService = remoteConfigService;
        }

        public async UniTask<T> GetConfig<T>() where T : IConfig
        {
            return Application.internetReachability == NetworkReachability.NotReachable
                ? await _localConfigService.GetConfig<T>()
                : await _remoteConfigService.GetConfig<T>();
        }
    }
}