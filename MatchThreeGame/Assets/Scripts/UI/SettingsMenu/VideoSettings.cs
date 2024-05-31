using UnityEngine;
using NaughtyAttributes;
public class VideoSettings : MonoBehaviour
{
    [SerializeField] private GameObject windowState;

    // Start is called before the first frame update
    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            windowState.SetActive(true);
            WindowStateChanged(PlayerPrefs.GetInt("windowState"));
        }

        ResolutionChanged(PlayerPrefs.GetInt("resolution"));
    }

    public void WindowStateChanged(int value)
    {
        Screen.fullScreenMode = value switch
        {
            0 => // Borderless
                FullScreenMode.FullScreenWindow,
            1 => // Windowed
                FullScreenMode.Windowed,
            2 => // Fullscreen
                FullScreenMode.ExclusiveFullScreen,
            _ => Screen.fullScreenMode
        };
        PlayerPrefs.SetInt("windowState", value); // Save the value to the player prefs
    }

    public void ResolutionChanged(int value)
    {
        // We need to check if the window state is fullscreen, as we need to pass that to the Screen.SetResolution method
        bool fullscreen = PlayerPrefs.GetInt("windowState", 0) == 2;
        PlayerPrefs.SetInt("resolution", value);
        switch (value)
        {
            case 0: // 2560x1440
                Screen.SetResolution(2560, 1440, fullscreen);
                break;
            case 1: // 1920x1080
                Screen.SetResolution(1920, 1080, fullscreen);
                break;
            case 2: // 1280x720
                Screen.SetResolution(1280, 720, fullscreen);
                break;
        }
    }
}
