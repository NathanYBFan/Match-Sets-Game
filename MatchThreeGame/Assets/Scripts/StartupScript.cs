using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;

public class StartupScript : MonoBehaviour
{
    [SerializeField, Required] private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("StartupSetup", 0) == 0)
        {
            PlayerPrefs.SetFloat("masterVolume", 1f);
            PlayerPrefs.SetFloat("musicVolume", 1f);
            PlayerPrefs.SetFloat("soundFxVolume", 1f);
            PlayerPrefs.SetInt("StartupSetup", 1);
        }

        // Master Volume
        if (Mathf.Log(PlayerPrefs.GetFloat("masterVolume", 1f)) * 20f == Mathf.NegativeInfinity)
            audioMixer.SetFloat("Master", -80f);
        else
            audioMixer.SetFloat("Master", Mathf.Log(PlayerPrefs.GetFloat("masterVolume", 1f)) * 20f);

        // Music Volume
        if (Mathf.Log(PlayerPrefs.GetFloat("musicVolume", 1f)) * 20f == Mathf.NegativeInfinity)
            audioMixer.SetFloat("Music", -80f);
        else
            audioMixer.SetFloat("Music", Mathf.Log(PlayerPrefs.GetFloat("musicVolume", 1f)) * 20f);

        // Sound FX Volume
        if (Mathf.Log(PlayerPrefs.GetFloat("soundFxVolume", 1f)) * 20f == Mathf.NegativeInfinity)
            audioMixer.SetFloat("SoundFX", -80f);
        else
            audioMixer.SetFloat("SoundFX", Mathf.Log(PlayerPrefs.GetFloat("soundFxVolume", 1f)) * 20f);
    }
}
