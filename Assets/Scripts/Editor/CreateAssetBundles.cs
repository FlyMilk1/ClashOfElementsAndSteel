using System;
using UnityEngine;
using UnityEditor;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/CreateAssetBundles")]
    private static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = Application.streamingAssetsPath + "/AssetBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch(Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}
