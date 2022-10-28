using CodeBase.Services.AssetProvider;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            ProjectContext projectContext = InitProjectContext();
            GameUpdate gameUpdate = InitGameUpdate();
            
            InitFrameDebugger();

            InitGame(projectContext, gameUpdate);
        }

        private static void InitGame(Context projectContext, GameUpdate gameUpdate) => 
            new Game(projectContext.Container, gameUpdate, gameUpdate);

        private static GameUpdate InitGameUpdate()
        {
            GameUpdate prefab = Resources.Load<GameUpdate>(AssetPath.GameUpdatePath);
            GameUpdate gameUpdate = Instantiate(prefab);
            return gameUpdate;
        }

        private static ProjectContext InitProjectContext()
        {
            ProjectContext prefab = Resources.Load<ProjectContext>(AssetPath.ProjectContextPath);
            ProjectContext projectContext = Instantiate(prefab);
            projectContext.Initialize();
            return projectContext;
        }


        private static void InitFrameDebugger() => 
            Instantiate(Resources.Load<GameObject>(AssetPath.FrameDebuggerPath));

    }
}