using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float levelTime = 20f;

    private List<Fish> fishAlive;
    private int defaultFishCount;
    private void Awake()
    {
        fishAlive = new List<Fish>(FindObjectsOfType<Fish>());
        defaultFishCount = fishAlive.Count;
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
        //ekran wygranej
    }

    public void LoseLevel()
    {
        //ekran przegranej
    }
}
