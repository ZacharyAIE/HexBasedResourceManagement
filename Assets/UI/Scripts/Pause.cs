using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject menu;
    public Canvas gameCanvas;

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void PauseGame()
    {
        menu.SetActive(true);
        gameCanvas.enabled = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        menu.SetActive(false);
        gameCanvas.enabled = true;
        Time.timeScale = 1;
    }
}
