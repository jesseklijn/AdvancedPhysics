using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Check for required components
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, zSize;
    private Vector3[] vertices;
    private Mesh mesh;


    private Vector2[] uv;


    public GameObject prefab;
    public Transform parent;

    private void OnDrawGizmos()
    {
        if (vertices == null) { return; } //Make sure they are only called if vertices are not null
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(vertices[i].x + transform.position.x, vertices[i].y + transform.position.y, vertices[i].z + transform.position.z), 0.05F);
        }

    }

    private void Awake()
    {

        //Assign the start of the mesh to the middle
        transform.position = new Vector3(transform.position.x - (xSize / 2), transform.position.y, transform.position.z - (zSize / 2));

        //Create a mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Grid";

        //Start the mesh generation
        StartCoroutine(Generate());


        //Enable for debugging purposes
        //for (int i = 0; i < vertices.Length; i++)
        //{

        //    GameObject text = Instantiate(prefab, new Vector3(vertices[i].x + transform.position.x, vertices[i].y + transform.position.y, vertices[i].z + transform.position.z), Quaternion.identity, parent);
        //    text.GetComponent<TextMesh>().text = i.ToString();
        //    text.transform.Rotate(90, 0, 0);
        //    text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + 0.2F, text.transform.position.z);
        //}
    }

    private IEnumerator Generate()
    {

        //Initialize the vertices according to the size of the grid board (having a minimum of 1x1)
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //Initialize the uv map
        uv = new Vector2[vertices.Length];

        //Fill the vertices with coordinates
        for (int i = 0, z = 0, ui = 0; z <= zSize; z++, ui += xSize + 1)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0, z);

                uv[ui + x] = new Vector2(x, z);


            }
        }

        //assign the vertices to the mesh
        mesh.vertices = vertices;

        //Assign the uv to the mesh
        mesh.uv = uv;

        //Assign the size of the triangles
        int[] triangles = new int[xSize * zSize * 6];

        //vi = vertex index,  ti = triangle index
        for (int ti = 0, vi = 0, z = 0; z < zSize; z++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                //Create a square, by filling up 6 triangle indexes
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;





            }
        }

        //Assign triangles to the mesh
        mesh.triangles = triangles;

        //Recalculate the normals 
        mesh.RecalculateNormals();

        yield return 0;
    }

    private void Update()
    {

    }

}
