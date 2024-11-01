using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private bool isGameActive = true;
    private bool isPaused = false;
    
    [HideInInspector] public int stage = 0;
    [HideInInspector] public int life = 1;
    [HideInInspector] public int money = 0;

    private Vector3 spawnPosition = new Vector3(8.0f, 0, 0);
    private float spawnRate = 2.0f;
    private int upgradeCost = 3;
    private int totStage = 3;
    private int enemies = 0;
    
    [HideInInspector] public UIManager uiManager;
    [HideInInspector] public PlayerManager playerManager;

    public List<GameObject> enemyPfbs;
    public List<int> enemyCounts;
    
    private static GameManager instance = null;
    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance.enemyPfbs = enemyPfbs;
            instance.enemyCounts = enemyCounts;
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    
    void Start()
    {
    }

    public void StageStart()
    {
        if(!isGameActive) return;
        uiManager.DisableStartButton();
        enemies = enemyCounts[stage];
        StartCoroutine(SpawnEnemies());
    }

    public void StageComplete()
    {
        if(++stage >= totStage) GameClear();
        uiManager.UpdateStage(stage + 1);
        uiManager.EnableStartButton();
    }

    public void UpdateMoney(int earn)
    {
        money += earn;
        if(money >= upgradeCost && !playerManager.upgraded) uiManager.SetUpgradeActive(true);
        uiManager.UpdateMoney(money);
    }

    public void UpdateLife(int damage)
    {
        life -= damage;
        uiManager.UpdateLife(life);
        if(life <= 0) GameOver();
    }

    public void UpdateEnemy()
    {
        if(--enemies <= 0) StageComplete();
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        isGameActive = true;
        stage = 0;
        life = 1;
        money = 0;
        enemies = 0;
    }
    
    public void GamePause()
    {
        if (!isGameActive) return;
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        isGameActive = false;
        Time.timeScale = 0;
        uiManager.ShowGameOver();
    }

    public void GameClear()
    {
        isGameActive = false;
        Time.timeScale = 0;
        uiManager.ShowGameClear();
    }

    public void UpgradePlayer()
    {
        if (!isGameActive) return;
        playerManager.Upgrade();
        uiManager.SetUpgradeActive(false);
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCounts[stage]; i++)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(enemyPfbs[stage], spawnPosition, Quaternion.Euler(0, -90, 0));
        }
    }
}
