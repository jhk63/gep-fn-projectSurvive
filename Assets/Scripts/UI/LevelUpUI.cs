using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class LevelUpUI : MonoBehaviour
{
    public GameObject LevelUpPanel;
    public Button[] buttons;
    public Text[] buttontexts;

    public enum StatType
    {
        Speed,
        Slash_Damage,
        Bullet_Damage,
        Slash_Term,
        Fire_Term,
        Slash_Range,
    }
    StatType statType;
    StatType[] randomStats = new StatType[3];

    // Start is called before the first frame update
    void Start()
    {
        LevelUpPanel.SetActive(false);

        buttons[0].onClick.AddListener(() => SelectStat(0));
        buttons[1].onClick.AddListener(() => SelectStat(1));
        buttons[2].onClick.AddListener(() => SelectStat(2));
    }

    public void ShowLevelUpUI()
    {
        LevelUpPanel.SetActive(true);

        List<StatType> statList = new List<StatType>((StatType[])System.Enum.GetValues(typeof(StatType)));

        for (int i = 0; i < randomStats.Length; i++)
        {
            int randomIndex = Random.Range(0, statList.Count);
            randomStats[i] = statList[randomIndex];
            statList.RemoveAt(randomIndex);
        }

        for (int i = 0; i < randomStats.Length; i++)
        {
            buttontexts[i].text = randomStats[i].ToString();
        }

    }

    void SelectStat(int statIndex)
    {
        GameManager.instance.player.IncreaseStat(randomStats[statIndex]);
        // Debug.Log(randomStats[statIndex]);

        LevelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
