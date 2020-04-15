using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//start player finish routine
public class endLevel : MonoBehaviour {
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.startFinishRoutine();
        }
    }
}
