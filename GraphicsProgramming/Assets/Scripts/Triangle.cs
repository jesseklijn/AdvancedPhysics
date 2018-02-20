using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{

    public Mesh mesh;
    public Vector3[] verts;
    int[] tris;

    public const int TRIANGLEVERTICECOUNT = 3;
    public int xPos = 4, yPos = 1, zPos = 4, MULTIPLIER = 2, step = 4;
    public int[] cubePoint = new int[] { 1, 5, 0, 5, 4, 0 };
    public Vector3[] vectors;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x, mesh.vertices[i].y, mesh.vertices[i].z), 0.05F);

        }
    }




    // Use this for initialization
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        //Assign vertices
        vectors = new Vector3[xPos * yPos * zPos];
        int index = 0;
        for (int x = 0; x < xPos; x++)
        {
            for (int y = 0; y < yPos; y++)
            {

                for (int z = 0; z < zPos; z++)
                {

                    vectors[index] = new Vector3(x, y, z);
                    index++;

                }
            }

        }

        verts = vectors;

        //Define triangles
        tris = new int[((xPos - 1) * (zPos - 1)) * TRIANGLEVERTICECOUNT];
        int increment = 4;

        for (int i = 0; i < tris.Length; i += (TRIANGLEVERTICECOUNT))
        {
            Debug.Log("test1");



            if (i/3 % (xPos - 1) == 0)
            {
                Debug.Log("test2");
                cubePoint = new int[] { 1, 5, 0, 5, 4, 0 };
                for (int z = 0; z < cubePoint.Length; z++)
                {
                    
                    cubePoint[z]+=1;
                }  
            }
            for (int l = 0; l < cubePoint.Length; l++)
            {
                tris[i + l] = cubePoint[l];
                cubePoint[l] += increment;
            }
            //1,5,0, 5,4,0,   5,9,4,  9,8,4,    9,13,8, 13,12,8, 
            //2,6,1,  6,5,1,    6,10,5,  10,9,5,    10,14,9, 14,13,9,
        }


        //2,6,1  6,5,1    6,10,5  10,9,5    10,14,9 14,13,9
        //1,5,0, 5,4,0,   5,9,4,  9,8,4,    9,13,8, 13,12,8, 

        //cube one

        //Assign all 
        mesh.vertices = verts;

        mesh.triangles = tris;

        //mesh.uv = new Vector2[] { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0) };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
