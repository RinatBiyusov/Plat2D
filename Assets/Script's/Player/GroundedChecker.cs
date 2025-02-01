using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    public bool IsGrounded {  get; private set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
    }
}
