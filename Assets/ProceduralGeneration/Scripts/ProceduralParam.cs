using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralParameter", menuName = "Procedural/Parameter", order = 0)]
public class ProceduralParam : ScriptableObject 
{
    [SerializeField,Range(1,200)]
    public int width = 1;

    [SerializeField,Range(1,200)]
    public int height = 1;

    [SerializeField,Range(0.1f,50.0f)]
    public float shapeWidth = 2.0f;

    [SerializeField,Range(0.1f,50.0f)]
    public float shapeHeight = 2.0f;

    [SerializeField,Range(0.1f,50.0f)]
    public float shapeDepth = 2.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    public float maxRandomHeight = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    public float maxRandomWidth = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    public float maxRandomDepth = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    public float maxRandomHeightOffset = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    public float maxRandomWidthOffset = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    public float maxRandomDepthOffset = 1.0f;

    [SerializeField, Range(1,100)]
    public int randomSeed = 1;

    [SerializeField]
    public ShapeTypes ShapeType = ShapeTypes.Cube;

    [SerializeField]
    public bool makeShapesStatic = true;

    [SerializeField]
    public bool shouldGenerateRigidBodies = false;

    [SerializeField]
    public Vector3 marginBetweenShapes = new Vector3(1.0f, 1.0f, 1.0f);
    
    [SerializeField]
    public Material[] defaultMaterials = null;

    [SerializeField]
    public string shaderName = "Lightweight Render Pipeline/Lit";

    [SerializeField, Range(0,100), Tooltip("How many procedural materials to generate?")]
    public int proceduralMaterialsToGenerate = 3;
}