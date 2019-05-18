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
    private float depth = 10.0f;

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

    #region Random Generation Implementation
    
    [SerializeField]
    private bool shouldGenerateRandomSizes = false;

    [SerializeField, Range(1, 100)]
    private int randomSeed = 10;

    private int prevRandomSeed = 10;

    [SerializeField, Range(1.0f, 30.0f)]
    private float maxRandomSize = 10.0f;
    
    #endregion

    void Start()
    {
        Random.InitState(randomSeed);

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        ApplyRandomMaterial();
    }

    void Update() 
    { 
        if(shapeType == ShapeType.Quad)
        {
            GenerateQuad();
        }
        else if(shapeType == ShapeType.Cube && !shouldGenerateRandomSizes)
        {
            GenerateCube();
        }

        if(prevRandomSeed != randomSeed && shouldGenerateRandomSizes)
        {
            prevRandomSeed = randomSeed;
            
            GenerateRandomSizes();
            
            GenerateCube();

            ApplyRandomMaterial();
        }
    } 

    void GenerateRandomSizes ()
    {
        width = Random.Range(1.0f, maxRandomSize);
        height = Random.Range(1.0f, maxRandomSize);
        depth = Random.Range(1.0f, maxRandomSize);
    }

    void GenerateCube()
    {
        // Step 1 - Create & Assign Vertices
        Vector3[] vertices = new Vector3[24];
        
        // Front Face Vertices
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(width, 0, 0);
        vertices[2] = new Vector3(0, height, 0);
        vertices[3] = new Vector3(width, height, 0);

        // Top Face Vertices
        vertices[4] = new Vector3(0, height, 0);
        vertices[5] = new Vector3(width, height, 0);
        vertices[6] = new Vector3(0, height, depth);
        vertices[7] = new Vector3(width, height, depth);

        // Back Face Vertices
        vertices[8] = new Vector3(0, 0, depth);
        vertices[9] = new Vector3(width, 0, depth);
        vertices[10] = new Vector3(0, height, depth);
        vertices[11] = new Vector3(width, height, depth);

        // Bottom Face Vertices
        vertices[12] = new Vector3(0, 0, 0);
        vertices[13] = new Vector3(width, 0, 0);
        vertices[14] = new Vector3(0, 0, depth);
        vertices[15] = new Vector3(width, 0, depth);

        // Left Face Vertices
        vertices[16] = new Vector3(0, 0, 0);
        vertices[17] = new Vector3(0, 0, depth);
        vertices[18] = new Vector3(0, height, 0);
        vertices[19] = new Vector3(0, height, depth);
        
        // Right Face Vertices
        vertices[20] = new Vector3(width, 0, 0);
        vertices[21] = new Vector3(width, 0, depth);
        vertices[22] = new Vector3(width, height, 0);
        vertices[23] = new Vector3(width, height, depth);

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

        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 top = Vector3.up;
        Vector3 bottom = Vector3.down;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        // Front Face normals
        normals[0] = front;
        normals[1] = front;
        normals[2] = front;
        normals[3] = front;

        // Top Face normals
        normals[4] = top;
        normals[5] = top;
        normals[6] = top;
        normals[7] = top;

        // Back Face normals
        normals[8] = back;
        normals[9] = back;
        normals[10] = back;
        normals[11] = back;

        // Bottom Face normals
        normals[12] = bottom;
        normals[13] = bottom;
        normals[14] = bottom;
        normals[15] = bottom;

        // Left Face normals
        normals[16] = left;
        normals[17] = left;
        normals[18] = left;
        normals[19] = left;

        // Right Face normals
        normals[20] = right;
        normals[21] = right;
        normals[22] = right;
        normals[23] = right;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        mesh.Optimize();
    }

    void GenerateQuad()
    {
        // Step 1 - Create & Assign Vertices
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(width, 0, 0);
        vertices[2] = new Vector3(0, height, 0);
        vertices[3] = new Vector3(width, height, 0);

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
