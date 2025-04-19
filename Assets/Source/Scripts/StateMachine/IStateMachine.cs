using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.StateMachine
{
    public interface IStateMachine
    {
        public event Action<IState> OnStateChanged;

        public UniTask ChangeState<TState>(CancellationToken token = default) where TState : IState;
    }
}