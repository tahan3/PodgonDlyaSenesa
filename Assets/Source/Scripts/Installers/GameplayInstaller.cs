using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Scripts.Config;
using Source.Scripts.Gameplay.Controller;
using Source.Scripts.Gameplay.Model;
using Source.Scripts.Gameplay.View;
using Source.Scripts.StateMachine;
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
            Container.BindInstance(gameView).AsCached();
            Container.BindInstance(clusterPrefab).AsCached();
            Container.BindInstance(wordSlotPrefab).AsCached();
            Container.BindInstance(slotsContainerPrefab).AsCached();
            
            var stateMachine = Container.Resolve<IStateMachine>();
            Container.Inject(stateMachine);
            stateMachine.ChangeState<GameplayState>();
        }
    }
}