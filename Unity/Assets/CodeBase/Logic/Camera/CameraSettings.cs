using UnityEngine;

namespace CodeBase.Logic.Camera
{
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class CameraSettings : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        
        private void Awake() => 
            _camera.depthTextureMode |= DepthTextureMode.Depth;
    }
}