using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Mediator.Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] 
        private LevelMediator _mediator;

        public override void InstallBindings() => 
            BindMediator();

        protected override void UninstallBindings() => 
            UnbindMediator();

        private void BindMediator() => 
            Container.ParentContainers[0].Bind<ILevelMediator>().FromInstance(_mediator).AsCached();

        private void UnbindMediator() => 
            Container.ParentContainers[0].Unbind<ILevelMediator>();
    }
}