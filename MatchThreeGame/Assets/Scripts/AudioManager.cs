using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _Instance;

    [SerializeField]
    private bool dontDestroyOnLoad;

    [SerializeField]
    private AudioSource menuAudioSource;

    [SerializeField]
    private AudioSource soundFXAudioSource;

    [SerializeField]
    private AudioSource musicAudioSource;

    [SerializeField]
    private AudioClip[] menuAudioClips;

    [SerializeField]
    private AudioClip[] gameAudioClips;

    [SerializeField]
    private AudioClip[] musicAudioClips;

    private void Awake()
    {
        // If true, allow for scene loading
        if (dontDestroyOnLoad)
        {
            AudioManager[] objs = FindObjectsOfType<AudioManager>();
            if (objs.Length > 1)
                if (this)
                    Destroy(gameObject);

            DontDestroyOnLoad(this.gameObject);
        }

        if (_Instance != null && _Instance != this)
            Destroy(this.gameObject);
        else if (_Instance == null)
            _Instance = this;
    }

    public void MenuButtonPressed()
    {
        menuAudioSource.PlayOneShot(menuAudioClips[0]);
    }

    public void PlayGameAudioClip(int clipToPlay)
    {
        soundFXAudioSource.PlayOneShot(gameAudioClips[clipToPlay]);
    }

}
