using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    float timer;
    float delay;

    int level;

    void Start() 
    {
        spawnPoints = GetComponentsInChildren<Transform>();

        timer = 1.0f;
        delay = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player.isDead)
            return;

        timer += Time.deltaTime;
        level = GameManager.instance.gameLevel;

        delay = ChangeDelay(level);

        if (timer > delay)
        {
            Spawn();
            timer = 0f;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }

    float ChangeDelay(float level)
    {
        float baseDelay = 2.0f;
        float minDelay = 0.5f;

        return Mathf.Max(minDelay, baseDelay - level * 0.5f);
    }
}
