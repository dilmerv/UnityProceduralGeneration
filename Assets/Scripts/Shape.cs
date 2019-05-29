using UnityEngine;

public abstract class Shape
{   
    public ShapeTypes ShapeType { get;set; }
    
    public float Width { get;set; }

    public float Height { get;set; }

    public float Depth { get;set; }

    public abstract Mesh Generate();
}