using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Scripts.Config;
using Source.Scripts.Gameplay.Controller;
using Source.Scripts.Gameplay.Model;
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
            var runtimeData = new RuntimeData(levelConfig.Words.ToList(), levelConfig.Segments.ToList());

            var validator = new BoardValidator(runtimeData, levelConfig);
            validator.OnValidated += (x) => Debug.Log(x.Message);
            gameView.ValidateButton.onClick.AddListener(validator.Validate);

            var clusterPlacement =
                new ClusterPlacement(runtimeData, clusterPrefab, wordSlotPrefab, slotsContainerPrefab);
            clusterPlacement.Initialize(gameView);
        }
    }
}