using System;

namespace Source.Scripts.Gameplay.Controller
{
    public interface IValidator<out T>
    {
        public void Validate();
        public event Action<T> OnValidated;
    }
}