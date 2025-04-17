using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.Scripts.DragDrop
{
    public class GraphicsHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Color targetColor = Color.green;
        [SerializeField] private Graphic targetGraphic;
        
        private Color _defaultColor;

        private void Start()
        {
            _defaultColor = targetGraphic.color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                Highlight(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
        }

        public void Highlight(bool mode)
        {
            targetGraphic.color = mode ? targetColor : _defaultColor;
        }
    }
}