using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class BaseSliders : HealthBar
{
    protected Slider Slider;

    protected override void Awake()
    {
        base.Awake();
        Slider = GetComponent<Slider>();
    }

    private void Start()
    {
        Slider.wholeNumbers = false;
        Slider.maxValue = Health.MaxPoints;
        Slider.value = Slider.maxValue;
    }
}