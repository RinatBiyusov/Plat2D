using System;
using UnityEngine;

public class InputPlayerReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public float HorizontalInput { get; private set; }

    public event Action JumpPressed;
    public event Action AttackPressed;
    public event Action AbilityPressed;

    private void Update()
    {
        HorizontalInput = Input.GetAxis(Horizontal);

        AttackButtonClick();
        JumpButtonClick();
        AbilityButtonCLick();
    }

    private void AbilityButtonCLick()
    {
        if (Input.GetKeyDown(KeyCode.E))
            AbilityPressed?.Invoke();
    }

    private void AttackButtonClick()
    {
        if (Input.GetMouseButtonDown(0))
            AttackPressed?.Invoke();
    }

    private void JumpButtonClick()
    {
        if (Input.GetButtonDown(Jump))
            JumpPressed?.Invoke();
    }
}