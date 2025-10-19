using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ReflectObject : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _target;


    private void Update()
    {   
        var position = this.transform.position;
        var rotation = this.transform.rotation;
        _target.transform.position = position + _offset;
        _target.transform.rotation = rotation;
        
    }
}
