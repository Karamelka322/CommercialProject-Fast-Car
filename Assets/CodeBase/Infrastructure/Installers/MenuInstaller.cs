using CodeBase.Mediator;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    internal class MenuInstaller : MonoInstaller
    {
        [SerializeField] 
        private MenuMediator _mediator;

        private DiContainer _projectContainer;

        public override void InstallBindings()
        {
            _projectContainer = Container.ParentContainers[0];
            
            BindMediator();
        }

        private void OnDestroy()
        {
            UnbindMediator();
        }

        private void BindMediator() => 
            _projectContainer.Bind<IMenuMediator>().FromInstance(_mediator).AsSingle();

        private void UnbindMediator() => 
            _projectContainer.Unbind<IMenuMediator>();
    }
}