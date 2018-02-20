using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexModifier : MonoBehaviour {


    //Create a mesh
    Mesh mesh;
    Vector3[] verts;
    Vector3[] normals;
    // Use this for initialization
    void Start () {

        //Initialize the mesh
        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
        normals = mesh.normals;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] += 0.1F * normals[i] * Mathf.Sin(Time.time * 5 +i);
        }
        mesh.vertices = verts;
    }
}
