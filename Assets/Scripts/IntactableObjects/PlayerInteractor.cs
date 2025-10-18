using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{   
    [SerializeField] private InputActionAsset _inputAction;
    
    [ReadOnly] [SerializeField] private InputAction _interactAction;
    [SerializeField] private float _searchRadius;
    
    void Awake()
    {
        _interactAction = _inputAction.FindAction("Interact");
    }
    
    private void OnEnable()
    {
        _inputAction.FindActionMap("Player").Enable();
    }
    
    private void OnDisable()
    {
        _inputAction.FindActionMap("Player").Disable();
    }
    
    private void Update()
    {
        if (_interactAction.WasPressedThisFrame())
        {
            IInteractable interactable = FindNearestObject<IInteractable>();
            interactable?.Interact();
        }
    }

    public T FindNearestObject<T>() where T : IInteractable
    {
        var objectsOfType = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<T>().ToList();
        T nearestObject = default(T);
        float nearestDistance = Mathf.Infinity;

        foreach (T item in objectsOfType)
        {
            var obj = item as MonoBehaviour;
            
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < _searchRadius && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = item;
            }
        }
        
        return nearestObject;
    }
    
}
