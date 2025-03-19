using UnityEngine;

[RequireComponent(typeof(InputPlayerReader), typeof(Rigidbody2D), typeof(PlayerAnimator))]
public class PlayerMover : MonoBehaviour
{ 
    [Range(0, 7)][SerializeField] private float _speed;
   
    private PlayerAnimator _animator;
    private InputPlayerReader _controller;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _controller = GetComponent<InputPlayerReader>();
    }

    public void Move()
    {
        Vector2 direction = new(_controller.HorizontalInput, 0);
        transform.Translate(direction * (_speed * Time.deltaTime), Space.World);

        _animator.TriggerRun(_controller.HorizontalInput);
    }
}
