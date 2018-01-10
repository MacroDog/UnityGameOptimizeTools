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
        GameObject scense = GameObject.Find("sceneMap");
        XmlDocument xmlDocument= new XmlDocument();
        XmlElement xmlElm= xmlDocument.CreateElement("Scene");
        xmlElm.SetAttribute("Name", SceneManager.GetActiveScene().name);
        if (scense!=null) {
            for (int i = 0; i < scense.transform.childCount; i++) {
                Transform child = scense.transform.GetChild(i);
                getChildAndRecord(child);
            }
        }
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="parent"></param>
    public static void  getChildAndRecord(Transform parent) {
        
    }
}
