using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle toggle;

    void Start()
    {
        if(PlayerPrefs.HasKey("SFXVolume")){
            LoadVolume();
        }
        else {
            SetMusicVolume();
            SetSFXVolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
    //turn off audiomxer
    public void ToggleAudio()
    {
        if(toggle.isOn)
        {
            audioMixer.SetFloat("Master", 0);
            PlayerPrefs.SetInt("Audio", 1);
        }
        else
        {
            audioMixer.SetFloat("Master", -80);
            PlayerPrefs.SetInt("Audio", 0);
        }
    }
}
