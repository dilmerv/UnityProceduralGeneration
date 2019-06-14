using UnityEngine;

public class Quad : Shape
{
    public override Mesh Generate()
    {
        ShapeType = ShapeTypes.Quad;

        Mesh mesh = new Mesh();

        // Step 1 - Create & Assign Vertices
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(Width, 0, 0);
        vertices[2] = new Vector3(0, 0, Depth);
        vertices[3] = new Vector3(Width,0, Depth);

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

        return mesh;
    }
}