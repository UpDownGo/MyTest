using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "Screen", menuName = "DB/Chapter", order = 3)]
public class DataScreem : ScriptableObject
{
    public ScreenInfo[] chapterInfo = new ScreenInfo[9];
}

[System.Serializable]
public class ScreenInfo
{
    public int chapNum;
    public GameObject chapObj;
   // public Vector3[] v_ItemLocation = new Vector3[9];

    // 각 방향의 스크린 레퍼런스 
    [System.NonSerialized]
    public ScreenInfo[] dirInfo = new ScreenInfo[4];
    // 방향이열렸는지
    public bool[] dir = new bool[4];
}
