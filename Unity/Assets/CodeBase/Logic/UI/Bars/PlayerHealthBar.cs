namespace CodeBase.UI
{
    public class PlayerHealthBar : UIBar
    {
        public override float Value
        {
            get => _image.fillAmount;
            set => _image.fillAmount = value;
        }
    }
}