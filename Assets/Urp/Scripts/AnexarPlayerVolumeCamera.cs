using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AnexarPlayerVolumeCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private UniversalAdditionalCameraData _camera;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _camera = Camera.main.GetUniversalAdditionalCameraData();
        AnexarPlayerToVolumeTrigger();
    }

    /**
     * Responsavel por adicionar o player como objeto para ser detectado no volume do urp
     */
    private void AnexarPlayerToVolumeTrigger()
    {
        _camera.volumeTrigger = _player;
    }
  
}
