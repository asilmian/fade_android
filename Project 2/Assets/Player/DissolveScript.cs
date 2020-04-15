using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//controls updates to the dissolve shader
public class DissolveScript : MonoBehaviour {

    public Material material;
    public PointLight lightsource;
    public float speed, max, min;
    private bool run = true;

    //controls direction of dissolve shader
    private bool reverse = false;
    private float runMax = 50f;


    private float currY; 

	// Use this for initialization
	void Start () {
        currY = min;
	}
	
	// Update is called once per frame
	void Update () {
        if (run)
        {
            //increase currY and pass to shader
            if (currY < max && !reverse)
            {
                material.SetFloat("_DissolveY", currY);
                material.SetFloat("_Intensity", this.lightsource.GetRange() * 0.5f);
                currY += Time.deltaTime * speed;
            }

            //decrease currY and pass to shader
            else if (reverse && currY > min)
            {
                material.SetFloat("_DissolveY", currY);
                currY -= Time.deltaTime * speed;
                material.SetFloat("_Intensity", this.lightsource.GetRange());
            }

            //stop updating currY and allow player to move
            if (currY >= max || currY <= min)
            {
                run = false;
            }
        }

        else if (!reverse)
        {
            material.SetFloat("_DissolveY", this.runMax);
            material.SetFloat("_Intensity", this.lightsource.GetRange());
        }
    }

    public bool getReverse()
    {
        return this.reverse;
    }

    public void setReverse(bool value)
    {
        this.reverse = value;
    }

    public bool getRun()
    {
        return this.run;
    }

    public void setRun(bool value)
    {
        this.run = value;
    }
}
