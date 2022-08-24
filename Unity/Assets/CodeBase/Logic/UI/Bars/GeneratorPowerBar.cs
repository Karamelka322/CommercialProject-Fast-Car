namespace CodeBase.UI
{
    public class GeneratorPowerBar : UIBar
    {
        public override float Value
        {
            get => _image.fillAmount;
            set => _image.fillAmount = value;
        }
    }
}