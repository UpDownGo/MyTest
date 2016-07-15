using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Dir { East, West, North, South }

public class ScreenSystem : MonoBehaviour {
    [System.Serializable]
    public class ItemBtn
    {
        public GameObject[] itemButton;

    }

    public GameObject objChapter;

    public GameObject mainSprite;
    
    public GameObject[] moveButton;
    public ItemBtn[] itemButton;
    public DataScreem screendata;

    ScreenInfo currChapter = null;

    void Awake()
    {
        if (!screendata)
        {
            screendata = Resources.Load("ScreenData") as DataScreem;
        }

        // 챕터 연결 정보 설정
        screendata.chapterInfo[0].dirInfo[(int)Dir.East] = null;
        screendata.chapterInfo[0].dirInfo[(int)Dir.West] = screendata.chapterInfo[6];
        screendata.chapterInfo[0].dirInfo[(int)Dir.North] = null;
        screendata.chapterInfo[0].dirInfo[(int)Dir.South] = screendata.chapterInfo[1];

        screendata.chapterInfo[1].dirInfo[(int)Dir.East] = screendata.chapterInfo[2];
        screendata.chapterInfo[1].dirInfo[(int)Dir.West] = screendata.chapterInfo[4];
        screendata.chapterInfo[1].dirInfo[(int)Dir.North] = screendata.chapterInfo[0];
        screendata.chapterInfo[1].dirInfo[(int)Dir.South] = screendata.chapterInfo[7];

        screendata.chapterInfo[2].dirInfo[(int)Dir.East] = screendata.chapterInfo[3];
        screendata.chapterInfo[2].dirInfo[(int)Dir.West] = screendata.chapterInfo[1];
        screendata.chapterInfo[2].dirInfo[(int)Dir.North] = null;
        screendata.chapterInfo[2].dirInfo[(int)Dir.South] = null;

        screendata.chapterInfo[3].dirInfo[(int)Dir.East] = null;
        screendata.chapterInfo[3].dirInfo[(int)Dir.West] = screendata.chapterInfo[2];
        screendata.chapterInfo[3].dirInfo[(int)Dir.North] = null;
        screendata.chapterInfo[3].dirInfo[(int)Dir.South] = null;

        screendata.chapterInfo[4].dirInfo[(int)Dir.East] = screendata.chapterInfo[1];
        screendata.chapterInfo[4].dirInfo[(int)Dir.West] = null;
        screendata.chapterInfo[4].dirInfo[(int)Dir.North] = screendata.chapterInfo[6];
        screendata.chapterInfo[4].dirInfo[(int)Dir.South] = screendata.chapterInfo[5];

        screendata.chapterInfo[5].dirInfo[(int)Dir.East] = screendata.chapterInfo[7];
        screendata.chapterInfo[5].dirInfo[(int)Dir.West] = null;
        screendata.chapterInfo[5].dirInfo[(int)Dir.North] = screendata.chapterInfo[4];
        screendata.chapterInfo[5].dirInfo[(int)Dir.South] = null;

        screendata.chapterInfo[6].dirInfo[(int)Dir.East] = screendata.chapterInfo[0];
        screendata.chapterInfo[6].dirInfo[(int)Dir.West] = null;
        screendata.chapterInfo[6].dirInfo[(int)Dir.North] = null;
        screendata.chapterInfo[6].dirInfo[(int)Dir.South] = screendata.chapterInfo[4];

        screendata.chapterInfo[7].dirInfo[(int)Dir.East] = null;
        screendata.chapterInfo[7].dirInfo[(int)Dir.West] = screendata.chapterInfo[5];
        screendata.chapterInfo[7].dirInfo[(int)Dir.North] = screendata.chapterInfo[1];
        screendata.chapterInfo[7].dirInfo[(int)Dir.South] = screendata.chapterInfo[8];

        screendata.chapterInfo[8].dirInfo[(int)Dir.East] = null;
        screendata.chapterInfo[8].dirInfo[(int)Dir.West] = null;
        screendata.chapterInfo[8].dirInfo[(int)Dir.North] = screendata.chapterInfo[7];
        screendata.chapterInfo[8].dirInfo[(int)Dir.South] = null;

        for(int i = 0; i < 9; i++)
        {
            screendata.chapterInfo[i].dir[0] = false;
            screendata.chapterInfo[i].dir[1] = false;
            screendata.chapterInfo[i].dir[2] = false;
            screendata.chapterInfo[i].dir[3] = false;
        }

        currChapter = screendata.chapterInfo[0];    
    }

    void Start()
    {

        // OpenGate((int)Dir.South, screendata.chapterInfo[0], screendata.chapterInfo[1]);
        for (int i = 0; i < 9; i++)
        {
            if (GameDataManager.Instance.userData.chapIsBuy[i])
            {
                WhenBuyChapter(i);
            }
        }

        SetScreen(currChapter);

    }

    public void HomeScreen()
    {
        SetScreen(currChapter);
    }
    public void InitScreen()
    {
        SetScreen(screendata.chapterInfo[0]);
    }



    void ScreenInit()
    {
        /*
        for (int i = 0; i < 9; i++)
        {
            int j = i * 9;
            screendata.chapterInfo[i].chapObj.transform.FindChild("item").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (1)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 1];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (2)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 2];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (3)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 3];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (4)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 4];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (5)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 5];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (6)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 6];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (7)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 7];
            screendata.chapterInfo[i].chapObj.transform.FindChild("Item (8)").GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j + 8];

        }
        */
    }

    void SetScreen(ScreenInfo currScreen)
    {
        // 메인 이미지 변경
        mainSprite.GetComponent<Image>().sprite = currScreen.chapterImage;

        int itemIndex = currScreen.chapNum * 9;

        // 아이템 이미지와 위치 변경
        for (int i = 0; i < 9; i++)
        {
            // 아이템 스프라이트 입력
            //itemButton[i].GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[itemIndex + i];
            
            // 아이템 위치 입력
           // itemButton[i].GetComponent<RectTransform>().localPosition = currScreen.v_ItemLocation[i];
            
            // 구입여부에 따른 활성화
            //if (GameDataManager.Instance.userData.itemIsBuy[itemIndex + i])
            //    itemButton[i].SetActive(true);
          //  else
           //     itemButton[i].SetActive(false);
        }

       
        // 버튼 활성화 설정
        moveButton[(int)Dir.East].SetActive(currScreen.dir[(int)Dir.East]);
        moveButton[(int)Dir.West].SetActive(currScreen.dir[(int)Dir.West]);
        moveButton[(int)Dir.North].SetActive(currScreen.dir[(int)Dir.North]);
        moveButton[(int)Dir.South].SetActive(currScreen.dir[(int)Dir.South]);
       
    }

    // 이동 함수
    public void MoveNorth()
    {
        currChapter = currChapter.dirInfo[(int)Dir.North];
        SetScreen(currChapter);

    }
    public void MoveSouth()
    {
        currChapter = currChapter.dirInfo[(int)Dir.South];

        SetScreen(currChapter);
    }
    public void MoveEast()
    {
        currChapter = currChapter.dirInfo[(int)Dir.East];

        SetScreen(currChapter);
    }
    public void MoveWest()
    {
        currChapter = currChapter.dirInfo[(int)Dir.West];

        SetScreen(currChapter);
    }


    // 챕터 별 문열리는 순서
    public void WhenBuyChapter(int chapterLevel)
    {
        switch (chapterLevel)
        {
            case 0:
                {
                   OpenGate((int)Dir.South,screendata.chapterInfo[0],screendata.chapterInfo[1]);                   
                    break;
                }
            case 1:
                {
                    OpenGate((int)Dir.East, screendata.chapterInfo[1], screendata.chapterInfo[2]);
                    break;
                }
            case 2:
                {
                    OpenGate((int)Dir.East, screendata.chapterInfo[2], screendata.chapterInfo[3]);
                    break;
                }
            case 3:
                {
                    OpenGate((int)Dir.West, screendata.chapterInfo[1], screendata.chapterInfo[4]);
                    break;
                }
            case 4:
                {
                    OpenGate((int)Dir.South, screendata.chapterInfo[4], screendata.chapterInfo[5]);

                    break;
                }
            case 5:
                {
                    OpenGate((int)Dir.North, screendata.chapterInfo[4], screendata.chapterInfo[6]);

                    break;
                }
            case 6:
                {
                    OpenGate((int)Dir.South, screendata.chapterInfo[1], screendata.chapterInfo[7]);

                    break;
                }
            case 7:
                {
                    OpenGate((int)Dir.South, screendata.chapterInfo[7], screendata.chapterInfo[8]);

                    break;
                }
            default:
                {
                    print("구입 문제 ");
                    print(chapterLevel);

                    break;
                }
        }
        SetScreen(currChapter);
    }
    
    // 아이템을 샀을때
    public void WhenBuyItem(int itemIndex)
    {
        int index = itemIndex % 9;
       // itemButton[index].SetActive(true);
    }

    // 문열기
    void OpenGate(int dir, ScreenInfo currScreen, ScreenInfo nextScreen)
    {
        switch (dir)
        {
            case (int)Dir.East:
                {
                    currScreen.dir[(int)Dir.East] = true;

                    nextScreen.dir[(int)Dir.West] = true;

                    break;
                }
            case (int)Dir.West:
                {
                    currScreen.dir[(int)Dir.West] = true;

                    nextScreen.dir[(int)Dir.East] = true;
                    break;
                }
            case (int)Dir.North:
                {
                    currScreen.dir[(int)Dir.North] = true;

                    nextScreen.dir[(int)Dir.South] = true;
                    break;
                }
            case (int)Dir.South:
                {
                    currScreen.dir[(int)Dir.South] = true;

                    nextScreen.dir[(int)Dir.North] = true;
                    break;
                }
        }
    }
}

