using UnityEngine;

namespace CodeBase.logic.Player
{
    [RequireComponent(typeof(Player), typeof(Engine), typeof(StreeringGear))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Engine _engine;

        [SerializeField] 
        private StreeringGear _streeringGear;
    }
}