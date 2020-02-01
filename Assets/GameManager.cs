using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelTime = 20f;
    public GameObject winLevelCanvas;
    public GameObject loseLevelCanvas;
    public GameObject pauseMenuCanvas;
    public Inventory inventory;

    private List<Fish> fishAlive;
    public List<Fish> meatEatingFishAlive;
    public List<Fish> plantEatingFishAlive;
    private int defaultFishCount;

    private void Awake()//execute before start
    {
        fishAlive = new List<Fish>(FindObjectsOfType<Fish>());
        defaultFishCount = fishAlive.Count;
    }

    public void MakeAllAggressiveFishToSearchForNewTarget()
    {
        for (int i = 0; i < meatEatingFishAlive.Count; i++)
        {
            meatEatingFishAlive[i].ChooseTargetToEat(0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }


    private IEnumerator FishAliveWatcher()
    {
        if(fishAlive.Count < defaultFishCount)
        {
            LoseLevel();
        }
        yield return new WaitForSeconds(1f);
    }

    public void WinLevel()
    {
        print("WinLevelMethod");
        Time.timeScale = 0f;
        HideSelectedObject();
        winLevelCanvas.SetActive(true);
    }

    public void LoseLevel()
    {
        Time.timeScale = 0f;
        HideSelectedObject();
        loseLevelCanvas.SetActive(true);
    }

    public void PauseMenu()
    {
        if(pauseMenuCanvas.activeInHierarchy)
        {
            Time.timeScale = 1f;
            pauseMenuCanvas.SetActive(false);
            ShowSelectedObject();


        }
        else
        {
            Time.timeScale = 0f;
            HideSelectedObject();
            pauseMenuCanvas.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        var activeScene = SceneManager.GetActiveScene();
        Time.timeScale = 1f;
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    private void HideSelectedObject()
    {
        if (inventory.objToSpawn != null)
        {
            inventory.objToSpawn.SetActive(false);
        }
    }

    private void ShowSelectedObject()
    {
        if (inventory.objToSpawn != null)
        {
            inventory.objToSpawn.SetActive(true);
        }
    }

}
