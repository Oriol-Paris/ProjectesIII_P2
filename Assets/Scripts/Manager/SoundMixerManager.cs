using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;


    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }
}
