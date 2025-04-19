using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Widget;
using Source.Scripts.Widget.Gameplay;

namespace Source.Scripts.StateMachine
{
    public class VictoryState : IState
    {
        private readonly IUIService _uiService;
        private readonly IStateMachine _stateMachine;

        private VictoryWidget _victoryWidget;

        public VictoryState(IUIService uiService, IStateMachine stateMachine)
        {
            _uiService = uiService;
            _stateMachine = stateMachine;
        }

        public async UniTask Enter(CancellationToken token)
        {
            _victoryWidget = await _uiService.GetWidget<VictoryWidget>();
            _victoryWidget.Show();
            
            _victoryWidget.MainMenuButton.onClick.AddListener(ShowMainMenu);
            _victoryWidget.NextLevelButton.onClick.AddListener(LoadNextLevel);
        }

        private void LoadNextLevel()
        {
            _stateMachine.ChangeState<GameplayLoadingState>();
        }

        private void ShowMainMenu()
        {
            _stateMachine.ChangeState<MainMenuLoadingState>();
        }
        
        public void Exit()
        {
            _victoryWidget.Hide();
        }
    }
}