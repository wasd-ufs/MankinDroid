using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{   
    
    [SerializeField] private float playerMaximumMoveSpeed = 4;
    [SerializeField] private float jumpForce = 5;
    private Rigidbody2D _playerRigidBody;
    private bool _playerRightSide = false;
    public Vector2 _playerDirection;

    [SerializeField] private string _inputMap; //Droid ou Player
    
    [SerializeField] private InputActionAsset _inputAction;
    private InputAction _moveInput;
    private InputAction _jumpInput;
    
    private void OnEnable()
    {
        _inputAction.FindActionMap(_inputMap).Enable();
    }
    
    private void OnDisable()
    {
        _inputAction.FindActionMap(_inputMap).Disable();
    }
    
    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _moveInput = _inputAction.FindAction(_inputMap+"/Move");
        _jumpInput = _inputAction.FindAction(_inputMap+"/Jump");
    }
    

    void Update()
    {
        _playerDirection = _moveInput.ReadValue<Vector2>();

        if (_jumpInput.WasPressedThisFrame())
            Jump();
    }

    private void FixedUpdate()
    {
        _playerRigidBody.linearVelocity = new Vector2(
            _playerDirection.x * playerMaximumMoveSpeed, 
            _playerRigidBody.linearVelocity.y
            );

        if (_playerDirection.x > 0 && !_playerRightSide || _playerDirection.x < 0 && _playerRightSide) 
            TurnPlayer();
    }

    private void TurnPlayer()
    {
        _playerRightSide = !_playerRightSide;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {
        _playerRigidBody.linearVelocity = new Vector2(_playerDirection.x, _playerRigidBody.linearVelocity.y * jumpForce);
    }
}
