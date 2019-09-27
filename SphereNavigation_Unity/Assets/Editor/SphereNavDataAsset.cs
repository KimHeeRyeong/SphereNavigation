using UnityEngine;
using UnityEditor;
using ShpereNavigation;

public class SphereNavDataAsset
{
    [MenuItem("Assets/Create/Sphere Navigation Data")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<SphereNavData> ();
    }
}
