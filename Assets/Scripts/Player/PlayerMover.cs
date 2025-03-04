using UnityEngine;

[RequireComponent(typeof(InputPlayerReader), typeof(Rigidbody2D), typeof(PlayerAnimationController))]
public class PlayerMover : MonoBehaviour
{ 
    [Range(0, 7)][SerializeField] private float _speed;
    [Range(10, 20)][SerializeField] private float _jumpForce;
    [SerializeField] private GroundedChecker _groundedChecker;

    private PlayerAnimationController _animationController;
    private InputPlayerReader _controller;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animationController = GetComponent<PlayerAnimationController>();
        _controller = GetComponent<InputPlayerReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _controller.JumpPressed += Jump;
    }

    private void OnDisable()
    {
        _controller.JumpPressed -= Jump;
    }

    private void Update()
    {
        Move();
    }

    private void Jump()
    {
        if (_groundedChecker.IsGrounded)
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Move()
    {
        Vector2 direction = new(_controller.HorizontalInput, 0);
        transform.Translate(_speed * direction * Time.deltaTime, Space.World);

        _animationController.TriggerRun(_controller.HorizontalInput);
    }
}
