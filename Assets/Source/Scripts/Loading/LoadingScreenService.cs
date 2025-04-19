using UnityEngine;

namespace Source.Scripts.Loading
{
    public class LoadingScreenService : ILoadingScreenService
    {
        private readonly GameObject _loadingCurtain;

        public LoadingScreenService(GameObject loadingCurtainPrefab)
        {
            _loadingCurtain = Object.Instantiate(loadingCurtainPrefab);
            Object.DontDestroyOnLoad(_loadingCurtain);
        }

        public void ShowLoadingScreen()
        {
            _loadingCurtain.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingCurtain.SetActive(false);
        }
    }
}