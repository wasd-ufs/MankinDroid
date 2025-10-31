using UnityEngine;

public class ExecutarAudioBotao : MonoBehaviour
{
  [SerializeField] private SettingBaseAudio _settingAudioPress;
  [SerializeField] private SettingBaseAudio _settingAudioHight;
  public void PlayAudioBtnHight()
  {
    if (_settingAudioHight != null)
    {
      AudioManager.instance.PlayAudio(_settingAudioHight.Clip,_settingAudioHight.Volume,_settingAudioHight.Pitch);
      return;
    }
    
    Debug.Log("Nao tem audio ._. hight");
  }

  public void PlayAudioBtnPress()
  {
    if (_settingAudioPress != null)
    {
      AudioManager.instance.PlayAudio(_settingAudioPress.Clip,_settingAudioPress.Volume,_settingAudioPress.Pitch);
      return;
    }
    
    Debug.Log("Nao tem audio ._. press");
  }
}
