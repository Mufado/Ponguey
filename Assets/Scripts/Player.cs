using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocity;
    public float jumpForce;
    public GameObject inputHandlerObject;

    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private bool _isAccelerating = false;
    [SerializeField] private bool _isSlowingDown = false;
    [SerializeField] private Enums.Player _playerEnum;

    private Rigidbody2D _rb;
    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputHandler = inputHandlerObject.GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        HandleJump();
        HandleAcceleration();
        HandleSlowDown();

        Move();
    }

    private void HandleJump()
    {
        if (_inputHandler.IsJumpTriggered && _isGrounded == true)
        {
            Jump();
        }
    }

    private void HandleAcceleration()
    {
        if (_inputHandler.IsSprintTriggered)
        {
            _isAccelerating = true;
        }
        else
        {
            _isAccelerating = false;
        }
    }

    private void HandleSlowDown()
    {
        if (_inputHandler.IsSlowDownTriggered)
        {
            _isSlowingDown = true;
        }
        else
        {
            _isSlowingDown = false;
        }
    }

    private void Move()
    {
        float speedMultiplier = _isAccelerating == _isSlowingDown ? 1f : _isAccelerating ? 2f : 0.5f;

        _rb.velocity = new Vector2(Time.fixedDeltaTime * speedMultiplier * velocity, _rb.velocity.y);
    }

    private void Jump()
    {
        _rb.AddForce(Time.fixedDeltaTime * jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    public void ChangeDirection()
    {
        velocity *= -1;
    }

    public void ToggleGrounded()
    {
        _isGrounded = !_isGrounded;
    }
}
