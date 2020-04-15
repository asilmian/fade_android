using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***
 * Camera controls with smoothing
 * higher the smooth the quicker the transition to newPosition
 * small smooth leads to camera lagging behind
 */
public class CameraLerp : MonoBehaviour
{

    private float turnSpeed = 3.0f;
    public Transform player;
    public float smooth = 8.5f;
    private Vector3 offset;
    private Vector3 oldPosition;

    void Start()
    {
        //distance of camera from player
        offset = this.transform.position - player.transform.position;
        oldPosition = this.transform.position;
    }

    void LateUpdate()
    {
        //sets camera position relative to the player
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        //store previous position needed for collision detection
        oldPosition = transform.position;
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

        //smooth out the transition between newposition and oldposition
        this.transform.position = Vector3.Lerp(this.oldPosition, this.transform.position, Time.deltaTime * smooth);
    }

}