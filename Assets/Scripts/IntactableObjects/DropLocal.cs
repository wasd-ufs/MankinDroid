using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum DropLocalState
{
    Unlocked,
    Locked
} 

public class DropLocal : MonoBehaviour
{   
    [ReadOnly] [SerializeField] private DropLocalState _state = DropLocalState.Unlocked;
    [SerializeField] private GameObject _finalGameObject;
    [SerializeField] private float _moveSpeed;
    private Vector3 _localPosition;
    private Collider2D _area;

    private void Awake()
    {
        _area = GetComponent<Collider2D>();
        _area.isTrigger = true;
        _localPosition = transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(_state == DropLocalState.Locked)
            return;
        
        if (other.gameObject == _finalGameObject)
        {
            _state = DropLocalState.Locked;
            var pickableObject = other.gameObject.GetComponent<PickableObject>();
            if (pickableObject == null)
            {
                Debug.LogWarning("PickableObject não encontrado em " + other.gameObject.name);
                return;
            }
            pickableObject.LetFixed();
            StartCoroutine(MoveToPositionSmoothly(other.gameObject.transform));
            
        }
    }
    
    private IEnumerator MoveToPositionSmoothly(Transform objectTransform)
    {   
        var targetPosition = _localPosition;
        //Vector3 targetPosition = transform.TransformPoint(_localPosition); // Converte _localPosition para posição global
        while (Vector3.Distance(objectTransform.position, targetPosition) > 0.01f)
        {
            objectTransform.position = Vector3.MoveTowards(
                objectTransform.position,
                targetPosition,
                _moveSpeed * Time.deltaTime
            );
            yield return null;
        }
        objectTransform.position = targetPosition; // Garante a posição exata
    }
}
