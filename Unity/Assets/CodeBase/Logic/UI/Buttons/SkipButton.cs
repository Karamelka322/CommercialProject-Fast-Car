using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using CodeBase.Services.Update;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Buttons
{
    public class SkipButton : UIButton
    {
        private const float LifeTime = 10;
        
        private IMenuMediator _mediator;
        private IUpdateService _updateService;

        protected override void Awake()
        {
            base.Awake();
            Invoke(nameof(OnClickButton), LifeTime);
        }

        [Inject]
        public void Construct(IMenuMediator mediator, IUpdateService updateService)
        {
            _updateService = updateService;
            _mediator = mediator;
        }

#if UNITY_EDITOR
        
        private void OnEnable() => 
            _updateService.OnUpdate += OnUpdate;

        private void OnDisable() => 
            _updateService.OnUpdate -= OnUpdate;

        private void OnUpdate()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                _mediator.ChangeMenuState(MenuState.MainMenu);
                Destroy(gameObject);
            }
        }
        
#endif
        
        protected override void OnClickButton()
        {
            _mediator.ChangeMenuState(MenuState.MainMenu);
            Destroy(gameObject);
        }
    }
}