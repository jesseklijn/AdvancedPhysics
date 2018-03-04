using UnityEngine;
using System.Collections;

public class WaveGen : MonoBehaviour
{
    public float scaleHeightDifference = 0.1f;
    public float waveSpeed = 1.0f;

    private Vector3[] baseVertices;
    Vector3[] newVertices;
    private Mesh mesh;

    private void Start()
    {
        //Get the Mesh
        mesh = GetComponent<MeshFilter>().mesh;
        //Put mesh vertices into our modifiable vertices
        baseVertices = mesh.vertices;
        //Assign the length of the vertices
        newVertices = new Vector3[baseVertices.Length];
    }


    void Update()
    {
        for (int i = 0; i < newVertices.Length; i++)
        {
            //Get old vertex, put it in the new sample vertex
            Vector3 sampleVertex = baseVertices[i];
            //Modify the y value of the sample vertex
            sampleVertex.y += Mathf.Sin(Time.time * waveSpeed + baseVertices[i].x + baseVertices[i].y + baseVertices[i].z) * scaleHeightDifference;

            //Assign it to the new collection
            newVertices[i] = sampleVertex;
        }

        //Assign the new vertices into the old vertices of the mesh
        mesh.vertices = newVertices;

        //Calculate normals
        mesh.RecalculateNormals();
    }
}
