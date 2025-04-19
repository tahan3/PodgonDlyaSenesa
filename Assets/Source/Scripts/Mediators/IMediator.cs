using Cysharp.Threading.Tasks;

namespace Source.Scripts.Mediators
{
    public interface IMediator
    {
        public UniTask ExecuteMediation();
    }
}