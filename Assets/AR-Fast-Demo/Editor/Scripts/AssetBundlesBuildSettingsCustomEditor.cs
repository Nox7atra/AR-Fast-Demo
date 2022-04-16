
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(AssetBundlesBuildSettings))]
public class AssetBundlesBuildSettingsCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(40);
        if (GUILayout.Button("Build All"))
        {
            var settings = target as AssetBundlesBuildSettings;
            AssetBundlesBuilder.BuildAllAssetBundles(settings);
        }
        GUILayout.Space(40);
        if (GUILayout.Button("Build From Names"))
        {
            var settings = target as AssetBundlesBuildSettings;
            AssetBundlesBuilder.BuildSpecifiedAssetBundles(settings);
        }
    }
}
