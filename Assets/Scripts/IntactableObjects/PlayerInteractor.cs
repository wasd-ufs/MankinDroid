using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{   
    [SerializeField] private InputActionAsset _inputAction;
    
    [ReadOnly] [SerializeField] private InputAction _interactAction;
    [SerializeField] private float _searchRadius;
    
    private string _inputMap; //Droid ou Human
    
    
    void Awake()
    {
        var controller = GetComponent<PlayerController>();
        _inputMap = controller.GetInputMapName();
        _interactAction = _inputAction.FindAction(_inputMap+"/Interact");
    }
    
    private void OnEnable()
    {
        _inputAction.FindActionMap(_inputMap).Enable();
    }
    
    private void OnDisable()
    {
        _inputAction.FindActionMap(_inputMap).Disable();
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

    public void PrintHello()
    {
        Debug.Log("Hello");
    }
    
}
