using System;
using System.Collections;
using UnityEngine;

public class InputPlayerReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public float HorizontalInput { get; private set; }

    public event Action JumpPressed;
    public event Action AttackPressed;

    private void Update()
    {
        HorizontalInput = Input.GetAxis(Horizontal);

        AttackButtonClick();
        JumpButtonClick();
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
