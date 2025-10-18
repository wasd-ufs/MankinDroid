using System.Collections;
using UnityEngine;

public class GerenciadorMenu : MonoBehaviour
{
    
    /*
     * Responsavel por iniciar o jogo
     */
    public void PlayGame(int numeroCena)
    {
        IniciaAnimacaoTransicaoCena.IniciarTransicao("Start", numeroCena);
    }

    public void Configuracoes()
    {
        //Chama a tela de configuracoes do jogo
    }
    
    
    /*
     * Responsavel por sair do jogo
     */
    public void QuitGame()
    {
		Application.Quit();   
    }
}
