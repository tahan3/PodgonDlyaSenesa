using Cysharp.Threading.Tasks;
using Source.Scripts.Config;
using Source.Scripts.Gameplay.Controller;
using Source.Scripts.Gameplay.View;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameView gameView;
        [SerializeField] private ClusterView clusterPrefab;
        [SerializeField] private WordSlotView wordSlotPrefab;
        [SerializeField] private SlotsContainerView slotsContainerPrefab;
        
        public override async void InstallBindings()
        {
            var levelConfig = Container.Resolve<GameplayConfig>().Levels[0];

            var clusterPlacement =
                new ClusterPlacement(levelConfig, clusterPrefab, wordSlotPrefab, slotsContainerPrefab);
            clusterPlacement.Initialize(gameView);
        }
    }
}