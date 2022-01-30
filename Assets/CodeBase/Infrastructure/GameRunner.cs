using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField]
        private GameBootstrapper _bootstrapperPrefab;
        
        private void Awake()
        {
            if(CheckBootstapper() == false) 
                Instantiate(_bootstrapperPrefab);

            Destroy(gameObject);
        }

        private static bool CheckBootstapper() => 
            FindObjectOfType<GameBootstrapper>() != null;
    }
}