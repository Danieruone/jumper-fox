using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        float playerPositionX = player.transform.position.x;
        transform.position = new Vector3(playerPositionX + 1.5f, 2.4f, -10);
    }
}