using UnityEngine;

[CreateAssetMenu(fileName = "ProceduralParams", menuName = "Procedural/Parameters", order = 0)]
public class ProceduralOptions : ScriptableObject 
{
    [SerializeField,Range(1,200)]
    private int width = 1;
    
}