using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*inherits shader, lightsource and texture from parent Floor script. 
 * Passes info to shader 
 */
public class SectionShader : MonoBehaviour {
    private Shader shader;
    private Texture texture;
    private PointLight lightsource;

	// Use this for initialization
	void Start () {
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        this.shader = this.GetComponentInParent<Floor>().shader;
        this.texture = this.GetComponentInParent<Floor>().texture;
        meshRenderer.material.shader = shader;
        meshRenderer.material.mainTexture = texture;
        this.lightsource = this.GetComponentInParent<Floor>().lightsource;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
		
	}
}
