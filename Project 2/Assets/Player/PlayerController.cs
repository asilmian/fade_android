/* Inspired by the Roll-A-Ball tutorial on Unity's website:
 * https://unity3d.com/learn/tutorials/projects/roll-ball-tutorial/moving-player?playlist=17141
 * written on 30.09.2018 by Karoline Bernacki, Asil Mian and Usama Ahmed
 * for COMP30019: Graphics and Interaction, project 2
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public Camera camera;

    //shader and lightsource pointers
    public DissolveScript shaderScript;
    public PointLight lightsource;


    // Speed of the player
    private float speed = 7.5f;
    private float ground = 0.85f;

    // Player is a rigid body
    private Rigidbody body;

    // Deciding movement of the ball
    private Vector3 movement;

    //Control booleans
    private bool canMove = false;

    private void Start()
    {
        //controller = GetComponent<CharacterController>();
        body = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkShader();
        movePlayer();
    }


    //runs shader in revese when player ends level. Also stop updating light and stop player from moving
    public void startFinishRoutine()
    {
        this.canMove = false;
        this.lightsource.setCanFade(false);
        this.shaderScript.setReverse(true);
        this.shaderScript.setRun(true);
        this.body.velocity = Vector3.zero;
    }

    private void movePlayer()
    {
        if (canMove)
        {
            // Ball rolls with movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");


            // make sure player moves on the ground
            if (this.transform.position.y <= ground)
            {
               
              
                Vector3 targetDirection = new Vector3(moveHorizontal, 0f, moveVertical);

                //sets movement vectors relative to the camera
                targetDirection = Camera.main.transform.TransformDirection(targetDirection);
                targetDirection.y = 0.0f;
                movement = targetDirection;

                body.AddForce(movement * speed);

            }
        }

        //if canMove is false, dont let player move
        else
        {
            this.body.velocity = Vector3.zero;
        }
    }


    //checks if dissolve shader has completed its animation
    private void checkShader()
    {

        //go to UI if player completed level
        if (shaderScript.getReverse() && !shaderScript.getRun())
        {
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                SceneManager.LoadScene("Win");
            }
            else
            {
                SceneManager.LoadScene("Open");
            }
        }

        else if (!shaderScript.getRun())
        {
            this.canMove = true;
            lightsource.setCanFade(true);
        }
    }

}
