using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandForming : MonoBehaviour
{

    public float scale = 0.1f;
    public float speed = 1.0f;
    public float noiseStrength = 5f;
    public float noiseWalk = 1f;

    public Grid grid;
    public Grid ocean;
    private Vector3[] baseHeight;

    void Start()
    {
        transform.position = new Vector3( -grid.xSize/2, ocean.transform.position.y, - grid.zSize/2);

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            float v = ((vertices[i].x - grid.xSize / 2) * (vertices[i].x - grid.xSize / 2) + (vertices[i].z - grid.zSize / 2) * (vertices[i].z - grid.zSize / 2));


            Debug.Log(v);

            float seed = Random.Range(0, .4f);
            Vector3 vertex = baseHeight[i];
            if (v < (grid.xSize + 1) + (grid.zSize + 1) / 2)
                vertex.y += 1f + Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y) + seed;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
