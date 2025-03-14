using UnityEngine;

namespace Buttons
{
    public class ButtonDealDamage : BaseButton
    {
        [SerializeField] private float _damageButton = 25;
        
        protected override void OnButtonClick()
        {
            HealthTarget.TakeDamage(_damageButton);
        }
    }
}