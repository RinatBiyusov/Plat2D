public class HealthBarSlider : BaseSliders
{
    protected override void OnHealthChanged() => Slider.value = Health.CurrentPoints;
}