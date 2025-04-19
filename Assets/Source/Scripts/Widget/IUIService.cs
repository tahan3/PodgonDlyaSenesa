using Cysharp.Threading.Tasks;

namespace Source.Scripts.Widget
{
    public interface IUIService
    {
        public UniTask<TWidget> GetWidget<TWidget>() where TWidget : IWidget;
    }
}