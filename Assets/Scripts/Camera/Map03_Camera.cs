using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map03_Camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float angle_x, angle_y;
    private float X_CAMERA = 0, Y_CAMERA = 60, Z_CAMERA = -60;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x + X_CAMERA, player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA);
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate ()
    {
        if (player.transform.position.z >= 360 && player.transform.position.z <= 590 && player.transform.position.x > 1100)
        {
            angle_x = (590 - player.transform.position.z) / (230f / 45f);
            transform.rotation = Quaternion.Euler(angle_x + 10 - (angle_x / 45f * 10f), 0, 0);

            // Base Height - ( Max angle - current angle ) / Weight(speed & min height)
            offset.y = Y_CAMERA - (45 - angle_x) / 2;
            offset.z = Z_CAMERA - (45 - angle_x) / 2;
        }
        else if (player.transform.position.z >= 820 && player.transform.position.z <= 940 && player.transform.position.x <= 1060)
        {
            angle_y = ((940 - player.transform.position.z) / (120f / 90f)) - 90;
            transform.rotation = Quaternion.Euler(10, angle_y, 0);

            offset.x = X_CAMERA + (0 - angle_y);
            offset.z = Z_CAMERA - ((45 - angle_x) / 2) + (0 - angle_y) + angle_y/15;
        }
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
