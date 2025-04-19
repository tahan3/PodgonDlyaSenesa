using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.SceneManagement
{
    public class SceneManager : ISceneManager
    {
        public async UniTask LoadScene(string sceneKey, CancellationToken token = default)
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneKey).WithCancellation(token);
        }
    }
}