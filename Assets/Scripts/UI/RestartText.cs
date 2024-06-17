using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartText : MonoBehaviour
{
    Text restartText;

    bool isOn;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        restartText = GetComponent<Text>();
        isOn = true;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        timer += Time.deltaTime;

        if (timer <= 1.0f)
            return;

        if (isOn)
        {
            restartText.text = string.Format("Press R Button To Restart");
            timer = 0f;
            isOn = false;
        } else
        {
            restartText.text = string.Format("");
            timer = 0f;
            isOn = true;
        }
    }
}
