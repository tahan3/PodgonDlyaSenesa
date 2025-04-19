using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Widget
{
    public class SettingsWidget : CanvasWidget
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Toggle soundToggle;
        
        public Button BackButton => backButton;
        public Toggle SoundToggle => soundToggle;
    }
}