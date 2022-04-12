using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.UI.Windows
{
    public class VictoryWindow : MonoBehaviour, IReplayHandler
    {
        public void OnReplay() => 
            Destroy(gameObject);
    }
}