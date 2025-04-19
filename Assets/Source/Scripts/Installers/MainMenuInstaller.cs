using Source.Scripts.StateMachine;
using Zenject;

namespace Source.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var stateMachine = Container.Resolve<IStateMachine>();
            Container.Inject(stateMachine);
            stateMachine.ChangeState<MainMenuState>();
        }
    }
}
