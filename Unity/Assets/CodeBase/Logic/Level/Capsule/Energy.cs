using CodeBase.Logic.World;
using CodeBase.Services.Replay;
using UnityEngine;

namespace CodeBase.Logic.Item
{
    [RequireComponent(typeof(FollowToObject), typeof(RandomRotate))]
    public class Energy : MonoBehaviour, IReplayHandler
    {
        [SerializeField] 
        private FollowToObject _followToObject;
        
        [Space]
        public int Power;

        public void Raise(Transform basket) => 
            _followToObject.Object = basket;

        public void OnReplay()
        {
            if(gameObject)
                Destroy(gameObject);
        }
    }
}