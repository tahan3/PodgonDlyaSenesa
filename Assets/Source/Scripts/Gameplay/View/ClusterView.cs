using System;
using Source.Scripts.DragDrop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.Gameplay.View
{
    public class ClusterView : MonoBehaviour
    {
        [Serializable]
        private class Letter
        {
            [SerializeField] private TextMeshProUGUI label;
            [SerializeField] private RectTransform rectTransform;
            
            public TextMeshProUGUI Label => label;
            public RectTransform RectTransform => rectTransform;

            public Letter Clone()
            {
                Letter clone = new Letter();
                clone.rectTransform = Instantiate(rectTransform, rectTransform.parent);
                clone.label = clone.rectTransform.GetComponentInChildren<TextMeshProUGUI>();
                return clone;
            }
        }
        
        [SerializeField] private Letter letter;
        [SerializeField] private float letterOffset = 100f;
        [SerializeField] private Draggable draggable;
        [SerializeField] private RectTransform rectTransform;
        
        private Action<ClusterView, WordSlotView> _onDrop;
        private Action<ClusterView> _onMiss;

        private void OnEnable()
        {
            draggable.OnEndDrag += PlaceCluster;
        }
        private void OnDisable()
        {
            draggable.OnEndDrag -= PlaceCluster;
        }

        public void Setup(string cluster, Action<ClusterView, WordSlotView> onDrop, Action<ClusterView> onMiss)
        {
            _onDrop = onDrop;
            _onMiss = onMiss;
            CreateWord(cluster);
        }
        
        private void CreateWord(string word)
        {
            var currentOffset = letterOffset;

            letter.Label.text = word[0].ToString();
            
            for (int i = 1; i < word.Length; i++)
            {
                var clone = letter.Clone();
                clone.Label.text = word[i].ToString();
                clone.RectTransform.anchoredPosition = new Vector2(currentOffset, 0);
                currentOffset += letterOffset;
            }

            rectTransform.sizeDelta = new Vector2(currentOffset, rectTransform.sizeDelta.y);
        }

        private void PlaceCluster(PointerEventData eventData)
        {
            if (eventData.pointerEnter != null)
            {
                if (eventData.pointerEnter.TryGetComponent<WordSlotView>(out var slot))
                {
                    slot.OnPlaced();
                    _onDrop?.Invoke(this, slot);
                    return;
                }
            }

            _onMiss?.Invoke(this);
        }
    }
}