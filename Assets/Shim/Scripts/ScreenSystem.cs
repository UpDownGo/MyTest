using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Dir { East, West, North, South }

public class ScreenSystem : MonoBehaviour {
    public GameObject superChapter;
    public GameObject[] moveButton;
    public DataScreem screendata;

    int currentPage;

    GameObject[] objChapter = new GameObject[9];
    Image[] itemImage = new Image[9];



    ScreenInfo currChapter = null;

    void Awake()
    {
        if (!screendata)
        {
            screendata = Resources.Load("NewScreenData") as DataScreem;
        }

        InitDir();
        InitChapterOBJ();
        InitItemImage();
    }

    // 방향 설정
    void InitDir()
    {  
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

        for (int i = 0; i < 9; i++)
        {
            screendata.chapterInfo[i].dir[0] = false;
            screendata.chapterInfo[i].dir[1] = false;
            screendata.chapterInfo[i].dir[2] = false;
            screendata.chapterInfo[i].dir[3] = false;
        }

    }

    // 챕터 Obj 생성
    void InitChapterOBJ()
    {

        for(int i = 0; i < 9; i++)
        {
            objChapter[i] = Instantiate(screendata.chapterInfo[i].chapObj);
            objChapter[i].transform.SetParent(superChapter.transform);
            objChapter[i].GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
            objChapter[i].GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }


       /*
        objChapter[0] = superChapter.transform.FindChild("Chapter01").gameObject;
        objChapter[1] = superChapter.transform.FindChild("Chapter02").gameObject;
        objChapter[2] = superChapter.transform.FindChild("Chapter03").gameObject;
        objChapter[3] = superChapter.transform.FindChild("Chapter04").gameObject;
        objChapter[4] = superChapter.transform.FindChild("Chapter05").gameObject;
        objChapter[5] = superChapter.transform.FindChild("Chapter06").gameObject;
        objChapter[6] = superChapter.transform.FindChild("Chapter07").gameObject;
        objChapter[7] = superChapter.transform.FindChild("Chapter08").gameObject;
        objChapter[8] = superChapter.transform.FindChild("Chapter09").gameObject;

        */
    }


    // 챕터 아이템 이미지 데이터 입력
    void InitItemImage()
    {
        for (int i = 0; i < 9; i++)
        {
            int j = i * 9;
            objChapter[i].transform.FindChild("Item 1-1").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j];
            objChapter[i].transform.FindChild("Item 1-2").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 1];
            objChapter[i].transform.FindChild("Item 1-3").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 2];
            objChapter[i].transform.FindChild("Item 2-1").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 3];
            objChapter[i].transform.FindChild("Item 2-2").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 4];
            objChapter[i].transform.FindChild("Item 2-3").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 5];
            objChapter[i].transform.FindChild("Item 3-1").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 6];
            objChapter[i].transform.FindChild("Item 3-2").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 7];
            objChapter[i].transform.FindChild("Item 3-3").gameObject.GetComponent<Image>().sprite = GameDataManager.Instance.itemData.itemImage[j+ 8];

            objChapter[i].transform.FindChild("Item 1-1").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 1-2").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 1-3").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 2-1").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 2-2").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 2-3").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 3-1").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 3-2").gameObject.GetComponent<Image>().enabled = false;
            objChapter[i].transform.FindChild("Item 3-3").gameObject.GetComponent<Image>().enabled = false;

            objChapter[i].SetActive(false);
        }
    }


    void Start()
    {
        
        // 스크린 설정
        currChapter = screendata.chapterInfo[0];        
        
        // 스크린 방항 정보 셋팅
        for (int i = 0; i < 9; i++)
        {
            if (GameDataManager.Instance.userData.chapIsBuy[i])            
                WhenBuyChapter(i);                    
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


    void SetScreen(ScreenInfo screenInfo)
    {
        // 메인 스테이지 엑티브

        for (int i = 0; i < objChapter.Length; i++)
        {
            if (screendata.chapterInfo[i].chapObj == screenInfo.chapObj)
            {
                objChapter[i].SetActive(true);
               // print("t :" + i);
                currentPage = i;
            }

            else
            {
                objChapter[i].SetActive(false);
               // print("f :" + i);

            }
        }

        ItemUpdate();

        
        // 버튼 활성화 설정
        moveButton[(int)Dir.East].SetActive(screenInfo.dir[(int)Dir.East]);
        moveButton[(int)Dir.West].SetActive(screenInfo.dir[(int)Dir.West]);
        moveButton[(int)Dir.North].SetActive(screenInfo.dir[(int)Dir.North]);
        moveButton[(int)Dir.South].SetActive(screenInfo.dir[(int)Dir.South]);

        // 현재 페이지 저장
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
    public void WhenBuyItem()
    {
        ItemUpdate();
    }

    // 아이템 갱신
    void ItemUpdate()
    {

        int itemIndex = currentPage * 9;

        // 아이템 관리
        itemImage[0] = objChapter[currentPage].transform.FindChild("Item 1-1").gameObject.GetComponent<Image>();
        itemImage[1] = objChapter[currentPage].transform.FindChild("Item 1-2").gameObject.GetComponent<Image>();
        itemImage[2] = objChapter[currentPage].transform.FindChild("Item 1-3").gameObject.GetComponent<Image>();
        itemImage[3] = objChapter[currentPage].transform.FindChild("Item 2-1").gameObject.GetComponent<Image>();
        itemImage[4] = objChapter[currentPage].transform.FindChild("Item 2-2").gameObject.GetComponent<Image>();
        itemImage[5] = objChapter[currentPage].transform.FindChild("Item 2-3").gameObject.GetComponent<Image>();
        itemImage[6] = objChapter[currentPage].transform.FindChild("Item 3-1").gameObject.GetComponent<Image>();
        itemImage[7] = objChapter[currentPage].transform.FindChild("Item 3-2").gameObject.GetComponent<Image>();
        itemImage[8] = objChapter[currentPage].transform.FindChild("Item 3-3").gameObject.GetComponent<Image>();

        for (int i = 0; i < 9; i++)
        {
            // 구입여부에 따른 활성화
            if (GameDataManager.Instance.userData.itemIsBuy[itemIndex + i])
                itemImage[i].enabled = true;
            else
                itemImage[i].enabled = false;
        }

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

                    currChapter = nextScreen;

                    break;
                }
            case (int)Dir.West:
                {
                    currScreen.dir[(int)Dir.West] = true;

                    nextScreen.dir[(int)Dir.East] = true;

                    currChapter = nextScreen;

                    break;
                }
            case (int)Dir.North:
                {
                    currScreen.dir[(int)Dir.North] = true;

                    nextScreen.dir[(int)Dir.South] = true;

                    currChapter = nextScreen;

                    break;
                }
            case (int)Dir.South:
                {
                    currScreen.dir[(int)Dir.South] = true;

                    nextScreen.dir[(int)Dir.North] = true;

                    currChapter = nextScreen;

                    break;
                }
        }
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

}

