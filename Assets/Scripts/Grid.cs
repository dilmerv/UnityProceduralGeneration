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

    private float constructionDelay = 1.0f;

    private GameObject[,] grid = null;

    [SerializeField,Range(1,200)]
    private float shapeWidth = 2.0f;

    [SerializeField,Range(1,200)]
    private float shapeHeight = 2.0f;

    [SerializeField,Range(1,200)]
    private float shapeDepth = 2.0f;

    [SerializeField,Range(1,200)]
    private float maxRandomHeight = 10;

    [SerializeField, Range(1,100)]
    private int randomSeed = 1;
    private int prevSeed = 1;

    [SerializeField]
    private ShapeTypes ShapeType = ShapeTypes.Cube;

    [SerializeField]
    private string shaderName = "Lightweight Render Pipeline/Lit";

    [SerializeField]
    private bool applyProceduralMaterial = false;

    [SerializeField]
    private Material defaultMaterial = null;

    #region UI Bindings

    [SerializeField]
    private Text numOfShapesText = null;

    [SerializeField]
    private Text lengthOfTimeText = null;

    #endregion
    void Start()
    {    
        prevWidth = width;
        prevHeight = height;

        Random.InitState(randomSeed);
        grid = new GameObject[height, width];       

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

        cell.transform.position = 
            new Vector3(shapeWidth * row,
                        shapeHeight * Random.Range(1.0f, maxRandomHeight),
                        shapeDepth * col);

        MeshFilter meshFilter = cell.AddComponent<MeshFilter>();
        MeshRenderer renderer = cell.AddComponent<MeshRenderer>();

        Cube cube = new Cube {
            Width = shapeWidth,
            Height = shapeHeight,
            Depth = shapeDepth
        };
        
        meshFilter.mesh = cube.Generate();
        
        if(applyProceduralMaterial)
            renderer.ApplyRandomMaterial(shaderName, cell.name);
        else
            renderer.material = defaultMaterial;

        yield return null;
    }

    void Update()
    {
        if(prevSeed != randomSeed)
        {
            BuildGrid();
            prevSeed = randomSeed;
        }

        if(prevWidth != width || prevHeight != height)
        {
            ClearAll();
            
            prevWidth = width;
            prevHeight = height;
            grid = new GameObject[height, width];
            
            BuildGrid();
        }
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
