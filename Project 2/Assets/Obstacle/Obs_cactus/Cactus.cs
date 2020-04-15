using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Same script as floor but with different name, added for easier readability
 */

public class Cactus : MonoBehaviour {
    public Shader shader;
    public Texture texture;
    public PointLight lightsource;

	// Use this for initialization
	void Start () {
        MeshRenderer meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material.shader = shader;
        meshRenderer.material.mainTexture = texture;
	}

    // Update is called once per frame
    void LateUpdate()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
    }
}
