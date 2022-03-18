using CodeBase.Data.Static.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerPreview : MonoBehaviour
    {
        [SerializeField] 
        private PlayerTypeId _type;
        
        [InlineProperty, SerializeField, BoxGroup("Body"), HideLabel] 
        private MeshData _body;
        
        [InlineProperty, SerializeField, BoxGroup("FrontLeftWheel"), HideLabel] 
        private MeshData _frontLeftWheel;
        
        [InlineProperty, SerializeField, BoxGroup("FrontRightWheel"), HideLabel] 
        private MeshData _frontRightWheel;
        
        [InlineProperty, SerializeField, BoxGroup("RearLeftWheel"), HideLabel] 
        private MeshData _rearLeftWheel;
        
        [InlineProperty, SerializeField, BoxGroup("RearRightWheel"), HideLabel]
        private MeshData _rearRightWheel;
        
        public PlayerTypeId Type => _type;
        
        public MeshData Body => _body;
        public MeshData FrontLeftWheel => _frontLeftWheel;
        public MeshData FrontRightWheel => _frontRightWheel;
        public MeshData RearLeftWheel => _rearLeftWheel;
        public MeshData RearRightWheel => _rearRightWheel;
    }
}