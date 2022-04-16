using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetBundleBuildSettings", menuName = "Asset Bundles/AssetBundleBuildSettings")]
public class AssetBundlesBuildSettings : ScriptableObject
{
    public string AssetBundleDirectory = "test";
    public BuildTarget[] Platforms;
    public string[] AssetBundleNamesToBuild;
}
