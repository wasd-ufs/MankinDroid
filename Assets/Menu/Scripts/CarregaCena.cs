using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregaCena : MonoBehaviour
{
    /*
     * Responsavel por carregar uma cena especifica
     */
    public static void CarregarCena(int numeroCena)
    {
        SceneManager.LoadScene(numeroCena);
    }
}
