using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using NaughtyAttributes;
public class AudioSettings : MonoBehaviour
{
    [Header("Audio Initializations")]
    [SerializeField, Required] private AudioMixer audioMixer;
    [SerializeField, Required] private Slider masterVolumeSlider;
    [SerializeField, Required] private Slider musicVolumeSlider;
    [SerializeField, Required] private Slider soundFXVolumeSlider;

    // Start is called before the first frame update
    void Awake()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 20f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 20f);
        soundFXVolumeSlider.value = PlayerPrefs.GetFloat("soundFxVolume", 20f);
    }
    public void MasterVolumeChanged()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        if (Mathf.Log(masterVolumeSlider.value) * 20f == Mathf.NegativeInfinity)
            audioMixer.SetFloat("Master", -80f);
        else
            audioMixer.SetFloat("Master", Mathf.Log(masterVolumeSlider.value) * 20f);
    }
    public void MusicVolumeChanged()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        if (Mathf.Log(musicVolumeSlider.value) * 20f == Mathf.NegativeInfinity)
            audioMixer.SetFloat("Music", -80f);
        else
            audioMixer.SetFloat("Music", Mathf.Log(musicVolumeSlider.value) * 20f);
    }
    public void SoundFxVolumeChanged()
    {
        PlayerPrefs.SetFloat("soundFxVolume", soundFXVolumeSlider.value);
        if (Mathf.Log(soundFXVolumeSlider.value) * 20f == Mathf.NegativeInfinity)
            audioMixer.SetFloat("SoundFX", -80f);
        else
            audioMixer.SetFloat("SoundFX", Mathf.Log(soundFXVolumeSlider.value) * 20f);
    }
}
