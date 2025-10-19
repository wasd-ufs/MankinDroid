using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class OutletInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer pluggedOutlet;
    [SerializeField] private SpriteRenderer unpluggedOutlet;
    
    private bool _isPlugged = true;
    [HideInInspector] public UnityEvent onUnplug;

    public void Interact()
    {
        if (_isPlugged)
        {
            pluggedOutlet.enabled = false;
            unpluggedOutlet.enabled = true;
            _isPlugged = true;
            
            onUnplug.Invoke();
        } 
    }
}
 