using CodeBase.Logic.Menu;
using CodeBase.Mediator;
using CodeBase.Services.Data.ReadWrite;
using CodeBase.Services.Window;
using Zenject;

namespace CodeBase.UI.Windows
{
    public class GarageWindow : UIWindow
    {
        private IReadWriteDataService _readWriteDataService;
        private IWindowService _windowService;
        private IMenuMediator _mediator;

        [Inject]
        public void Construct(IWindowService windowService, IReadWriteDataService readWriteDataService, IMenuMediator mediator)
        {
            _readWriteDataService = readWriteDataService;
            _windowService = windowService;
            _mediator = mediator;
        }

        protected override void OnOpen()
        {
            _windowService.Register(this);
            _readWriteDataService.InformSingleReaders(gameObject);
        }

        protected override void OnClose()
        {
            _windowService.Unregister(this);
            _readWriteDataService.InformSingleWriters(gameObject);

            _mediator.ChangeMenuState(MenuState.MainMenu);
            
            Destroy(gameObject);
        }

        public override void Show() => 
            gameObject.SetActive(true);

        public override void Unshow() => 
            gameObject.SetActive(false);
    }
}