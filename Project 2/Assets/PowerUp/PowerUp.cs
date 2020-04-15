using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***power up lights for the player
 */
public class PowerUp : MonoBehaviour
{

    public Color color;
    public Light lt;
    public Texture texture;
    public PointLight playerLight;

    private void Start()
    {
        lt = this.GetComponent<Light>();
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.material.mainTexture = texture;
    }

    //increase player light on collision
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            playerLight.lt.intensity += 0.2f;
            if(playerLight.lt.intensity >= playerLight.max_intensity)
            {
                playerLight.lt.intensity = playerLight.max_intensity;
            }
            Destroy(this.gameObject);
        }
    }

}
