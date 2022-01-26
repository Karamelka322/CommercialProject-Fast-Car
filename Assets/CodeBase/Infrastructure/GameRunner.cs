using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField]
        private GameBootstrapper _bootstrapperPrefab;
        
        private void Awake()
        {
            if(CheckBootstapper() == false)
            {
                if(CheckInitialScene() == false)
                    LoadInitialScene();
                
                Instantiate(_bootstrapperPrefab);
            }

            Destroy(gameObject);
        }

        private static void LoadInitialScene() => 
            SceneManager.LoadScene(SceneNameConstant.Initial);

        private static bool CheckBootstapper() => 
            FindObjectOfType<GameBootstrapper>() != null;

        private static bool CheckInitialScene() => 
            SceneManager.GetActiveScene().name == SceneNameConstant.Initial;
    }
}