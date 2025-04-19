using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Widget.Gameplay
{
    public class VictoryWidget : CanvasWidget
    {
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button mainMenuButton;
        
        public Button NextLevelButton => nextLevelButton;
        public Button MainMenuButton => mainMenuButton;
    }
}