using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGenerator : MonoBehaviour
{   
    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }


    public void Start()
    {
        GenerateShip();
    }

    private void GenerateShip()
    {
        mesh = new Mesh();
        
        //Vertices
        Vector3[] vertices = new Vector3[5];
        vertices[0] = new Vector3(2, 0, 0);
        vertices[1] = new Vector3(5, 0, 0);
        vertices[2] = new Vector3(4, 7, 0);
        vertices[3] = new Vector3(1, 2, 0);
        vertices[4] = new Vector3(2, 1, 0);

        mesh.vertices = vertices;
        
        //Triangles
        int[] trianges ={0,1,4,1,2,4,2,3,4};
        

        
        mesh.triangles = trianges;
        meshFilter.mesh = mesh;
        mesh.RecalculateNormals();
        
    }
}
