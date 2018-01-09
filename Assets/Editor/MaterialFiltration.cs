using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MaterialFiltration : Editor {
    static string path = ""; 
[MenuItem("XYFOptimizeTools/Genery")]
    static void FiltratiionMaterial()
    {
        Dictionary<string, string> dicMatrial = new Dictionary<string, string>();
        MeshRenderer[] meshRenderers = Resources.FindObjectsOfTypeAll<MeshRenderer>();
        string rootPath = Directory.GetCurrentDirectory();
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            MeshRenderer meshrender = meshRenderers[i];
            Material[] newMatirals = new Material[meshrender.sharedMaterials.Length];
            for (int j = 0; j < meshrender.sharedMaterials.Length; j++)
            {
                Material m = meshrender.sharedMaterials[j];
                string mPath = AssetDatabase.GetAssetPath(m);
                if (!string.IsNullOrEmpty(mPath)&&mPath.Contains("Asset/"))
                {

                }
            }
        }
    }
    static bool Compare(Material m1 ,Material m2)
    {
        EditorSettings.serializationMode = SerializationMode.ForceText;
        string m1path = AssetDatabase.GetAssetPath(m1);
        string m2path = AssetDatabase.GetAssetPath(m2);
        if (!string .IsNullOrEmpty(m1path)&&!string.IsNullOrEmpty(m2path))
        {
            string rootPath = Directory.GetCurrentDirectory();
            string text1 = File.ReadAllText(m1path).Replace("m_Name:" + m1.name, "");
            string text2 = File.ReadAllText(m2path).Replace("m_Name:" + m2.name, "");
            return (text1 == text2);
        }
    }
}
