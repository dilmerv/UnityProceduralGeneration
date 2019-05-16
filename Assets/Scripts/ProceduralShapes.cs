using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralShapes : MonoBehaviour
{
    [SerializeField]
    private float width = 10.0f;

    [SerializeField]
    private float height = 10.0f;

    [SerializeField]
    private string randomShaderTypeName = "Lightweight Render Pipeline/Lit";

    [SerializeField]
    private ShapeType shapeType = ShapeType.Quad;

    private enum ShapeType
    {
        Quad,
        Cube
    }

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        ApplyRandomMaterial();
    }

    void Update() { 
        if(shapeType == ShapeType.Quad)
            GenerateQuad(width, height);
        else if(shapeType == ShapeType.Cube)
            GenerateCube(width, height);
    } 

    void GenerateCube(float newWidth, float newHeight)
    {
        // Step 1 - Create & Assign Vertices
        Vector3[] vertices = new Vector3[24];
        
        // Front Face Vertices
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(newWidth, 0, 0);
        vertices[2] = new Vector3(0, newHeight, 0);
        vertices[3] = new Vector3(newWidth, newHeight, 0);

        // Top Face Vertices
        vertices[4] = new Vector3(0, newHeight, 0);
        vertices[5] = new Vector3(newWidth, newHeight, 0);
        vertices[6] = new Vector3(0, newHeight, newWidth);
        vertices[7] = new Vector3(newWidth, newHeight, newWidth);

        // Back Face Vertices
        vertices[8] = new Vector3(0, 0, newWidth);
        vertices[9] = new Vector3(newWidth, 0, newWidth);
        vertices[10] = new Vector3(0, newHeight, newWidth);
        vertices[11] = new Vector3(newWidth, newHeight, newWidth);

        // Bottom Face Vertices
        vertices[12] = new Vector3(0, 0, 0);
        vertices[13] = new Vector3(newWidth, 0, 0);
        vertices[14] = new Vector3(0, 0, newWidth);
        vertices[15] = new Vector3(newWidth, 0, newWidth);

        // Left Face Vertices
        vertices[16] = new Vector3(0, 0, 0);
        vertices[17] = new Vector3(0, 0, newWidth);
        vertices[18] = new Vector3(0, newHeight, 0);
        vertices[19] = new Vector3(0, newHeight, newWidth);
        
        // Right Face Vertices
        vertices[20] = new Vector3(newWidth, 0, 0);
        vertices[21] = new Vector3(newWidth, 0, newWidth);
        vertices[22] = new Vector3(newWidth, newHeight, 0);
        vertices[23] = new Vector3(newWidth, newHeight, newWidth);

        // Step 2 - Create & Assign Triangles
        int[] triangles = new int[36];

        // Front Face Triangles
        triangles[0] = 0; // first triangle
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 2; // second triangle
        triangles[4] = 3;
        triangles[5] = 1;

        // Top Face Triangles
        triangles[6] = 4; // first triangle
        triangles[7] = 6;
        triangles[8] = 5;

        triangles[9] = 6; // second triangle
        triangles[10] = 7;
        triangles[11] = 5;

        // Back Face Triangles
        triangles[12] = 8; // first triangle
        triangles[13] = 10;
        triangles[14] = 9;

        triangles[15] = 10; // second triangle
        triangles[16] = 11;
        triangles[17] = 9;

        // Bottom Face Triangles
        triangles[18] = 12; // first triangle
        triangles[19] = 14;
        triangles[20] = 13;

        triangles[21] = 14; // second triangle
        triangles[22] = 15;
        triangles[23] = 13;

        // Left Face Triangles
        triangles[24] = 16; // first triangle
        triangles[25] = 18;
        triangles[26] = 17;

        triangles[27] = 18; // second triangle
        triangles[28] = 19;
        triangles[29] = 17;

        // Left Face Triangles
        triangles[30] = 20; // first triangle
        triangles[31] = 22;
        triangles[32] = 21;

        triangles[33] = 22; // second triangle
        triangles[34] = 23;
        triangles[35] = 21;

        // Step 3 - Create & Assign normal
        Vector3[] normals = new Vector3[24];

        // Front Face normals
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;

        // Top Face normals
        normals[4] = -Vector3.forward;
        normals[5] = -Vector3.forward;
        normals[6] = -Vector3.forward;
        normals[7] = -Vector3.forward;

        // Back Face normals
        normals[8] = -Vector3.forward;
        normals[9] = -Vector3.forward;
        normals[10] = -Vector3.forward;
        normals[11] = -Vector3.forward;

        // Bottom Face normals
        normals[12] = -Vector3.forward;
        normals[13] = -Vector3.forward;
        normals[14] = -Vector3.forward;
        normals[15] = -Vector3.forward;

        // Left Face normals
        normals[16] = -Vector3.forward;
        normals[17] = -Vector3.forward;
        normals[18] = -Vector3.forward;
        normals[19] = -Vector3.forward;

        // Right Face normals
        normals[20] = -Vector3.forward;
        normals[21] = -Vector3.forward;
        normals[22] = -Vector3.forward;
        normals[23] = -Vector3.forward;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        mesh.Optimize();
    }

    void GenerateQuad(float newWidth, float newHeight)
    {
        // Step 1 - Create & Assign Vertices
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(newWidth, 0, 0);
        vertices[2] = new Vector3(0, newHeight, 0);
        vertices[3] = new Vector3(newWidth, newHeight, 0);

        // Step 2 - Create & Assign Triangles
        int[] triangles = new int[6];
        triangles[0] = 0; // first triangle
        triangles[1] = 2;
        triangles[2] = 1;

        triangles[3] = 2; // second triangle
        triangles[4] = 3;
        triangles[5] = 1;

        // Step 3 - Create & Assign normal
        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;

        // Step 4 - Create & Assign the UVs
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0,0);
        uvs[1] = new Vector2(1,0);
        uvs[2] = new Vector2(0,1);
        uvs[3] = new Vector2(1,1);

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uvs;

        mesh.Optimize();
    }

    void ApplyRandomMaterial()
    {
        Material randomMaterial = new Material(Shader.Find(randomShaderTypeName));
        randomMaterial.name = $"{gameObject.name}_material";
        randomMaterial.color = GetRandomColor();
        randomMaterial.EnableKeyword("_EMISSION");
        randomMaterial.SetInt("_Cull", 0);
        randomMaterial.SetColor("_EmissionColor", randomMaterial.color);
        GetComponent<Renderer>().material = randomMaterial;
    }

    Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
}
