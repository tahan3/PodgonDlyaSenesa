using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace Source.Scripts.Config.Core.Remote
{
    public class RemoteConfigService : IConfigService
    {
        private struct UserAttributes { }
        private struct AppAttributes { }
        
        private readonly string _key;

        public RemoteConfigService(string key)
        {
            _key = key;
        }

        public async UniTask<T> GetConfig<T>() where T : IConfig
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                await UnityServices.InitializeAsync();
            }

            while (UnityServices.State == ServicesInitializationState.Initializing)
            {
                await UniTask.Yield();
            }

            await Unity.Services.RemoteConfig.RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(),
                new AppAttributes()).AsUniTask();

            if (Unity.Services.RemoteConfig.RemoteConfigService.Instance.requestStatus == ConfigRequestStatus.Success)
            {
                string jsonData = Unity.Services.RemoteConfig.RemoteConfigService.Instance.appConfig.GetJson(_key);
                Debug.Log("JSON Data: " + jsonData);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            Debug.LogError("Failed to fetch Remote Config");
            
            return default;
        }
    }
}