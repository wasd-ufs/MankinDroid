using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public enum PickableState
{
    Dropped,
    Grabbed,
    Fixed
}

public enum AbleToGrab
{
    Droid,
    Human
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PickableObject : MonoBehaviour, IInteractable
{   
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Transform _target;

    [SerializeField] private AbleToGrab _whoCanInteract;
    [ReadOnly] [SerializeField] private PickableState _state = PickableState.Dropped;
    
    [Header("Configurações de Atração de Objetos")]
    [SerializeField] Vector3 _offset;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _pullSpeed;
    
    [Header("Configurações de Vibração")]
    [SerializeField] private float vibrationDuration = 0.5f;
    [SerializeField] private float vibrationIntensity = 0.1f;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Interact()
    {
        switch (_state)
        {
            case PickableState.Dropped:
                Grab();
                break;
            case PickableState.Grabbed:
                Drop();
                break;
            case PickableState.Fixed:
                Vibrate();
                break;
        }
    }

    void Grab()
    {
        GameObject player = GameObject.FindGameObjectWithTag(_whoCanInteract == AbleToGrab.Droid ? "Droid" : "Human");
        _target = player.GetComponent<Transform>();
        _state = PickableState.Grabbed;
        _rigidbody.simulated = false;
    }

    void Drop()
    {   
        _rigidbody.simulated = true;
        _state = PickableState.Dropped;
    }

    void Vibrate() =>
        StartCoroutine(VibrateCoroutine());

    private void FixedUpdate()
    {   
        
        if(_state == PickableState.Dropped)
            return;
        if(_state == PickableState.Fixed)
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
    
    private IEnumerator VibrateCoroutine()
    {   
        var originalPosition = transform.localPosition;
        
        float elapsedTime = 0f;

        while (elapsedTime < vibrationDuration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-vibrationIntensity, vibrationIntensity),
                Random.Range(-vibrationIntensity, vibrationIntensity),
                0
            );
            
            transform.localPosition = originalPosition + randomOffset;
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    public void LetFixed()
    {
        _state = PickableState.Fixed;
        _rigidbody.simulated = false;
    }
}
