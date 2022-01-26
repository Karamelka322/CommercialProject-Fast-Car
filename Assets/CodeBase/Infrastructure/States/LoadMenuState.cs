using CodeBase.Services.Input.Factories.UI;
using CodeBase.Services.Input.LoadScene;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.States
{
    public class LoadMenuState : IState
    {
        private const float SpeedHideCurtain = 1f;
        private const float DelayHideCurtain = 0.5f;

        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IUIFactory _uiFactory;

        public LoadMenuState(GameStateMachine stateMachine, ISceneLoaderService sceneLoader, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Enter() => 
            _sceneLoader.Load(SceneNameConstant.Menu, LoadSceneMode.Single, HideLoadingCurtain);

        public void Exit() { }

        private void HideLoadingCurtain() => 
            _uiFactory.LoadingCurtain.Hide(SpeedHideCurtain, DelayHideCurtain, DestroyCurtain);

        private void DestroyCurtain() => 
            Object.Destroy(_uiFactory.LoadingCurtain.gameObject);
    }
}