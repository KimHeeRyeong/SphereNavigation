using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SphereDataCreator : EditorWindow
{
    Mesh mesh;

    [MenuItem("Window/Sphere Data Creator")]
    static void Init() {
        SphereDataCreator window = 
            (SphereDataCreator)EditorWindow.GetWindow(typeof(SphereDataCreator));
    }
    private void OnGUI()
    {
        EditorGUILayout.ObjectField(mesh, typeof(Mesh), true);
        if (GUILayout.Button("Bake"))
        {
            
        }
    }
    private void Update()
    {
        
    }
}
