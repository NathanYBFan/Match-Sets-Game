using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayButtonPressed(string sceneName)
    {
        AudioManager._Instance.MenuButtonPressed();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void SettingsButtonPressed(string sceneName)
    {
        AudioManager._Instance.MenuButtonPressed();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void QuitButtonPressed()
    {
        AudioManager._Instance.MenuButtonPressed();
#if UNITY_EDITOR


        UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
    }
}
