using CodeBase.Data.Perseistent;

namespace CodeBase.UI
{
    public class PlayerHealthBar : UIBar
    {
        private PlayerSessionData _playerSessionData;

        public void Construct(PlayerSessionData playerSessionData) => 
            _playerSessionData = playerSessionData;

        private void Start() => 
            _playerSessionData.ChangeHealth += OnChangeHealth;

        private void OnDisable() => 
            _playerSessionData.ChangeHealth -= OnChangeHealth;

        private void OnChangeHealth(float health) => 
            Fill = health / _playerSessionData.MaxHealth;
    }
}