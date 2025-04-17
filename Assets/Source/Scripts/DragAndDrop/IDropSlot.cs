using System;
using UnityEngine.EventSystems;

namespace Source.Scripts.DragDrop
{
    public interface IDropSlot<out T>
    {
        public event Action<T> OnDrop;
        public void Drop(PointerEventData eventData);
    }
}