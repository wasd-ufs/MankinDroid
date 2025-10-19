using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Collider2D _trigger;
    [SerializeField] private GameObject _destroyEffect;
    [SerializeField] private float _destroyEffectDuration;
    private GameObject _owner;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
        _owner = gameObject;
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

    private void SpawnDestroyEffect()
    {
        if (_destroyEffect)
        {
            GameObject effect = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, _destroyEffectDuration);
        }
    }
}
