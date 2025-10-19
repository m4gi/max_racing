using MoreMountains.HighroadEngine;
using MoreMountains.Tools;
using UnityEngine;

public class SoundHelperI : MMPersistentHumbleSingleton<SoundHelperI>
{
    [SerializeField]
    private AudioSource audioSource;
    
    public AudioClip buttonAudioSource;
    
    public void PlayButtonClick()
    {
        if (LocalDataPlayer.Instance.GetSound)
        {
            audioSource.PlayOneShot(buttonAudioSource);
        }
    }
}
