using CodeBase.Services.Window;
using Zenject;

namespace CodeBase.UI.Windows
{
    public class MainMenuWindow : UIWindow
    {
        private IWindowService _windowService;

        [Inject]
        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
        }

        protected override void OnOpen() => 
            _windowService.Register(this);

        protected override void OnClose() => 
            Destroy(gameObject);

        public override void Show() => 
            gameObject.SetActive(true);

        public override void Unshow() => 
            gameObject.SetActive(false);
    }
}