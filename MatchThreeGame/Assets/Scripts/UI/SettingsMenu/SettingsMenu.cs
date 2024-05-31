using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Initializations")]
    [SerializeField] private string settingsMenuName;
    [SerializeField, Required] private GameObject videoControls;
    [SerializeField, Required] private Image videoButton;
    [SerializeField, Required] private GameObject audioControls;
    [SerializeField, Required] private Image audioButton;

    [Header("Color Schemes")]
    [SerializeField] private Color selectedButtonColor;
    [SerializeField] private Color deselectedButtonColor;

    // Start is called before the first frame update
    void Awake()
    {
#if !UNITY_EDITOR_WIN
        videoButton.gameObject.SetActive(Application.platform == RuntimePlatform.WindowsPlayer);
#endif
        audioControls.SetActive(true);
        audioButton.color = selectedButtonColor;

        videoButton.color = deselectedButtonColor;
        videoControls.SetActive(false);
    }

    public void BackButtonPressed()
    {
        AudioManager._Instance.MenuButtonPressed();
        SceneManager.UnloadSceneAsync(1);
    }

    public void SettingsButtonPressed(int buttonNumber)
    {
        AudioManager._Instance.MenuButtonPressed();
        switch (buttonNumber)
        {
            case 1: // Video settings
                videoButton.color = selectedButtonColor;
                audioButton.color = deselectedButtonColor;
                videoControls.SetActive(true);
                audioControls.SetActive(false);
                break;
            case 2: // Audio settings
                videoButton.color = deselectedButtonColor;
                audioButton.color = selectedButtonColor;
                videoControls.SetActive(false);
                audioControls.SetActive(true);
                break;
            default: // Error
                Debug.Log("Invalid button selected");
                break;
        }
    }
}
