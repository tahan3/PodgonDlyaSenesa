using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Widget;
using Source.Scripts.Widget.PopUp;
using UnityEngine;

namespace Source.Scripts.PopUp
{
    public class GameplayTipPopUpService : IPopUpService<string>
    {
        private readonly IUIService _uiService;
        private readonly float _popUpDuration;
        
        private CancellationTokenSource _cancellationTokenSource;

        public GameplayTipPopUpService(IUIService uiService, float popUpDuration)
        {
            _uiService = uiService;
            _popUpDuration = popUpDuration;
        }

        public async void ShowPopUp(string popUpContent)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            var popUpWidget = await _uiService.GetWidget<PopUpWidget>();
            popUpWidget.Title.text = popUpContent;
            popUpWidget.Show();
            var cancelStatus = await UniTask.WaitForSeconds(_popUpDuration, cancellationToken: _cancellationTokenSource.Token)
                .SuppressCancellationThrow();

            if (!cancelStatus)
            {
                popUpWidget.Hide();
            }
        }
    }
}