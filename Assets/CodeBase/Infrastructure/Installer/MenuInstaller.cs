using CodeBase.Mediator;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    internal class MenuInstaller : MonoInstaller
    {
        [SerializeField] 
        private MenuMediator _mediator;

        public override void InstallBindings() => 
            BindMediator();

        protected override void UninstallBindings() => 
            UnbindMediator();

        private void BindMediator() => 
            Container.ParentContainers[0].Bind<IMenuMediator>().FromInstance(_mediator).AsCached();

        private void UnbindMediator() => 
            Container.ParentContainers[0].Unbind<IMenuMediator>();
    }
}