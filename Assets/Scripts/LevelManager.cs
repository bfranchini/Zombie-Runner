using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

    public float autoLoadNextLevelAfter;

    void Start()
    {
        if (autoLoadNextLevelAfter > 0)
            Invoke("LoadNextLevel", autoLoadNextLevelAfter);
        else
        {
            Debug.Log("Level auto load disabled, use a positive number in seconds");
        }
    }

    public void LoadNextLevel()
    {
        unpause();
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void LoadLevel(string name)
    {
        unpause();
        Application.LoadLevel(name);
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    private void unpause()
    {
        //Ensure game is not paused when loading a new level 
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }
}
