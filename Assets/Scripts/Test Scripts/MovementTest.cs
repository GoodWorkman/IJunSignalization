using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputController))]
public class MovementTest : MonoBehaviour, IPlayer
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _rotateSpeed = 3f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundPoint;
    
    private InputController _controller;
    private Rigidbody _rigidbody;
    private float _groundCheckRadius = 0.5f;
    private bool _isGrounded;
    private bool _canJump;
    
    
    private void OnValidate()
    {
        _controller ??= GetComponent<InputController>();
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CheckJumpStatus();
    }

    private void FixedUpdate()
    {
        Move();
        TryJump();
    }

    private void CheckJumpStatus()
    {
        _isGrounded = Physics.CheckSphere(_groundPoint.position, _groundCheckRadius, _groundLayer);

        if (_isGrounded && _controller.IsJump)
        {
            _canJump = true;
        }
    }

    private void TryJump()
    {
        if (_canJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _canJump = false;
        }
    }
    private void Move()
    {
        Vector3 velocity = new Vector3(_controller.Horizontal, 0f, _controller.Vertical).normalized * _moveSpeed;

        velocity.y = _rigidbody.velocity.y;

        Vector3 worldVelocity = transform.TransformVector(velocity);

        _rigidbody.velocity = worldVelocity;

        _rigidbody.angularVelocity = new Vector3(0f,_controller.MouseInput.x * _rotateSpeed , 0f);
    }
}
