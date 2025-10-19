using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Necessário para Light2D

[RequireComponent(typeof(Light2D))]
[RequireComponent(typeof(Collider2D))]
public class BlinkingLamp : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private float blinkInterval = 0.8f;   // tempo entre piscadas
    [SerializeField] private Color lightColor = Color.yellow; // cor da luz
    [SerializeField] private float lightIntensity = 1.2f;  // intensidade máxima
    [SerializeField] private float fadeSpeed = 4f;         // velocidade do fade (para suavizar)

    private Light2D _light2D;
    private Collider2D _collider2D;
    private bool _isOn = true;

    private void Awake()
    {
        _light2D = GetComponent<Light2D>();
        _collider2D = GetComponent<Collider2D>();

        _light2D.color = lightColor;
        _light2D.intensity = lightIntensity;
        _light2D.lightType = Light2D.LightType.Point; // luz pontual (não afeta o cenário inteiro)
        _collider2D.isTrigger = true;
    }

    private void Start()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkInterval);
            _isOn = !_isOn;
        }
    }

    private void Update()
    {
        // Faz um fade suave da luz
        float targetIntensity = _isOn ? lightIntensity : 0f;
        _light2D.intensity = Mathf.Lerp(_light2D.intensity, targetIntensity, Time.deltaTime * fadeSpeed);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Só mata se a luz estiver acesa o suficiente
        if (_light2D.intensity > lightIntensity * 0.7f && other.CompareTag("Human"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
