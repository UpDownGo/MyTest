using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "Item", menuName = "DB/Item", order = 1)]
public class DataItem : ScriptableObject
{
    public string[] itemName = new string[81];
    public string[] itemScenario = new string[81];
    public bool[] itemDiaLock = new bool[81];
    public int[] itemCoin = new int[81];
    public int[] itemDia = new int[81];
    public Sprite[] itemImage = new Sprite[81];


    public string[] stageName = new string[27];
    public string[] stageScenario = new string[27];
    public bool[] stageDiaLock = new bool[27];
    public int[] stageCoin = new int[27];
    public int[] stageDia = new int[27];
    //public Sprite[] stageImage = new Sprite[27];


    public string[] chapName = new string[9];
    public string[] chapSkill = new string[9];
    public string[] chapScenario = new string[9];
    public bool[] chapDiaLock = new bool[9];
    public int[] chapCoin = new int[9];
    public int[] chapDia = new int[9];
    // public Sprite[] chapImage = new Sprite[9];

}
    /*
    public ObjectInfo[] itemProperty = new ObjectInfo[81];
    public ObjectInfo[] stageProperty = new ObjectInfo[27];
    public ObjectInfo[] chapProperty = new ObjectInfo[9];
}

[System.Serializable]
public class ObjectInfo
{
    public string strName = "new item";
    public string strScenario = "new Scenario";
    public bool isDiaLock = false;
    public int priceCoin = 100;
    public int priceDia = 100;
    public Sprite imageItem;
}
*/