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

    static Color GetRandomColor() => Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    
}