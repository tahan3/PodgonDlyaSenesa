using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.SceneManagement
{
    public interface ISceneManager
    {
        public UniTask LoadScene(string sceneKey, CancellationToken token = default);
    }
}