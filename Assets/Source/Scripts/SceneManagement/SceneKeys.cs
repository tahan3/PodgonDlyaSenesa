using UnityEngine;

namespace Source.Scripts.SceneManagement
{
    [CreateAssetMenu(fileName = "SceneKeys", menuName = "SceneManagement/SceneKeys", order = 1)]
    public class SceneKeys : ScriptableObject
    {
        [SerializeField] private string bootstrap = "Bootstrap";
        [SerializeField] private string gameplay = "Gameplay";
        [SerializeField] private string mainMenu = "MainMenu";

        public string Bootstrap => bootstrap;
        public string Gameplay => gameplay;
        public string MainMenu => mainMenu;
    }
}