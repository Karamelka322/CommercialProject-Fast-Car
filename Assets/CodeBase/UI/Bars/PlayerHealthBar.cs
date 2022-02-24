using CodeBase.Data.Perseistent;
using CodeBase.Logic.Player;
using CodeBase.Services.Data.ReaderWriter;

namespace CodeBase.UI
{
    public class PlayerHealthBar : UIBar, IReadData
    {
        private PlayerHealth _playerHealth;

        private float _maxHealth;
        
        public void Construct(PlayerHealth playerHealth) => 
            _playerHealth = playerHealth;

        private void Start() => 
            _playerHealth.OnChangeHealth += OnChangeHealth;

        private void OnDisable() => 
            _playerHealth.OnChangeHealth -= OnChangeHealth;

        private void OnChangeHealth(float health) => 
            Fill = health / _maxHealth;

        public void ReadData(PlayerPersistentData persistentData) => 
            _maxHealth = persistentData.SessionData.PlayerData.MaxHealth;
    }
}