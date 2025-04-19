using UnityEngine;

namespace Source.Scripts.Widget
{
    public abstract class Widget : MonoBehaviour, IWidget
    {
        public abstract void Show();

        public abstract void Hide();
    }
}