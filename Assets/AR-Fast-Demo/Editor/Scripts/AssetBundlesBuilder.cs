using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetBundlesBuilder 
{
    
    public static void BuildAllAssetBundles(AssetBundlesBuildSettings settings)
    {
        BuildCustomAssetBundles(
            settings.AssetBundleDirectory,
            null,
            settings.Platforms);
    }
    
    public static void BuildSpecifiedAssetBundles(AssetBundlesBuildSettings settings)
    {
        BuildCustomAssetBundles(
            settings.AssetBundleDirectory,
            settings.AssetBundleNamesToBuild,
            settings.Platforms);
    }
    private static void BuildCustomAssetBundles(
        string path,
        string[] assetBundleNames,
        BuildTarget[] platforms)
    {
        if(platforms == null)
        {
            Debug.LogError("Set at least one platform!");
            return;
        };
        
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var builds = new List<AssetBundleBuild>();
        if (assetBundleNames != null && assetBundleNames.Length != 0)
        {
            assetBundleNames = assetBundleNames.Distinct().ToArray();

            foreach (var assetBundle in assetBundleNames)
            {
                var assetPaths = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundle);
                var build = new AssetBundleBuild
                {
                    assetBundleName = assetBundle,
                    assetNames = assetPaths
                };
                builds.Add(build);
                Debug.Log($"[Asset Bundles] Build bundle: {build.assetBundleName}");
            }
        }
        for (int i = 0; i < platforms.Length; i++)
        {
            var platform = platforms[i];
            BuildAssetBundlesForTarget(path, platform, GetPlatformDirectory(platform),builds.ToArray());
        }
    }
    private static void BuildAssetBundlesForTarget(string path, BuildTarget target, string targetPath, AssetBundleBuild[] bundles = null)
    {
        var directory = Path.Combine(path, targetPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (bundles == null || bundles.Length == 0)
        {
            BuildPipeline.BuildAssetBundles(directory, BuildAssetBundleOptions.None, target);
        }
        else
        {
            BuildPipeline.BuildAssetBundles(directory, bundles.ToArray(), BuildAssetBundleOptions.None, target);
        }   
    }
    public static string GetPlatformDirectory(BuildTarget target)
    {
        switch (@target)
        {
            default:
                return "standalone";
            case BuildTarget.Android:
                return "android";
            case BuildTarget.iOS:
                return "ios";
            case BuildTarget.StandaloneWindows:
                return "standalone";
            case BuildTarget.StandaloneWindows64:
                return "standalone64";
        }
    }
}
