using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Widget
{
    public class CanvasWidget : Widget
    {
        [SerializeField] private Canvas canvas;
        
        public override void Show()
        {
            canvas.enabled = true;
        }

        public override void Hide()
        {
            canvas.enabled = false;
        }
    }
}