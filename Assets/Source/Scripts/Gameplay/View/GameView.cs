using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Gameplay.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Transform clustersParent;
        [SerializeField] private Transform slotsParent;

        public Transform ClustersParent => clustersParent;
        public Transform SlotsParent => slotsParent;
    }
}