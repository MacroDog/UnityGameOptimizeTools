using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class AssetBundleWindows : EditorWindow {

    public Object[] ToBuildCommpressed;
    public Object[] ToBuildNoCompressed;

    private SerializedObject serialzedObject;
    private SerializedProperty _toBuildComProperty;
    private SerializedProperty _toBuildNoComProperty;
    private Vector2 scrollPos;
    public static string path = "Assets/Build"; 
	[MenuItem("XYFTools/AssetBundle")]
    static void ShowWindow()
    {
        GetWindow<AssetBundleWindows>().Show();
    }
    private void OnEnable()
    {
        serialzedObject = new SerializedObject(this);
        _toBuildComProperty = serialzedObject.FindProperty("ToBuildCommpressed");
        _toBuildNoComProperty = serialzedObject.FindProperty("ToBuildNoCompressed");
    }
    private void OnGUI()
    {
        serialzedObject.Update();
        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUILayout.Label("压缩构建");
        EditorGUILayout.PropertyField(_toBuildComProperty);
        GUILayout.Label("不压缩构建");
        EditorGUILayout.PropertyField(_toBuildNoComProperty);
        GUILayout.EndScrollView();
        serialzedObject.ApplyModifiedProperties();
        if (GUILayout.Button("构建"))
        {
            for (int i = 0; i < ToBuildCommpressed.Length; i++)
            {
                bool result = 
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isCom">isCompress</param>
    /// <returns></returns>
    private bool BuildABCom(Object obj,bool isCom)
    {
        AssetBundleBuild b1 = new AssetBundleBuild()
        {
            assetBundleName = GetBundleName(obj),
            assetNames = new string[] { AssetDatabase.GetAssetPath(obj) },
        };
        var bo = isCom ? BuildAssetBundleOptions.CompleteAssets : BuildAssetBundleOptions.None;
        var bd = BuildPipeline.BuildAssetBundles(path, bo,EditorUserBuildSettings.activeBuildTarget);
        if (bd!=null)
        {
            
        }
    }
    private static string GetBundleName(Object obj)
    {
        var path = AssetDatabase.GetAssetPath(obj);
        string dir = path;
        for (int i = 0; i < 100; i++)
        {
            dir = Path.GetDirectoryName(dir);
            if (dir == null)
            {
                break;
            }
            if (dir == Path.GetPathRoot(dir))
            {
                return Path.GetFullPath(path);
            }
        }
        dir += "/";
        var fileName = path.Remove(0, dir.Length);
        fileName = fileName.Replace("/", "|");//这个是为了不同平台做的改动
        return fileName;
    }
}
