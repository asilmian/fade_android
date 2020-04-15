using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*same code as floor script
 * added to increase readability 
 */
public class Bridge : MonoBehaviour {

    // Use this for initialization
    public Shader shader;
    public Texture texture;
    public PointLight lightsource;
    // Use this for initialization
    void Start()
    {
        MeshRenderer mesh = this.gameObject.GetComponent<MeshRenderer>();
        mesh.material.shader = shader;
        mesh.material.mainTexture = texture;
    }

    //pass lightsource info to shader
    void LateUpdate()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
    }
}
