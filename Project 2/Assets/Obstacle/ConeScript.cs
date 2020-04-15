using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/***
 * Renders a cone on the screen
 * same code as ConeScript parent except it inherits the textures and shader
 */
public class ConeScript : MonoBehaviour
{

    public float sections;
    public float radius;
    public float high;
    private BoxCollider collider;
    private Shader shader;
    private PointLight lightsource;
    private Texture texture;

    // Use this for initialization
    void Start()
    {
        //make cone
        MeshFilter coneMesh = this.gameObject.AddComponent<MeshFilter>();
        coneMesh.mesh = this.ConeMesh();

        //add shader and texture to cone
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = this.gameObject.GetComponentInParent<ConeScriptParent>().shader;
        renderer.material.mainTexture = this.gameObject.GetComponentInParent<ConeScriptParent>().texture;

        //add collider
        collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;

        this.lightsource = this.gameObject.GetComponentInParent<ConeScriptParent>().lightsource;

    }

    Mesh ConeMesh()
    {
        Mesh m = new Mesh();

        m.name = "Cone";

        //starting angle of cone
        float currentAngle = 0.0f;
        float nextAngle;
        Vector3[] vertices = new Vector3[(int)sections * 3];
        for (int i = 0; i < sections * 3; i++)
        {
            if (i % 3 == 0)
            {
                //create vertcies
                currentAngle += (float)(1.0f / sections * Mathf.PI * 2.0f);
                vertices[i] = new Vector3(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle), 0.0f) * radius;
                nextAngle = currentAngle + (float)(1.0f / sections * Mathf.PI * 2.0f);
                vertices[i + 1] = new Vector3(Mathf.Sin(nextAngle), Mathf.Cos(nextAngle), 0.0f) * radius;
                vertices[i + 2] = new Vector3(0.0f, 0.0f, high);
            }
        }
        m.vertices = vertices;

        Color[] color = new Color[(int)sections * 3];
        for (int i = 0; i < sections * 3; i++)
        {
            //assign arbitary color
            color[i] = Color.red;
        }
        m.colors = color;

        int[] tri = new int[m.vertexCount];
        for (int i = 0; i < m.vertexCount; i++)
        {
            //reverse order of triangles
            tri[i] = m.vertexCount - i - 1;
        }
        m.triangles = tri;

        return m;
    }


    //pass info to shader
    void LateUpdate()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
    }

    //reset level if player dies
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
