using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Widget
{
    public class MainMenuWidget : CanvasWidget
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        
        public Button PlayButton => playButton;
        public Button SettingsButton => settingsButton;
    }
}