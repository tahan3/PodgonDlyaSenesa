using System;

namespace Source.Scripts.DragDrop
{
    public interface IDraggable<out T>
    {
        public event Action<T> OnBeginDrag;
        public event Action<T> OnEndDrag;
    }
}