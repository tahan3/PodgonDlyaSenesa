using Source.Scripts.DragDrop;
using UnityEngine;

namespace Source.Scripts.Gameplay.View
{
    public class WordSlotView : MonoBehaviour, ISlotView
    {
        [SerializeField] private GraphicsHighlighter slot;
        
        public void OnPlaced()
        {
            slot.Highlight(false);
        }
    }
}