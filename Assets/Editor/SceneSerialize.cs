using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class SceneSerialize : Editor {
    public string path = Application.dataPath + "/Resources/ScenseMap";

    [MenuItem("XYFOptimizeTools/SaveScene")]
     static void saveScene() {
        GameObject scene = GameObject.Find("scene");
        XmlDocument xmlDocument= new XmlDocument();
        XmlElement xmlElm= xmlDocument.CreateElement("Scene");
        xmlElm.SetAttribute("Name", SceneManager.GetActiveScene().name);
        GameObject[] childs = scene.GetComponentsInChildren<GameObject>();
        for (int i = 0; i < childs.Length;i++)
        {
            XmlElement gameObject = xmlDocument.CreateElement("gameObject");
            if (PrefabUtility.GetPrefabType(childs[i]) == PrefabType.PrefabInstance)  //prefab文件从resources文件夹中加载
            {
                gameObject.SetAttribute("name", childs[i].name);
                gameObject.SetAttribute("assetPass", AssetDatabase.GetAssetPath(childs[i]).Split("Resoutces")[1]);
                gameObject.SetAttribute("Position", childs[i].transform.position.x + "|" + childs[i].transform.position.y + "|"+childs[i].transform.position.z);
                gameObject.SetAttribute("Rotation", childs[i].transform.localRotation.x + "|" + childs[i].transform.localRotation.y + "|" + childs[i].transform.rotation.z);
                gameObject.SetAttribute("Scale", childs[i].transform.localScale.x + "|" + childs[i].transform.localScale.y + "|" + childs[i].transform.localScale.z);

            }
        }
    }
}
