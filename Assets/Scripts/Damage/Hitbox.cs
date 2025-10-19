using System;
using UnityEngine;

public enum State
{
    started,
    stopped
}

public class Hitbox : MonoBehaviour
{
    [SerializeField] private bool _spawnAnimations;
    [SerializeField] private float _animationDuration;
    [SerializeField] private GameObject _animationPrefab;
    [SerializeField] private float _timeLoop;
    [ReadOnly] [SerializeField] private float _elapsedTime;
    [ReadOnly] [SerializeField] private State _state;
    
    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private float _destroyEffectDuration;
    
    private Collider2D _trigger;
    private GameObject _owner;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
        _owner = gameObject;
        _state = State.stopped;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hurtbox"))
        {
            var hurtbox = other.GetComponent<Hurtbox>();
            hurtbox?.TakeDamage();
            SpawnDestroyEffect();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_state == State.stopped  || !_spawnAnimations)
            return;
        
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _timeLoop)
        {   
            var anim = Instantiate(_animationPrefab, transform.position, Quaternion.identity);
            Destroy(anim, _animationDuration);
            _elapsedTime = 0;
        }
        
    }

    private void SpawnDestroyEffect()
    {
        if (_destroyEffect)
        {
            GameObject effect = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, _destroyEffectDuration);
        }
    }

    public void StartAnimation()
    {
        _state = State.started;
    }

    public void StopAnimation()
    {
        _state = State.stopped;
    }
    
}
