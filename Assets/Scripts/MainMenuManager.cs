using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject levelList;
    public void ShowLevelList()
    {
        levelList.SetActive(true);
    }

    public void HideLevelList()
    {
        levelList.SetActive(false);
    }
    
    public void LoadLevel(int levelNumber)
    {
        if(levelNumber < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(levelNumber);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
