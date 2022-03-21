#if !NOT_UNITY3D

using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Zenject
{
    // We'd prefer to make this abstract but Unity 5.3.5 has a bug where references
    // can get lost during compile errors for classes that are abstract
    [DebuggerStepThrough]
    public class MonoInstallerBase : MonoBehaviour, IInstaller
    {
        [Inject]
        protected DiContainer Container
        {
            get; set;
        }

        public virtual bool IsEnabled
        {
            get { return enabled; }
        }

        public virtual void Start()
        {
            // Define this method so we expose the enabled check box
        }

        private void OnDestroy() => 
            UninstallBindings();

        public virtual void InstallBindings() => 
            Debug.LogWarning("Non Implemented Installer");

        protected virtual void UninstallBindings() => 
            Debug.LogWarning("Non Implemented Uninstaller");
    }
}

#endif

