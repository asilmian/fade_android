/*
 * Manages the light of the player
 * Made by Karoline Bernacki, Asil Mian, Usama Ahmed
 * Made on 08.10.2018
 * Last updated on 08.10.2018
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class PointLight : MonoBehaviour
{
    public Color color;
    public Light lt;
    public float lightfadespeed = 0.02f;
    public float max_intensity = 2.5f;

    private bool canFade = false;

    private void Start()
    {
        lt = this.GetComponent<Light>();
    }

    public Vector3 GetWorldPosition()
    {
        return this.transform.position;
    }

    public float GetRange()
    {
        return this.lt.intensity;
    }


    //decrease light intensity if playerController allows
    private void Update() {
        if (canFade)
        {
            this.lt.intensity -= lightfadespeed * Time.deltaTime;
        }

        if(this.lt.intensity <= 0f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void setCanFade(bool value)
    {
        this.canFade = value;
    }
}
