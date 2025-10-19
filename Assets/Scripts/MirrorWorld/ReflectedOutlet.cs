using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ReflectedOutlet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer pluggedOutlet;
    [SerializeField] private SpriteRenderer unpluggedOutlet;
    
    [SerializeField] private OutletInteractable outletInteractable;
    private bool _isPlugged = true;
    
    private void Awake()
    {
        outletInteractable.onUnplug.AddListener(ChangeSprite);
    }

    private void ChangeSprite()
    {
        if (_isPlugged)
        {
            pluggedOutlet.enabled = false;
            unpluggedOutlet.enabled = true;
            _isPlugged = true;
        } 
    }
}
