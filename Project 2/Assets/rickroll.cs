using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rickroll : MonoBehaviour {

    public AudioSource audio;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            audio.Play();
        }
    }


}
