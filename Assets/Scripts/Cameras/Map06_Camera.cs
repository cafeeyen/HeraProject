using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map06_Camera : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    private float angle_x, angle_y;
    private float X_CAMERA = 0, Y_CAMERA = 80, Z_CAMERA = -50;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x + X_CAMERA, player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA);
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
/*
 * 
 * (550 - z) / (230 / 45)
 * 550 -> Max, 320 -> Min,  230 -> Max - Min, 45 -> Base Angle
 * 
 * angle + 10 - (angle / 45 * 10)
 * need lowest angle to 10 -> angle + 10 - (angle / Max angle * 10)
 * 
*/
