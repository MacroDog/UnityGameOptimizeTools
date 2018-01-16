using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEditor.SceneManagement;
public class MaterialFiltration : Editor
{
    [MenuItem(("XYFOptimizeTools/GenerateMaterial"))]
    public static void filtration()
    {
        Dictionary<string, string> dicMaterial = new Dictionary<string, string>();
        MeshRenderer[] meshRenders = Resources.FindObjectsOfTypeAll<MeshRenderer>();
        string rootPath = Directory.GetCurrentDirectory();
        for (int i = 0; i < meshRenders.Length; i++)
        {
            MeshRenderer meshRender = meshRenders[i];
            EditorUtility.DisplayProgressBar("遍历mesh render", meshRenders[i].name, (float)i / meshRenders.Length);
            Material[] newMaterials = new Material[meshRender.sharedMaterials.Length];
            for (int j = 0; j < newMaterials.Length; j++)
            {
                Material m = meshRender.sharedMaterials[j];
                string mPath = AssetDatabase.GetAssetPath(m);
                string fullPath = Path.Combine(rootPath, mPath);
                if (!string.IsNullOrEmpty(mPath) && mPath.Contains("Assets/"))
                {
                    string text = File.ReadAllText(fullPath).Replace("m_Name: " + m.name, "");
                    string change;
                    if (!dicMaterial.TryGetValue(text, out change))
                    {
                        dicMaterial[text] = mPath;
                        change = mPath;
                    }
                    else
                    {
                        if (mPath != change)
                        {
                            Debug.Log(mPath + " " + change);
                            AssetDatabase.DeleteAsset(mPath);//删除重复的材质
                        }
                    }
                    newMaterials[j] = AssetDatabase.LoadAssetAtPath<Material>(change);


                }
            }
            meshRender.sharedMaterials = newMaterials;
        }
        EditorUtility.ClearProgressBar();
        EditorSceneManager.MarkAllScenesDirty();
    }
}
