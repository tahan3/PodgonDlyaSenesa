using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace Source.Scripts.Config.Core
{
    public class ConfigService : IConfigService
    {
        private readonly Dictionary<NetworkReachability, IConfigService> _reachabilityMap =
            new Dictionary<NetworkReachability, IConfigService>();

        public ConfigService(IConfigService localConfigService, IConfigService remoteConfigService)
        {
            _reachabilityMap[NetworkReachability.NotReachable] = localConfigService;
            _reachabilityMap[NetworkReachability.ReachableViaCarrierDataNetwork] = remoteConfigService;
            _reachabilityMap[NetworkReachability.ReachableViaLocalAreaNetwork] = remoteConfigService;
        }

        public async UniTask<T> GetConfig<T>() where T : IConfig
        {
            return await _reachabilityMap[Application.internetReachability].GetConfig<T>();
        }
    }
}