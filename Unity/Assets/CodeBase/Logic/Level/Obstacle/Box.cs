using UnityEngine;

namespace CodeBase.Logic.Level.Obstacle
{
    public class Box : MonoBehaviour, IObstacle
    {
        [SerializeField, Min(1)] 
        private float _demage = 1;

        public float Damage => _demage;
    }
}