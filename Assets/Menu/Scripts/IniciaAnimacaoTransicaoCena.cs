using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class IniciaAnimacaoTransicaoCena : MonoBehaviour
{
    public static IniciaAnimacaoTransicaoCena Instancia { get; private set; }
    [SerializeField] private Animator _animacaoTransicao;
    [SerializeField] private float _tempoTransicao; 
    
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instancia = this;
        }
    }
    
    /**
     * Responsavel por iniciar uma animacao de transicao de cena
     */
    public static void IniciarTransicao(string nomeAnimacao, int numeroCena)
    {
        if (Instancia != null)
        {
            Instancia.StartCoroutine(Instancia.Transition(nomeAnimacao, numeroCena));
        }
        else
        {
            Debug.LogError("IniciaAnimacaoTransicaoCena não está na cena. Não foi possível iniciar a transição.");
        }
    }
    
    /**
     * Corrotina para executar a animacao e atrasar o carregamento da fase
     */
    private IEnumerator Transition(string nomeAnimacao, int numeroCena)
    {
        if (_animacaoTransicao != null)
        {
            _animacaoTransicao.SetTrigger(nomeAnimacao);
        }
        else
        {
            Debug.LogError("Animator de transição não atribuído!");
        }
        
        yield return new WaitForSeconds(_tempoTransicao);
        CarregaCena.CarregarCena(numeroCena);
    }
}