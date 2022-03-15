using CodeBase.Data.Static.Player;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerPreview : MonoBehaviour
    {
        [SerializeField] 
        private PlayerTypeId _type;

        public PlayerTypeId Type => _type;
    }
}