using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Config;
using Source.Scripts.Gameplay.Controller;
using Source.Scripts.Gameplay.Model;
using Source.Scripts.Gameplay.View;
using Source.Scripts.Loading;
using Source.Scripts.PopUp;
using Source.Scripts.Widget;
using Source.Scripts.Widget.Gameplay;
using UnityEngine;
using Zenject;

namespace Source.Scripts.StateMachine
{
    public class GameplayState : IState
    {
        private readonly DiContainer _container;
        private readonly IUIService _uiService;
        private readonly IStateMachine _stateMachine;
        
        private IPopUpService<string> _popUpService;

        public GameplayState(DiContainer container, IUIService uiService, IStateMachine stateMachine)
        {
            _container = container;
            _uiService = uiService;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter(CancellationToken token)
        {
            //kal
            var gameView = _container.Resolve<GameView>();
            var clusterPrefab = _container.Resolve<ClusterView>();
            var wordSlotPrefab = _container.Resolve<WordSlotView>();
            var slotsContainerPrefab = _container.Resolve<SlotsContainerView>();
            //
            
            var levelConfig = _container.Resolve<GameplayConfig>().Levels[0/*progressService.GetProgressData().levelIndex*/];
            var runtimeData = new RuntimeData(levelConfig.Words.ToList(), levelConfig.Segments.ToList());

            _popUpService = new GameplayTipPopUpService(_uiService, 1f);
            
            var validator = new BoardValidator(runtimeData, levelConfig);
            validator.OnValidated += ValidationResultCheck;//Observable
            
            var clusterPlacement =
                new ClusterPlacement(runtimeData, clusterPrefab, wordSlotPrefab, slotsContainerPrefab);
            clusterPlacement.Initialize(gameView);

            var gameplayWidget = await _uiService.GetWidget<GameplayWidget>();
            gameplayWidget.ValidateButton.onClick.AddListener(validator.Validate);
        }

        public void Exit()
        {
            
        }

        private void ValidationResultCheck(ValidationResult result)
        {
            if (!result.IsValid)
            {
                _popUpService.ShowPopUp(result.Message);
            }
            else
            {
                _stateMachine.ChangeState<VictoryState>();
            }
        }
    }
}