using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUB : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Hp}
    public InfoType type;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate() 
    {
        switch (type)
        {
            case InfoType.Exp:
                float exp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.playerLevel];
                mySlider.value = exp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("{0:D2}", GameManager.instance.gameLevel);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:D2}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float time = GameManager.instance.timer;
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Hp:
                // float hp = 3;
                float hp = GameManager.instance.player.hp;
                float maxHp = GameManager.instance.player.maxHp;
                for (int i = 0; i < hearts.Length; i++)
                {
                    if (i < hp)
                        hearts[i].sprite = fullHeart;
                    else
                        hearts[i].sprite = emptyHeart;

                    if(i < maxHp)
                        hearts[i].enabled = true;
                    else
                        hearts[i].enabled = false;
                }
                break;
        }    
    }
}
