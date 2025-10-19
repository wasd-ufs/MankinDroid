using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;
    private Transform _transform;
    private Hitbox _hitbox;
    
    void Start()
    {   
        _transform = GetComponent<Transform>();
        _hitbox = GetComponent<Hitbox>();
        _direction = _direction.normalized;
    }
    
    void Update()
    {
        transform.Translate(_direction.normalized * (_speed * Time.deltaTime), Space.World);
    }
}
