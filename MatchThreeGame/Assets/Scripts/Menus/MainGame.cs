using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseButtonPressed(true);
    }
    public void PauseButtonPressed(bool wasKeyPress)
    {
        if (!wasKeyPress) AudioManager._Instance.MenuButtonPressed();
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void SettingsButtonPressed(string sceneName)
    {
        AudioManager._Instance.MenuButtonPressed();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void QuitButtonPressed(string sceneName)
    {
        Time.timeScale = 1;
        AudioManager._Instance.MenuButtonPressed();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }
}
