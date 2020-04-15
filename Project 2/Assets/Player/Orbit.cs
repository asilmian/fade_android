using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***
 * Camera controls
 */
public class Orbit : MonoBehaviour
{

    private float turnSpeed = 3.0f;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        //distance of camera from player
        offset = this.transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        //sets camera position relative to the player
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
        this.occuludeRay();
    }


    //camera collision detection
    private void occuludeRay()
    {
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(player.position, this.transform.position, out wallHit))
        {
            //move camera to point of collison
            this.transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, this.transform.position.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
    }

}