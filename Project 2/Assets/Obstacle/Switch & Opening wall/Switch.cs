using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* switch animation
 * rotates object 180 in y axis 
 */
public class Switch : MonoBehaviour {

    public GameObject wall;

	// Use this for initialization

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Destroy(wall);
            this.transform.eulerAngles = new Vector3 (0, 180, 0);
        }
    }
}
