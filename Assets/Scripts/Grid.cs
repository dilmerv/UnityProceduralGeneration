using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField,Range(1,200)]
    private int width = 1;
    private int prevWidth = 1;

    [SerializeField,Range(1,200)]
    private int height = 1;
    private int prevHeight = 1;

    private GameObject[,] grid = null;

    [SerializeField,Range(0.1f,50.0f)]
    private float shapeWidth = 2.0f;

    private float prevShapeWidth = 2.0f;

    [SerializeField,Range(0.1f,50.0f)]
    private float shapeHeight = 2.0f;

    private float prevShapeHeight = 2.0f;

    [SerializeField,Range(0.1f,50.0f)]
    private float shapeDepth = 2.0f;

    private float prevShapeDepth = 2.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    private float maxRandomHeightOffset = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    private float maxRandomWidthOffset = 1.0f;

    [SerializeField,Range(0.1f, 50.0f)]
    private float maxRandomDepthOffset = 1.0f;

    [SerializeField, Range(1,100)]
    private int randomSeed = 1;
    private int prevSeed = 1;

    [SerializeField]
    private string shaderName = "Lightweight Render Pipeline/Lit";

    [SerializeField, Tooltip("How many procedural materials to generate?")]
    private int proceduralMaterialsToGenerate = 3;

    private int prevProceduralMaterialsToGenerate = 3;

    private Material[] proceduralMaterials = null;

    [SerializeField]
    private Material[] defaultMaterials = null;

    #region Margins

    [SerializeField]
    private Vector3 marginBetweenShapes = new Vector3(1.0f, 1.0f, 1.0f);

    #endregion

    #region Physics

    [SerializeField]
    private bool shouldGenerateRigidBodies = false;

    private bool prevShouldGenerateRigidBodies = false;

    #endregion

    #region UI Bindings

    [SerializeField]
    private Text numOfShapesText = null;

    [SerializeField]
    private Text lengthOfTimeText = null;

    [SerializeField]
    private bool makeShapesStatic = true;

    #endregion
    void Start()
    {   
        prevWidth = width;
        prevHeight = height;

        Random.InitState(randomSeed);
        grid = new GameObject[height, width];               
        proceduralMaterials = new Material[proceduralMaterialsToGenerate];

        // generate materials if needed
        if(proceduralMaterialsToGenerate > 0 && defaultMaterials.Length == 0)
            proceduralMaterials = MeshRendererExtensions.GetRandomMaterials(shaderName, proceduralMaterialsToGenerate);

        BuildGrid(); 
    }

    void BuildGrid()
    {
        numOfShapesText.text = $"{width * height}";

        DateTime started = DateTime.Now;
        
        for(int row = 0; row < height; row++)
        {
            for(int col = 0; col < width; col++)
            {
                StartCoroutine(AddCell(row, col));
            }
        }

        DateTime ended = DateTime.Now;
        TimeSpan diff = ended - started;
        if(diff.Seconds == 0)
            lengthOfTimeText.text = $"{diff.Milliseconds} milliseconds";
        else if(diff.Seconds < 100)
            lengthOfTimeText.text = $"{diff.Seconds}.{diff.Milliseconds} seconds";
        else 
            lengthOfTimeText.text = $"{diff.Minutes}.{diff.Seconds} minutes";
            
    }
    
    IEnumerator AddCell(int row, int col)
    {
        GameObject cell = null;
        if(grid[row, col] == null)
        {
            cell = new GameObject($"cell_{row}_{col}");
            cell.transform.parent = gameObject.transform;
            grid[row, col] = cell;
        }   
        else {
            DestroyImmediate(grid[row, col]);
            grid[row, col] = new GameObject($"cell_{row}_{col}");
            cell = grid[row, col];
            cell.transform.parent = gameObject.transform;
        }

        cell.isStatic = makeShapesStatic;
        cell.transform.position = 
            Vector3.Scale(new Vector3(
                        shapeWidth  * row * Random.Range(1.0f, maxRandomWidthOffset),
                        shapeHeight * Random.Range(1.0f, maxRandomHeightOffset),
                        shapeDepth  * col * Random.Range(1.0f, maxRandomDepthOffset)), 
                        marginBetweenShapes);

        MeshFilter meshFilter = cell.AddComponent<MeshFilter>();
        MeshRenderer renderer = cell.AddComponent<MeshRenderer>();

        Cube cube = new Cube {
            Width = shapeWidth,
            Height = shapeHeight,
            Depth = shapeDepth
        };
        
        meshFilter.mesh = cube.Generate();
        
        if(proceduralMaterials.Length > 0 && defaultMaterials.Length == 0)
            renderer.material = proceduralMaterials[Random.Range(0, proceduralMaterialsToGenerate - 1)];
        else if(proceduralMaterials.Length == 0 && defaultMaterials.Length > 0)
            renderer.material = defaultMaterials[Random.Range(0, defaultMaterials.Length - 1)];

        if(shouldGenerateRigidBodies){
            cell.AddComponent<BoxCollider>();
            cell.AddComponent<Rigidbody>();
        }

        yield return null;
    }

    void Update()
    {
        if(prevSeed != randomSeed)
        {
            BuildGrid();
            prevSeed = randomSeed;
        }

        if(PropertiesChanged())
        {
            ClearAll();
            
            prevWidth = width;
            prevHeight = height;
            prevShapeWidth = shapeWidth;
            prevShapeHeight = shapeHeight;
            prevShapeDepth = shapeDepth;
            prevShouldGenerateRigidBodies = shouldGenerateRigidBodies;
            prevProceduralMaterialsToGenerate = proceduralMaterialsToGenerate;

            grid = new GameObject[height, width];
            
            BuildGrid();
        }
    }

    private bool PropertiesChanged()
    {
        return prevWidth != width || prevHeight != height || prevShapeWidth != shapeWidth
            || prevShapeHeight != shapeHeight || prevShapeDepth != shapeDepth
            || prevShouldGenerateRigidBodies != shouldGenerateRigidBodies
            || prevProceduralMaterialsToGenerate != proceduralMaterialsToGenerate;
    }

    private void ClearAll()
    {
        for(int row = 0; row < prevHeight; row++)
        {
            for(int col = 0; col < prevWidth; col++)
            {
                if(grid[row, col] != null)
                    DestroyImmediate(grid[row, col]);
            }
        }
    }
}
