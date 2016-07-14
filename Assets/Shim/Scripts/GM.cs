using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GM : MonoBehaviour {
    //bool bOnMainScreen;

    public GameObject canvasBall;
    public GameObject canvasShake;
    public GameObject canvasMarry;
    public GameObject canvasItem;
    public GameObject canvasLottery;
    public GameObject canvasCenter;




    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    ///////////  canvas on/off  ///////////
    public void MainCanvas()
    {
        canvasBall.SetActive(false);
        canvasShake.SetActive(false);
        canvasMarry.SetActive(false);
        canvasItem.SetActive(false);
        canvasLottery.SetActive(false);
        canvasCenter.SetActive(true);
        //bOnMainScreen = true;
    }

    // 강화
    public void OnBallUpCanvas()
    {
        canvasBall.SetActive(!canvasBall.active);
        canvasShake.SetActive(false);
        canvasMarry.SetActive(false);
        canvasItem.SetActive(false);
        canvasLottery.SetActive(false);
        canvasCenter.SetActive(true);


        //bOnMainScreen = false;
    }

    // 쉐이킷
    public void OnShakeCanvas()
    {
        canvasShake.SetActive(true);
        canvasBall.SetActive(false);
        canvasMarry.SetActive(false);
        canvasItem.SetActive(false);
        canvasLottery.SetActive(false);
        canvasCenter.SetActive(false);
        //bOnMainScreen = false;
    }

    //결혼하자
    public void OnMarryCanvas()
    {
        canvasMarry.SetActive(!canvasMarry.active);
        canvasBall.SetActive(false);
        canvasShake.SetActive(false);
        canvasItem.SetActive(false);
        canvasLottery.SetActive(false);
        canvasCenter.SetActive(true);

        // bOnMainScreen = false;
    }

    // 아이템
    public void OnItemCanvas()
    {
        canvasItem.SetActive(!canvasItem.active);
        canvasBall.SetActive(false);
        canvasShake.SetActive(false);
        canvasMarry.SetActive(false);
        canvasLottery.SetActive(false);
        canvasCenter.SetActive(true);

        // bOnMainScreen = false;
    }

    // 로또
    public void OnLotteryCanvas()
    {
        canvasBall.SetActive(false);
        canvasShake.SetActive(false);
        canvasMarry.SetActive(false);
        canvasItem.SetActive(false);
        canvasLottery.SetActive(!canvasLottery.active);
        canvasCenter.SetActive(true);


        // bOnMainScreen = false;
    }


    // 닫기
    public void OffBasic(GameObject obj)
    {
        obj.SetActive(false);
    }
    // 열기
    public void OnBasic(GameObject obj)
    {
        obj.SetActive(true);
    }
}
