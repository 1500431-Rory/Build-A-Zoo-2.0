using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCreator : MonoBehaviour {


    Mesh triangleMesh;
    MeshRenderer meshRenderer;
    Vector3[] vertices;

    int[] triangles;

    public Material material;

	// Use this for initialization
	void Start () {

        gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.material = material;

        triangleMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = triangleMesh;

        vertices = new[]
        {
            new Vector3(-0.5f,0,-0.5f),
            new Vector3(-0.5f,0,0.5f),
             new Vector3(0.5f,0,-0.5f)

        };

        triangleMesh.vertices = vertices;

        triangles = new[] { 0,1,2 };

        triangleMesh.triangles = triangles;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
