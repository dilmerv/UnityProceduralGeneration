using UnityEngine;

public static class MeshRendererExtensions
{
    public static void ApplyRandomMaterial(this MeshRenderer renderer, string shaderName, string gameObjectName)
    {
        Material randomMaterial = new Material(Shader.Find(shaderName));
        randomMaterial.name = $"{gameObjectName}_material";
        randomMaterial.color = GetRandomColor();
        randomMaterial.EnableKeyword("_EMISSION");
        randomMaterial.SetInt("_Cull", 0);
        randomMaterial.SetColor("_EmissionColor", randomMaterial.color);
        renderer.material = randomMaterial;
    }

    public static Material[] GetRandomMaterials(string shaderName, int count)
    {
        Material[] materials = new Material[count];
        for(int i = 0; i < count; i++){
            Material randomMaterial = new Material(Shader.Find(shaderName));
            randomMaterial.name = $"{i}_material";
            randomMaterial.color = GetRandomColor();
            randomMaterial.EnableKeyword("_EMISSION");
            randomMaterial.SetInt("_Cull", 0);
            randomMaterial.SetColor("_EmissionColor", randomMaterial.color);
            materials[i] = randomMaterial;
        }
        return materials;
    }

    static Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    
}