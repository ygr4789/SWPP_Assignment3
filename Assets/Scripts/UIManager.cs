using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI stageValueText;
    public TextMeshProUGUI lifeValueText;
    public TextMeshProUGUI moneyValueText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameClearText;
    public Button startButton;
    public Button pauseButton;
    public Button restartButton;
    public Button upgradeButton;
    
    void Start()
    {
        GameManager.Instance.uiManager = this;
        startButton.onClick.AddListener(GameManager.Instance.StageStart);
        pauseButton.onClick.AddListener(GameManager.Instance.GamePause);
        restartButton.onClick.AddListener(GameManager.Instance.GameRestart);
        upgradeButton.onClick.AddListener(GameManager.Instance.UpgradePlayer);
    }

    public void UpdateStage(int stage)
    {
        stageValueText.text = stage.ToString();
    }

    public void UpdateLife(int life)
    {
        lifeValueText.text = life.ToString();
    }

    public void UpdateMoney(int money)
    {
        moneyValueText.text = money.ToString();
    }

    public void DisableStartButton()
    {
        startButton.interactable = false;
        startButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
    }

    public void EnableStartButton()
    {
        startButton.interactable = true;
        startButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }

    public void SetUpgradeActive(bool active)
    {
        upgradeButton.gameObject.SetActive(active);
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void ShowGameClear()
    {
        gameClearText.gameObject.SetActive(true);
    }
}
