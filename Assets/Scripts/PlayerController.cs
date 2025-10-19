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
    
    [SerializeField] private InputActionReference moveReference;
    [SerializeField] private InputActionReference interactReference;
    [SerializeField] private InputActionReference jumpReference;
        
    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>(); ;
        interactReference.action.performed += Interact;
        jumpReference.action.performed += Jump;
    }
    

    void Update()
    {
        _playerDirection = moveReference.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _playerRigidBody.linearVelocity = new Vector2(_playerDirection.x * playerMaximumMoveSpeed, _playerRigidBody.linearVelocity.y);

        if (_playerDirection.x > 0 && !_playerRightSide || _playerDirection.x < 0 && _playerRightSide) TurnPlayer();
    }

    private void TurnPlayer()
    {
        _playerRightSide = !_playerRightSide;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        _playerRigidBody.linearVelocity = new Vector2(_playerDirection.x, _playerRigidBody.linearVelocity.y * jumpForce);
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        Debug.Log("Interact");
    }
}
