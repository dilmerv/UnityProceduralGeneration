using UnityEngine;

public class GridLine : MonoBehaviour
{
    [SerializeField]
    private Color color = new Color(1, 0, 0, 0.5f);

    [SerializeField]
    private GridWithParams gridWithParams;

    [SerializeField]
    private Vector3 gridOffset = Vector3.zero;

    public enum DrawOptions
    {
        Selection,
        Always
    }

    [SerializeField]
    private DrawOptions Options = DrawOptions.Always;

    void OnDrawGizmos() 
    {
        if(Options == DrawOptions.Always && gridWithParams != null)
        {
            Gizmos.color = color;
            Gizmos.DrawCube(gridWithParams.Bounds.center, gridWithParams.Bounds.size + gridOffset);
        }
    }

    void OnDrawGizmosSelected() 
    {
        if(Options == DrawOptions.Selection && gridWithParams != null)
        {
            Gizmos.color = color;
            Gizmos.DrawCube(gridWithParams.Bounds.center, gridWithParams.Bounds.size + gridOffset);
        }
    }
}