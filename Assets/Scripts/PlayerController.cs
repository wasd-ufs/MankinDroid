using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum JumpState
{
    Floating,
    Grounded
}

public class PlayerController : MonoBehaviour
{   
    
    [SerializeField] private float horizontalSpeed = 4;
    
    [SerializeField] private float jumpForce = 5;
    [ReadOnly] [SerializeField] private JumpState _jumpState = JumpState.Grounded;
    private Rigidbody2D _rigidbody;
    
    
    private bool _rightSide = false;
    private Vector2 _playerDirection;
    
    [Header("Droid or Human")]
    [SerializeField] private string _inputMap; //Droid ou Human
    
    [Header("InputSystem_Actions")]
    [SerializeField] private InputActionAsset _inputActionAsset;
    private InputAction _moveInput;
    private InputAction _jumpInput;
    
    private Animator _animator;
    private string _currentAnimation = "";
    
    private void OnEnable()
    {
        _inputActionAsset.FindActionMap(_inputMap).Enable();
    }
    
    private void OnDisable()
    {
        _inputActionAsset.FindActionMap(_inputMap).Disable();
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveInput = _inputActionAsset.FindAction(_inputMap+"/Move");
        _jumpInput = _inputActionAsset.FindAction(_inputMap+"/Jump");
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        ChangeAnimation("idle_boy");
    }

    void Update()
    {
        _playerDirection = _moveInput.ReadValue<Vector2>();
        _rigidbody.linearVelocity = new Vector2(
            _playerDirection.x * horizontalSpeed, 
            _rigidbody.linearVelocity.y
            );
        
        if (_jumpState == JumpState.Grounded)
            ChangeAnimation(Mathf.Abs(_rigidbody.linearVelocity.x) > 0 ? "walking_boy" : "idle_boy");

        if (_jumpInput.WasPressedThisFrame() && _jumpState == JumpState.Grounded)
            Jump();
        
        if (_playerDirection.x < 0 && !_rightSide || _playerDirection.x > 0 && _rightSide) 
            TurnPlayer();
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _jumpState = JumpState.Grounded;
            ChangeAnimation("idle_boy");
        }
    }
    
    private void TurnPlayer()
    {
        _rightSide = !_rightSide;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Jump()
    {   
        _jumpState = JumpState.Floating;
        ChangeAnimation("jumping_boy");
        _rigidbody.AddForce(new Vector2(0f, jumpForce));
    }

    public string GetInputMapName() => _inputMap;

    private void ChangeAnimation(string animationName, float crossfade = 0.2f)
    {   
        if (_currentAnimation == animationName)
            return;
        
        _currentAnimation = animationName;
        _animator.CrossFade(animationName,crossfade);
    }
}
