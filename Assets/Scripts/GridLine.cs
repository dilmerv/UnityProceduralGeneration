using UnityEngine;

public class GridLine : MonoBehaviour
{
    [SerializeField]
    private Color color = new Color(1, 0, 0, 0.5f);

    private GridWithParams gridWithParams;

    [SerializeField]
    private Vector3 gridOffset = Vector3.zero;

    void OnDrawGizmosSelected()
    {
        gridWithParams = GetComponent<GridWithParams>();
        Gizmos.color = color;
        Gizmos.DrawCube(gridWithParams.Bounds.center, gridWithParams.Bounds.size + gridOffset);
    }
}