/*
using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/Data_Obj")]
    public static void CreateMyAsset()
    {
        DataItem itemdata = ScriptableObject.CreateInstance<DataItem>();
        DataPlayer userdata = ScriptableObject.CreateInstance<DataPlayer>();
        DataScreem chapterdata = ScriptableObject.CreateInstance<DataScreem>();

        AssetDatabase.CreateAsset(itemdata, "Assets/NewItemData.asset");
        AssetDatabase.CreateAsset(userdata, "Assets/NewUserData.asset");
        AssetDatabase.CreateAsset(chapterdata, "Assets/NewScreenData.asset");

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = itemdata;
        Selection.activeObject = userdata;
        Selection.activeObject = chapterdata;
    }
}
*/