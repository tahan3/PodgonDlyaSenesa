using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.StateMachine
{
    public interface IState
    {
        public UniTask Enter(CancellationToken token);
        public void Exit();
    }
}