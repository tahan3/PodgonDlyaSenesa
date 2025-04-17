using Cysharp.Threading.Tasks;

namespace Source.Scripts.Config.Core
{
    public interface IConfigService
    {
        public UniTask<T> GetConfig<T>() where T : IConfig;
    }
}