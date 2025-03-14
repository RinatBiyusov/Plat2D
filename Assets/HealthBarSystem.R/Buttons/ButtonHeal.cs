using UnityEngine;

namespace Buttons
{
    public class ButtonHeal : BaseButton
    {
        [SerializeField] private float _healPoints = 25;
    
        protected override void OnButtonClick()
        {
            HealthTarget.TakeHeal(_healPoints);
        }
    }
}