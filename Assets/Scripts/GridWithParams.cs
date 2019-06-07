using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GridWithParams : MonoBehaviour
{
    [SerializeField]
    public ProceduralParam parameters = null;

    private GameObject[,] grid = null;

    private Material[] proceduralMaterials = null;

    #region UI Bindings

    [SerializeField]
    private Text numOfShapesText = null;

    [SerializeField]
    private Text lengthOfTimeText = null;

    #endregion

    private Bounds bounds;


    void OnEnable() 
    {
        if(parameters == null)
        {
            Debug.LogError("You must set procedural parameters in order to use this class");
            enabled = false;
        }
    }

    void Reset()
    {
        ClearAll();

        Random.InitState(parameters.randomSeed);
        grid = new GameObject[parameters.height, parameters.width];               
        proceduralMaterials = new Material[parameters.proceduralMaterialsToGenerate];
        bounds = new Bounds (Vector3.zero, Vector3.zero);

        // generate materials if needed
        if(parameters.proceduralMaterialsToGenerate > 0 && parameters.defaultMaterials.Length == 0)
            proceduralMaterials = MeshRendererExtensions.GetRandomMaterials(parameters.shaderName, parameters.proceduralMaterialsToGenerate);
    }

    void Start() => BuildGrid();

    public void BuildGrid()
    {
        Reset();

        grid = new GameObject[parameters.height, parameters.width];

        numOfShapesText.text = $"{parameters.width * parameters.height}";

        DateTime started = DateTime.Now;        

        for(int row = 0; row < parameters.height; row++)
        {
            for(int col = 0; col < parameters.width; col++)
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

        cell.isStatic = parameters.makeShapesStatic;
        cell.transform.localPosition =  
            Vector3.Scale(new Vector3(
                        parameters.shapeWidth * row * Random.Range(1.0f, parameters.maxRandomWidthOffset), 0,
                        parameters.shapeDepth * col * Random.Range(1.0f, parameters.maxRandomDepthOffset)), 
                        parameters.marginBetweenShapes);

        MeshFilter meshFilter = cell.AddComponent<MeshFilter>();
        MeshRenderer renderer = cell.AddComponent<MeshRenderer>();

        Cube cube = new Cube {
            Width = parameters.shapeWidth * Random.Range(1.0f, parameters.maxRandomWidth),
            Height = parameters.shapeHeight * Random.Range(1.0f, parameters.maxRandomHeight),
            Depth = parameters.shapeDepth * Random.Range(1.0f, parameters.maxRandomDepth)
        };

        meshFilter.mesh = cube.Generate();

        if(proceduralMaterials.Length > 0 && parameters.defaultMaterials.Length == 0)
            renderer.material = proceduralMaterials[Random.Range(0, parameters.proceduralMaterialsToGenerate - 1)];
        else if(proceduralMaterials.Length == 0 && parameters.defaultMaterials.Length > 0)
            renderer.material = parameters.defaultMaterials[Random.Range(0, parameters.defaultMaterials.Length - 1)];

        if(parameters.shouldGenerateRigidBodies){
            cell.AddComponent<BoxCollider>();
            cell.AddComponent<Rigidbody>();
        }
        
        // calculate bounds
        bounds.Encapsulate(renderer.bounds);

        yield return null;
    }

    public Bounds Bounds
    {
        get 
        {
            return bounds;
        }
    }

    private void ClearAll()
    {
        if(grid?.Length > 0) 
        {
            for(int row = 0; row < grid.GetLength(0); row++)
            {
                for(int col = 0; col < grid.GetLength(1); col++)
                {
                    if(grid[row, col] != null)
                        DestroyImmediate(grid[row, col]);
                }
            }
        }

        if(transform.childCount > 0)
        {
            foreach(Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        grid = null;
    }
}
