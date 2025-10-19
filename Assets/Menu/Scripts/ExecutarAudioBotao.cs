using UnityEngine;

public class ExecutarAudioBotao : MonoBehaviour
{
  [SerializeField] private AudioSource _audioSource;
  [SerializeField] private AudioClip _audioClipPress;
  [SerializeField] private AudioClip _audioClipHight;

  void Start()
  {
	  _audioSource = GetComponent<AudioSource>();
  }
  
  public void PlayAudioBtnHight()
  {
    if (_audioClipPress != null)
    {
      _audioSource.clip = _audioClipPress;
      _audioSource.Play();
      return;
    }
    
    Debug.Log("Nao tem audio ._. hight");
  }

  public void PlayAudioBtnPress()
  {
    if (_audioClipHight != null)
    {
      _audioSource.clip = _audioClipHight; 
      _audioSource.Play();
      return;
    }
    
    Debug.Log("Nao tem audio ._. press");
  }
}
