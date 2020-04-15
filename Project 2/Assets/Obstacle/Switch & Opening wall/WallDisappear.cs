using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* applies texture to a wall
 * used for wall that will be destroyed
 */
public class WallDisappear : MonoBehaviour {


    public Texture texture;

	// Use this for initialization
	void Start () {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
