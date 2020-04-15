using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Same code as section shader but with different name, inherits from Bridge script.
 * added to increase readability 
 */
public class BridgeWall : MonoBehaviour {
    private PointLight lightsource;
	// Use this for initialization
	void Start () {
        MeshRenderer mesh = this.gameObject.GetComponent<MeshRenderer>();
        mesh.material.shader = this.GetComponentInParent<Bridge>().shader;
        mesh.material.mainTexture = this.GetComponentInParent<Bridge>().texture;
        this.lightsource = this.GetComponentInParent<Bridge>().lightsource;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
    }
}
