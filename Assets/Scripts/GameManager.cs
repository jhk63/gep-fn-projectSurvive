using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("GameObject")]
    public PoolManager poolManager;
    public LevelUpUI levelUpUI;

    public Player player;
    public AttackInRange attackInRange;

    public GameObject GameOverPanel;

    [Header("GameTimer")]
    public float timer;
    public float maxTime = 2 * 10f;

    [Header("Level Design")]
    public int gameLevel = 0;
    public int playerLevel = 0;
    public int kill;
    public int exp;
    public int[] nextExp = {  };

    bool isLevelUp;

    private void Awake() 
    {
        instance = this;
        isLevelUp = false;

        GameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.instance.player.isDead)
        {
            // GameOverPanel.SetActive(true);
            return;
        }

        timer += Time.deltaTime;

        if (timer % 15 == 0)
        {
            gameLevel++;
        }

        // Game Pause
        if (isLevelUp)
        {
            Time.timeScale = 0;
            levelUpUI.ShowLevelUpUI();
            isLevelUp = false;
        }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     isLevelUp = false;
        //     Time.timeScale = 1;
        // }
    }

    public void IsKill()
    {
        kill++;
        exp ++;

        if (exp == nextExp[playerLevel])
        {
            playerLevel++;
            isLevelUp = true;
            exp = 0;
        }
    }
}
