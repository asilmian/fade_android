using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Creates a Cone (spike) on the screen
 * Same code as ConeScript. See ConeScript for detailed comments
 */
public class ConeScriptParent : MonoBehaviour
{

    public float sections;
    public float radius;
    public float high;
    public BoxCollider collider;
    public Shader shader;
    public PointLight lightsource;
    public Texture texture;

    // Use this for initialization
    void Start()
    {
        MeshFilter coneMesh = this.gameObject.AddComponent<MeshFilter>();
        coneMesh.mesh = this.ConeMesh();

        //assign shader and texture
        MeshRenderer renderer = this.gameObject.AddComponent<MeshRenderer>();
        renderer.material.shader = shader;
        renderer.material.mainTexture = texture;

        collider = gameObject.AddComponent<BoxCollider>();
        this.collider.isTrigger = true;

    }

    Mesh ConeMesh()
    {
        Mesh m = new Mesh();

        m.name = "Cone";
        float currentAngle = 0.0f;
        float nextAngle;
        Vector3[] vertices = new Vector3[(int)sections * 3];
        for (int i = 0; i < sections * 3; i++)
        {
            if (i % 3 == 0)
            {
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
            color[i] = Color.red;
        }
        m.colors = color;

        int[] tri = new int[m.vertexCount];
        for (int i = 0; i < m.vertexCount; i++)
        {
            tri[i] = m.vertexCount - i - 1;
        }
        m.triangles = tri;

        return m;
    }

    void LateUpdate()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        renderer.material.SetColor("_PointLightColor", this.lightsource.color);
        renderer.material.SetVector("_PointLightPosition", this.lightsource.GetWorldPosition());
        renderer.material.SetFloat("_Range", this.lightsource.GetRange());
    }
}