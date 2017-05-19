using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map05_Camera : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;
    private float angle_x, angle_y;
    private float X_CAMERA = 0, Y_CAMERA = 40, Z_CAMERA = -40;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x + X_CAMERA, player.transform.position.y + Y_CAMERA, player.transform.position.z + Z_CAMERA);
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate ()
    {
        if ( player.transform.position.z <= 230)
        {
            offset.z = 190 - player.transform.position.z;
        }

        if (player.transform.position.z >= 550 && player.transform.position.z <= 660 && player.transform.position.y < 15)
        {
            angle_x = (660 - player.transform.position.z) / (110f / 25f);
            transform.rotation = Quaternion.Euler(angle_x + 45, 0, 0);

            // Base Height - ( Max angle - current angle ) / Weight(speed & min height)
            offset.y = Y_CAMERA + angle_x;
            offset.z = Z_CAMERA + angle_x;
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
