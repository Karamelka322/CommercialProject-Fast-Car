using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class PauseWindow : MonoBehaviour, IReplayHandler
    {
        public void OnReplay() => 
            Destroy(gameObject);
    }
}