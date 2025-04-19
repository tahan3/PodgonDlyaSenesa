using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Widget.Gameplay
{
    public class GameplayWidget : CanvasWidget
    {
        [SerializeField] private Button validateButton;
        
        public Button ValidateButton => validateButton;
    }
}