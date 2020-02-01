using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    private GameManager gameManager;
    private float timeLeft;
    private float maxTime;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Text timeLeftText;
    [SerializeField]
    private Image timerImage;
    private bool winLevelShowed = false;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        timeLeft = gameManager.levelTime;
        maxTime = gameManager.levelTime;
    }

    private void Update()
    {
        Countdown();
    }

    public void SetMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    private void Countdown()
    {
        if (winLevelShowed == false && timeLeft <= 0)
        {
            winLevelShowed = true;
            gameManager.WinLevel();
            return;
        }
        timeLeft -= Time.deltaTime;
        SetTimeGUI();
    }

    private void SetTimeGUI()
    {
        timeLeftText.text = Mathf.Round(timeLeft).ToString();
        timerImage.fillAmount = (maxTime-timeLeft) / gameManager.levelTime;

    }
}
