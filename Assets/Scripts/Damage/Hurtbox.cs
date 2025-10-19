using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Collider2D _trigger;
    [SerializeField] public GameObject Owner { get; private set; }
    [SerializeField] private List<UnityEvent> _events;
    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
        Owner = gameObject;
    }

    public void TakeDamage()
    {
        foreach (UnityEvent e in _events)
        {
            e?.Invoke();
        }
    }
    
}
