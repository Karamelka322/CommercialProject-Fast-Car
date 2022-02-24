using UnityEngine;

namespace CodeBase.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] 
        private GeneratorPowerBar _generatorPowerBar;
        
        [SerializeField] 
        private PlayerHealthBar _playerHealthBar;

        [SerializeField] 
        private Transform _inputContainer;

        public Transform InputContainer => _inputContainer;
        public GeneratorPowerBar GeneratorPowerBar => _generatorPowerBar;
        public PlayerHealthBar PlayerHealthBar => _playerHealthBar;
    }
}