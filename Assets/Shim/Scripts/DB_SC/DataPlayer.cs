using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "User", menuName = "DB/Player", order = 2)]

public class DataPlayer : ScriptableObject
{
    public int currentCoin;
    public int currentDia;
    public float boxSpeed;

    public int[] hitPoint = new int[4];

    public bool[] itemIsBuy = new bool[81];
    public bool[] itemLock = new bool[81];

    public bool[] stageIsBuy = new bool[27];
    public bool[] stageLock = new bool[27];

    public bool[] chapIsBuy = new bool[9];
    public bool[] chapLock = new bool[9];
}
