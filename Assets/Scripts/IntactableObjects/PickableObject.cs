using System;
using UnityEngine;


public enum PickableState
{
    Dropped,
    Grabbed
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PickableObject : MonoBehaviour, IInteractable
{   
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Transform _target;
    [ReadOnly] [SerializeField] private PickableState _objectState = PickableState.Dropped;
    [SerializeField] Vector3 _offset;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _pullSpeed;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Interact()
    {
        if (_objectState == PickableState.Dropped)
            Grab();
        else
            Drop();
    }

    void Grab()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _target = player.GetComponent<Transform>();
        _objectState = PickableState.Grabbed;
        _rigidbody.simulated = false;
    }

    void Drop()
    {   
        _rigidbody.simulated = true;
        _objectState = PickableState.Dropped;
    }

    private void FixedUpdate()
    {   
        
        if(_objectState != PickableState.Grabbed)
            return;

        if (Vector3.Distance(transform.position, _target.position) > _stopDistance)
        {
            _transform.position = Vector3.MoveTowards(
                transform.position,
                _target.position + _offset,
                _pullSpeed * Time.deltaTime
            );
        }
    }
}
