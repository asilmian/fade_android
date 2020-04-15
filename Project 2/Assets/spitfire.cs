using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spitfire : MonoBehaviour {

    public ParticleSystem fire;
    public Collider boxcollider;
    public AudioSource audio;
    private float timer;
    private bool isActive = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            if (fire.isPlaying)
            {
                fire.Stop();
                if (audio != null)
                {
                    audio.Stop();
                }
                isActive = false;
            }

            else
            {
                fire.Play();
                if(audio != null)
                {
                    audio.Play();
                }
                isActive = true;
            }

            timer = 0.0f;
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && this.isActive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
