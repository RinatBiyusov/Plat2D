using System.Collections;
using UnityEngine;

public class HealthBarSliderSmooth : BaseSliders
{
    [Range(5, 10)] [SerializeField] private int _speedChangingHeath;

    private Coroutine _coroutine;

    protected override void OnHealthChanged()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeSliderSmooth());
    }

    private IEnumerator ChangeSliderSmooth()
    {
        while (Mathf.Approximately(Slider.value, Health.CurrentPoints) == false)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, Health.CurrentPoints, Time.deltaTime * _speedChangingHeath);

            yield return null;
        }
    }
}