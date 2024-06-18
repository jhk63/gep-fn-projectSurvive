using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{

    public void OnStartButton()
    {
        Time.timeScale = 1;
        GameManager.instance.GameStartPanel.SetActive(false);
    }
}
