using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ReflectObject : MonoBehaviour
{
    [FormerlySerializedAs("_offset")] [SerializeField] private Vector3 offset;
    [FormerlySerializedAs("_followedObject")] [SerializeField] private Transform followedObject;
    [FormerlySerializedAs("_pickableObject")] [SerializeField] private PickableObject pickableObject;
    [SerializeField] private float transparency = 0.5f;
    
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    
    private void Awake()
    {
        offset = transform.position - followedObject.transform.position;
        pickableObject.grabbedObject.AddListener(Grabbed);
        pickableObject.droppedObject.AddListener(Dropped);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    { 
        transform.position =  followedObject.position + offset;
        transform.rotation = followedObject.rotation;
    }

    private void Grabbed()
    {
        _collider.enabled = false;
        var color = _spriteRenderer.color;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, transparency);
    }

    private void Dropped()
    {
        _collider.enabled = true;
        Color color = _spriteRenderer.color;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, 1);
    }
}
