using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    Vector2 tilePos;
    Vector2 playerPos;

    float tileSize = 20.0f;

    void Update() 
    {
        tilePos = transform.position;
        playerPos = GameManager.instance.player.transform.position;

        float distX = playerPos.x - tilePos.x;
        float distY = playerPos.y - tilePos.y;

        // Tile Reposition
        if (distX > tileSize)
        {
            tilePos.x += tileSize * 2;
        }
        else if (distX < -tileSize)
        {
            tilePos.x -= tileSize * 2;
        }

        if (distY > tileSize)
        {
            tilePos.y += tileSize * 2;
        }
        else if (distY < -tileSize)
        {
            tilePos.y -= tileSize * 2;
        }

        transform.position = tilePos;
    }
    
}
