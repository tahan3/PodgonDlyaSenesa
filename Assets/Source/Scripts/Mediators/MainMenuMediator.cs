using Cysharp.Threading.Tasks;
using Source.Scripts.StateMachine;
using Source.Scripts.Widget;

namespace Source.Scripts.Mediators
{
    public class MainMenuMediator : IMediator
    {
        private readonly IUIService _uiService;
        private readonly IStateMachine _stateMachine;

        private MainMenuWidget _mainMenuWidget;
        private SettingsWidget _settingsWidget;

        public MainMenuMediator(IUIService uiService, IStateMachine stateMachine)
        {
            _uiService = uiService;
            _stateMachine = stateMachine;
        }

        public async UniTask ExecuteMediation()
        {
            _mainMenuWidget = await _uiService.GetWidget<MainMenuWidget>();
            _settingsWidget = await _uiService.GetWidget<SettingsWidget>();
            
            _mainMenuWidget.PlayButton.onClick.AddListener(OnPlayButtonClicked);
            _mainMenuWidget.SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
            
            _settingsWidget.BackButton.onClick.AddListener(OnSettingsBackButtonClicked);
            
            _settingsWidget.Hide();
            _mainMenuWidget.Show();
        }

        private void OnSettingsBackButtonClicked()
        {
            _settingsWidget.Hide();
            _mainMenuWidget.Show();
        }

        private void OnSettingsButtonClicked()
        {
            _settingsWidget.Show();
            _mainMenuWidget.Hide();
        }

        private void OnPlayButtonClicked()
        {
            _stateMachine.ChangeState<GameplayLoadingState>();
        }
    }
}