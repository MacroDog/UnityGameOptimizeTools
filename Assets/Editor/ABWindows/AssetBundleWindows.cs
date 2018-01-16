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
    private const string AB_BUILD_OUTPUT_DIR = "/BuildAB/";
    public static string FullAbBuildOutPutDir {
        get {
            string ret = Application.dataPath + AB_BUILD_OUTPUT_DIR + EditorUserBuildSettings.activeBuildTarget + "/";
            if (!Directory.Exists(ret))
            {
                Directory.CreateDirectory(ret);
            }
            return ret;
        }
    }

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
        EditorGUILayout.PropertyField(_toBuildComProperty,true);
        GUILayout.Label("不压缩构建");
        EditorGUILayout.PropertyField(_toBuildNoComProperty,true);
        GUILayout.EndScrollView();
        serialzedObject.ApplyModifiedProperties();
        if (GUILayout.Button("构建"))
        {
            for (int i = 0; i < ToBuildCommpressed.Length; i++)
            {
                EditorUtility.DisplayProgressBar("object", ToBuildCommpressed[i].name, (float)i / ToBuildCommpressed.Length);

                Debug.Log("show");
                bool result = BuildABCom(ToBuildCommpressed[i], true);
                if (!result)
                {
                    EditorUtility.DisplayDialog("error", "构建出错" + ToBuildCommpressed[i].name, "ok");
                    return;
                }
            }
            for (int i = 0; i < ToBuildNoCompressed.Length; i++)
            {

                EditorUtility.DisplayProgressBar("object", ToBuildNoCompressed[i].name, (float)i / ToBuildNoCompressed.Length);
                
                    Debug.Log("show");
                    bool result = BuildABCom(ToBuildNoCompressed[i], false);
                    if (!result)
                    {
                        EditorUtility.DisplayDialog("error", "构建出错" + ToBuildNoCompressed[i].name, "ok");
                        return;
                    }
                
            }
            EditorUtility.ClearProgressBar();
            if (!Directory.Exists(FullAbBuildOutPutDir))
            {
                Directory.CreateDirectory(FullAbBuildOutPutDir);
            }
            EditorUtility.RevealInFinder(FullAbBuildOutPutDir);
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
        Debug.Log("Build");
        AssetBundleBuild b1 = new AssetBundleBuild()
        {
            assetBundleName = GetBundleName(obj),
            assetNames = new string[] { AssetDatabase.GetAssetPath(obj) },
        };
        var bo = isCom ?  BuildAssetBundleOptions.ForceRebuildAssetBundle:BuildAssetBundleOptions.UncompressedAssetBundle;
        var manifest = BuildPipeline.BuildAssetBundles(FullAbBuildOutPutDir, new AssetBundleBuild[] { b1 }, BuildAssetBundleOptions.None,EditorUserBuildSettings.activeBuildTarget);
        if (manifest != null) {
            var path = FullAbBuildOutPutDir + b1.assetBundleName.ToLower();
            if (File.Exists(path)) {
                var tPath = FullAbBuildOutPutDir + Path.GetFileNameWithoutExtension(b1.assetBundleName);
                if (File.Exists(tPath)) {
                    File.Delete(tPath);
                }
                Debug.Log(path);
               // File.Move(path,tPath);//Unity会自动转为小写
                return true;
            }
        }
        return false;
    }
    private static string GetBundleName(Object obj)
    {
        var path = AssetDatabase.GetAssetPath(obj);
       var fileName = path.Replace("/", "!");//这个是为了不同平台做的改动
        return fileName;
    }
}
