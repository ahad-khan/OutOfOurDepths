using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseMenuUI;
    public GameObject HUD;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(paused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
