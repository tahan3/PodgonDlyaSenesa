using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Source.Scripts.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        public event Action<IState> OnStateChanged;

        private IState _currentState;

        [Inject] private DiContainer _container;

        public async UniTask ChangeState<TState>(CancellationToken token = default) where TState : IState
        {
            _currentState?.Exit();

            _currentState = _container.Instantiate<TState>();
            OnStateChanged?.Invoke(_currentState);

            await _currentState.Enter(token);
        }
    }
}