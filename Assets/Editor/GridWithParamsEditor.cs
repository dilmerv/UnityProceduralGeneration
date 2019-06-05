using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridWithParams))]
public class GridWithParamsEditor : Editor
{
    private GridWithParams gridWithParams;
    private Editor editor;

    private void OnEnable()
    {
        gridWithParams = (GridWithParams)target;
    }

    public override void OnInspectorGUI()
    {
        if (DrawDefaultInspector())
        {
            gridWithParams.BuildGrid();
        }

        DrawSettingsEditor(gridWithParams.parameters, gridWithParams.BuildGrid, true, ref editor);
    }
    
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, bool foldout, ref Editor editor)
    {
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (onSettingsUpdated != null)
                        {
                            onSettingsUpdated();
                        }
                    }
                }
            }
        }
    }
}