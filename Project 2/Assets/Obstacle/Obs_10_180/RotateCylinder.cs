using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Rotates object based upon speed
 * also assigns texture, shader and lightsource information
 */
public class RotateCylinder : MonoBehaviour {
    public float rotateSpeed = 10;
    public Shader shader;
    public Texture texture;
    public PointLight lightsource;

    private void Start()
    {
        MeshRenderer mesh = this.GetComponent<MeshRenderer>();
        mesh.material.shader = shader;
        mesh.material.mainTexture = texture;
    }
    // Rotate object and pass info to shader
    void LateUpdate () {
        this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * rotateSpeed, Vector3.up);
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
    }
}
