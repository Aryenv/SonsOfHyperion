using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : TemporalSingleton<Scenes>
{
    public void LoadScene(int num)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(num);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
