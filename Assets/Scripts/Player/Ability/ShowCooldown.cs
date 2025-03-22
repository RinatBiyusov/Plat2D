using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Slider))]
public class CooldownRenderer : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirismAbility;
    [SerializeField] private Image _fillImage;

    private readonly float _maxValueSlider = 100f;
    private readonly float _minValueSlider = 0f;
    private readonly Color _mainColor = Color.green;
    private readonly Color _secondaryColor = Color.red;

    private List<Color> _colors;
    private Slider _slider;
    private int _currentIndexColor = 0;
    private Coroutine _coroutine;
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _vampirismAbility.AbilityStatusChanged += DrawCooldownStatus;
    }

    private void Start()
    {
        _fillImage.color = _mainColor;
        _slider.maxValue = _maxValueSlider;
        _slider.minValue = _minValueSlider;
        _slider.wholeNumbers = false;

        _colors = new List<Color>()
        {
            _mainColor,
            _secondaryColor
        };
    }

    private void OnDisable()
    {
        _vampirismAbility.AbilityStatusChanged -= DrawCooldownStatus;
    }

    private void DrawCooldownStatus()
    {
        if (_vampirismAbility.IsActivated)
        {
            StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ChangeSmoothCooldown(_slider.minValue, _vampirismAbility.DurationAbility));
        }
        else if (_vampirismAbility.IsOnCooldown)
        {
            StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(ChangeSmoothCooldown(_slider.maxValue, _vampirismAbility.DurationAbility));
        }
    }

    private Color ChangeColor()
    {
        _currentIndexColor++;
        return _colors[_currentIndexColor % _colors.Count];
    }
    
    private IEnumerator ChangeSmoothCooldown(float endPoint, float timeOfChange)
    {
        float elapsed = 0f;
        float startValue = _slider.value;
        
        while (elapsed < timeOfChange)
        {
            _slider.value = Mathf.Lerp(startValue, endPoint, elapsed / timeOfChange);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        _fillImage.color = ChangeColor();
        _slider.value = endPoint;
    }
}