using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    private int _touchedGrounds = 0;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        CheckingGrounded();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
            _touchedGrounds++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ground ground))
            _touchedGrounds--;
    }
    
    private void CheckingGrounded()
    {
        if (_touchedGrounds > 0)
            IsGrounded = true;
        else
            IsGrounded = false;
    }
}
