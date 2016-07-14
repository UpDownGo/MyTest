using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockSystem : MonoBehaviour {
    Sprite unlockSprite;
    DataPlayer userData;

    GameObject[] itemObj;
    GameObject[] stageObj;
    GameObject[] chapObj;

	// Use this for initialization
    void Start()
    {
        itemObj = GameDataManager.Instance.itemObj;
        stageObj = GameDataManager.Instance.stageObj;
        chapObj = GameDataManager.Instance.chapObj;
        userData = GameDataManager.Instance.userData;
        unlockSprite = GameDataManager.Instance.unlockSprite;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // 아이템 구입시 스테이지 락 해제 확인
    public void WhenBuyItem(int itemIndex)
    {
        int stageIndex = itemIndex / 3;
        int checkItme = stageIndex;

        // 아이템 구입이 완료 되었는지 확인
        if (userData.itemIsBuy[checkItme] == false) return;
        if (userData.itemIsBuy[checkItme + 1] == false) return;
        if (userData.itemIsBuy[checkItme + 2] == false) return;

        UnlockStage(stageIndex);
    }
 
    // 스테이지 구입 시 아이템 락 해제
    public void WhenBuyStage(int stageIndex)
    {
        int subItemIndex = (stageIndex + 1) * 3;

        // 챕터 락을 해제 될 경우 아이템 락 통과
        if (stageIndex % 3 == 2)
        {
            int index = stageIndex / 3;
            UnlockChapter(index);
            return;
        }
     
        UnlockItem(subItemIndex);
        UnlockItem(subItemIndex + 1);
        UnlockItem(subItemIndex + 2);
    }    

    // 챕터 구입 시 아이템 락 해제
    public void WhenBuyChapter(int chapIndex)
    {
        int index = (chapIndex + 1) * 9;
        UnlockItem(index);
        UnlockItem(index + 1);
        UnlockItem(index + 2);
    }

    // 락 해제
    void UnlockItem(int itemIndex )
    {
        userData.itemLock[itemIndex] = false;
        itemObj[itemIndex].GetComponent<Image>().sprite = unlockSprite;
        itemObj[itemIndex].transform.FindChild("Item_Image").GetComponent<Image>().enabled = true;
        itemObj[itemIndex].transform.FindChild("Item_Image").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[itemIndex];
    }
    void UnlockStage(int stageIndex)
    {
        userData.stageLock[stageIndex] = false;
        stageObj[stageIndex].GetComponent<Image>().sprite = unlockSprite;
        stageObj[stageIndex].transform.FindChild("Text").GetComponent<Text>().enabled = true;
        stageObj[stageIndex].transform.FindChild("Text").GetComponent<Text>().text = GameDataManager.Instance.itemData.stageName[stageIndex];
    }
    void UnlockChapter(int chapIndex)
    {
        userData.chapLock[chapIndex] = false;
        chapObj[chapIndex].transform.FindChild("Chapter_Btn").transform.FindChild("Chapter_Text").GetComponent<Text>().text = GameDataManager.Instance.itemData.chapName[chapIndex];
        chapObj[chapIndex].transform.FindChild("Chapter_Btn").GetComponent<Image>().sprite = unlockSprite;
    }

}

