using TMPro;
using UnityEngine;

namespace HealthBars
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HealthBarText : HealthBar
    {
        private TextMeshProUGUI _healthText;

        protected override void Awake()
        {
            base.Awake();
            _healthText =  GetComponent<TextMeshProUGUI>(); 
        }

        private void Start()
        {
            SetHealthText();
        }
        
        protected override void OnHealthChanged()
        {
            SetHealthText();
        }

        private void SetHealthText()
        {
            _healthText.text = $"{Health.CurrentPoints}/{Health.MaxPoints}";
        }
    }
}