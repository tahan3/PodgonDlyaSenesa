using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace Source.Scripts.Config.Core.Local
{
    public class LocalConfigService : IConfigService
    {
        private readonly string _path;

        public LocalConfigService(string path)
        {
            _path = path;
        }

        public async UniTask<T> GetConfig<T>() where T : IConfig
        {
            var jsonData = await File.ReadAllTextAsync(_path).AsUniTask();

            if (jsonData != null)
            {
                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            
            return default;
        }
    }
}