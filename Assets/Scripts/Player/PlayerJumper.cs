using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 15f;

    public void Jump(Rigidbody2D rigidbody2D) => rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
}