using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.DragDrop
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Draggable : MonoBehaviour, IDraggable<PointerEventData>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Canvas _parentCanvas;
        private Transform _parentTransform;
        private CanvasGroup _canvasGroup;
        
        public event Action<PointerEventData> OnBeginDrag;
        public event Action<PointerEventData> OnEndDrag;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _parentCanvas = GetComponentInParent<Canvas>();
            _parentTransform = _parentCanvas.transform;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _rectTransform.SetParent(_parentTransform);
            
            OnBeginDrag?.Invoke(eventData);
        }
        
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            OnEndDrag?.Invoke(eventData);
        }
    }
}
