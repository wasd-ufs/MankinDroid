using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AutoVideoScene : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private VideoPlayer videoPlayer; // Arraste o componente VideoPlayer aqui
    [SerializeField] private string nextSceneName = "Level1"; // Nome da próxima cena

    private void Awake()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.playOnAwake = true; // Inicia automaticamente
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Quando o vídeo termina, carrega a próxima cena
        SceneManager.LoadScene(nextSceneName);
    }
}
