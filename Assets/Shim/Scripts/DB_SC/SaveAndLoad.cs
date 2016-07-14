using UnityEngine;
using System.Collections;

public class SaveAndLoad : MonoBehaviour
{

    string strItemBuy = "itemBuy";
    string strItemLock = "itemLock";

    string strStageBuy = "stageBuy";
    string strStageLock = "stageLock";

    string strChapBuy = "chapBuy";
    string strChapLock = "chapLock";

    string strCoin = "Coin";
    string strDia = "Dia";

    string strHitPoint = "HitPoint";

    public void SaveDB()
    {
        string itemBuy = "";
        string itemLock = "";

        // 아이템 정보 저장
        for (int i = 0; i < GameDataManager.Instance.itemData.itemName.Length; i++)
        {
            if (i < GameDataManager.Instance.itemData.itemName.Length - 1)
            {
                itemBuy += GameDataManager.Instance.userData.itemIsBuy[i].ToString() + ",";
                itemLock += GameDataManager.Instance.userData.itemLock[i].ToString() + ",";
            }
            else
            {
                itemBuy += GameDataManager.Instance.userData.itemIsBuy[i].ToString();
                itemLock += GameDataManager.Instance.userData.itemLock[i].ToString();
            }
        }
        PlayerPrefs.SetString(strItemBuy, itemBuy);
        PlayerPrefs.SetString(strItemLock, itemLock);



        itemBuy = "";
        itemLock = "";
        // 스테이지 정보 저장
        for (int i = 0; i < GameDataManager.Instance.itemData.stageName.Length; i++)
        {
            if (i < GameDataManager.Instance.itemData.stageName.Length - 1)
            {
                itemBuy += GameDataManager.Instance.userData.stageIsBuy[i].ToString() + ",";
                itemLock += GameDataManager.Instance.userData.stageLock[i].ToString() + ",";
            }
            else
            {
                itemBuy += GameDataManager.Instance.userData.stageIsBuy[i].ToString();
                itemLock += GameDataManager.Instance.userData.stageLock[i].ToString();
            }
        }
        PlayerPrefs.SetString(strStageBuy, itemBuy);
        PlayerPrefs.SetString(strStageLock, itemLock);



        itemBuy = "";
        itemLock = "";
        // 챕터 정보 저장
        for (int i = 0; i < GameDataManager.Instance.itemData.chapName.Length; i++)
        {
            if (i < GameDataManager.Instance.itemData.chapName.Length - 1)
            {
                itemBuy += GameDataManager.Instance.userData.chapIsBuy[i].ToString() + ",";
                itemLock += GameDataManager.Instance.userData.chapLock[i].ToString() + ",";
            }
            else
            {
                itemBuy += GameDataManager.Instance.userData.chapIsBuy[i].ToString();
                itemLock += GameDataManager.Instance.userData.chapLock[i].ToString();
            }
        }
        PlayerPrefs.SetString(strChapBuy, itemBuy);
        PlayerPrefs.SetString(strChapLock, itemLock);


        // 유저 정보
        int coin = 0;
        coin = GameDataManager.Instance.userData.currentCoin;
        int dia = 0;
        dia = GameDataManager.Instance.userData.currentDia;

        PlayerPrefs.SetInt(strCoin, (int)coin);
        PlayerPrefs.SetInt(strDia, (int)dia);

        string hitPoint = "";
        for (int i = 0; i < GameDataManager.Instance.userData.hitPoint.Length; i++)
        {
            if (i < GameDataManager.Instance.userData.hitPoint.Length - 1)
                hitPoint += GameDataManager.Instance.userData.hitPoint[i].ToString() + ",";
            else
                hitPoint += GameDataManager.Instance.userData.hitPoint[i].ToString();
        }


    }


    public void LoadDB()
    {
        // 아이템 로드
        if (PlayerPrefs.GetString(strItemBuy) != "")
        {
            string[] itemBuy;
            string[] itemLock;

            itemBuy = PlayerPrefs.GetString(strItemBuy).Split(',');
            itemLock = PlayerPrefs.GetString(strItemLock).Split(',');

            print("Item IsBuy : " + PlayerPrefs.GetString(strItemBuy) + " \n\n");
            print("Item Lock : " + PlayerPrefs.GetString(strItemLock) + " \n\n");

            for (int i = 0; i < GameDataManager.Instance.itemData.itemName.Length; i++)
            {
                GameDataManager.Instance.userData.itemIsBuy[i] = System.Convert.ToBoolean(itemBuy[i]);
                GameDataManager.Instance.userData.itemLock[i] = System.Convert.ToBoolean(itemLock[i]);
            }
        }


        // 스테이지 로드
        if (PlayerPrefs.GetString(strStageBuy) != "")
        {
            string[] stageBuy;
            string[] stageLock;

            stageBuy = PlayerPrefs.GetString(strStageBuy).Split(',');
            stageLock = PlayerPrefs.GetString(strStageLock).Split(',');

            print("Stage IsBuy : " + PlayerPrefs.GetString(strStageBuy) + " \n\n");
            print("Stage Lock : " + PlayerPrefs.GetString(strStageLock) + " \n\n");

            for (int i = 0; i < GameDataManager.Instance.itemData.stageName.Length; i++)
            {
                GameDataManager.Instance.userData.stageIsBuy[i] = System.Convert.ToBoolean(stageBuy[i]);
                GameDataManager.Instance.userData.stageLock[i] = System.Convert.ToBoolean(stageLock[i]);
            }
        }

        // 챕터 로드
        if (PlayerPrefs.GetString(strChapBuy) != "")
        {
            string[] chapBuy;
            string[] chapLock;

            chapBuy = PlayerPrefs.GetString(strChapBuy).Split(',');
            chapLock = PlayerPrefs.GetString(strChapLock).Split(',');

            for (int i = 0; i < GameDataManager.Instance.itemData.chapName.Length; i++)
            {
                GameDataManager.Instance.userData.chapIsBuy[i] = System.Convert.ToBoolean(chapBuy[i]);
                GameDataManager.Instance.userData.chapLock[i] = System.Convert.ToBoolean(chapLock[i]);
            }
        }



        // 유저 정보 로드
        GameDataManager.Instance.userData.currentCoin = PlayerPrefs.GetInt(strCoin);
        GameDataManager.Instance.userData.currentDia = PlayerPrefs.GetInt(strDia);

        print(PlayerPrefs.GetInt(strCoin));
        print(PlayerPrefs.GetInt(strDia));


        if (PlayerPrefs.GetString(strHitPoint) != "")
        {
            string[] hitPoint;

            hitPoint = PlayerPrefs.GetString(strHitPoint).Split(',');

            for (int i = 0; i < GameDataManager.Instance.userData.hitPoint.Length; i++)
            {
                GameDataManager.Instance.userData.hitPoint[i] = System.Convert.ToInt32(hitPoint[i]);
            }
        }

    }
}
