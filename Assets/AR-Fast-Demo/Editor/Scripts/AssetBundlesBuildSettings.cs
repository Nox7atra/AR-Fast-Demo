using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetBundleBuildSettings", menuName = "Asset Bundles/AssetBundleBuildSettings")]
public class AssetBundlesBuildSettings : ScriptableObject
{
    public Object AssetBundleDirectoryObject;
    public string AssetBundleDirectory => AssetDatabase.GetAssetPath(AssetBundleDirectoryObject);
    public BuildTarget[] Platforms;
    public string[] AssetBundleNamesToBuild;
}
