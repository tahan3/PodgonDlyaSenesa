using UnityEngine;

namespace Source.Scripts.Gameplay.View
{
    public class SlotsContainerView : MonoBehaviour
    {
        [SerializeField] private Transform slotsParent;

        public Transform SlotsParent => slotsParent;
    }
}