using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool pause = false;

    [SerializeField] private GameObject settingMenu;

    public void Pause()
    {
        pause = true;
        if (pauseMenu)
        {
            pauseMenu.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pause = false;
        if (pauseMenu && settingMenu) 
        {
            settingMenu.SetActive(false);
            pauseMenu.SetActive(false);
        }
        Time.timeScale = 1.0f;
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public bool GetPause()
    {
        return pause;
    }
}
