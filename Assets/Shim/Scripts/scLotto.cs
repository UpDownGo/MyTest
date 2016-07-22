using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class scLotto : MonoBehaviour {

    public Sprite imagePick;
    public Sprite imageYesPick;
    public Sprite imageNoPick;

    List<int> listPick = new List<int>();
    List<int> listGambleNum = new List<int>();

    GameObject[] objSelect = new GameObject[10];
    GameObject[] objGamble = new GameObject[2];

    Button[] btnSelectNum = new Button[10];
    Button[] btnGambleNum = new Button[2];
    Button btnGetPrize;

    bool[] isNumPick = new bool[10];
    bool[] isGamblePick = new bool[2];
    bool[] isWin = new bool[2];
    bool isDiaLotto;

    Text pirzeText;

    int[] pirzeReward = new int[3];

    bool isStart = false;

    int rankings = 0;

    public void GambleInit(int _first, int _sec, int _third, bool _kind)
    {
        isDiaLotto = _kind;
        pirzeReward[0] = _third;
        pirzeReward[1] = _sec;
        pirzeReward[2] = _first;

        listPick.Clear();
        listGambleNum.Clear();

        pirzeText.text = "? ? ?";

        isStart = false;

        for (int i = 0; i < 2; i++)
        {
            isGamblePick[i] = false;
            isWin[i] = false;
            objGamble[i].GetComponent<Image>().sprite = imageNoPick;

        }

        for (int i = 0; i < 10; i++)
        {
            isNumPick[i] = false;
            objSelect[i].GetComponent<Image>().sprite = imageNoPick;
        }

        NumGenerator();
    }

    void Awake()
    {
        btnGetPrize = transform.FindChild("Btn_GetPrize").gameObject.GetComponent<Button>();
        btnGetPrize.onClick.AddListener(delegate() { GetPrize(); });


        objGamble[0] = transform.FindChild("First_Num").gameObject;
        objGamble[1] = transform.FindChild("Second_Num").gameObject;


        for (int i = 0; i < 2; i++)
        {
            btnGambleNum[i] = objGamble[i].GetComponent<Button>();
        }

        btnGambleNum[0].onClick.AddListener(() => { PickGambleNum(0); });
        btnGambleNum[1].onClick.AddListener(() => { PickGambleNum(1); });



        objSelect[0] = transform.FindChild("Btn_Grid").FindChild("Num (0)").gameObject;
        objSelect[1] = transform.FindChild("Btn_Grid").FindChild("Num (1)").gameObject;
        objSelect[2] = transform.FindChild("Btn_Grid").FindChild("Num (2)").gameObject;
        objSelect[3] = transform.FindChild("Btn_Grid").FindChild("Num (3)").gameObject;
        objSelect[4] = transform.FindChild("Btn_Grid").FindChild("Num (4)").gameObject;
        objSelect[5] = transform.FindChild("Btn_Grid").FindChild("Num (5)").gameObject;
        objSelect[6] = transform.FindChild("Btn_Grid").FindChild("Num (6)").gameObject;
        objSelect[7] = transform.FindChild("Btn_Grid").FindChild("Num (7)").gameObject;
        objSelect[8] = transform.FindChild("Btn_Grid").FindChild("Num (8)").gameObject;
        objSelect[9] = transform.FindChild("Btn_Grid").FindChild("Num (9)").gameObject;

        for (int i = 0; i < 10; i++)
        {
            btnSelectNum[i] = objSelect[i].GetComponent<Button>();
        }

        // onClick() 설정
        btnSelectNum[0].onClick.AddListener(() => { PickNum(0); });
        btnSelectNum[1].onClick.AddListener(() => { PickNum(1); });
        btnSelectNum[2].onClick.AddListener(() => { PickNum(2); });
        btnSelectNum[3].onClick.AddListener(() => { PickNum(3); });
        btnSelectNum[4].onClick.AddListener(() => { PickNum(4); });
        btnSelectNum[5].onClick.AddListener(() => { PickNum(5); });
        btnSelectNum[6].onClick.AddListener(() => { PickNum(6); });
        btnSelectNum[7].onClick.AddListener(() => { PickNum(7); });
        btnSelectNum[8].onClick.AddListener(() => { PickNum(8); });
        btnSelectNum[9].onClick.AddListener(() => { PickNum(9); });


        // 상품 텍스트
        pirzeText = transform.FindChild("Prize").FindChild("Text").gameObject.GetComponent<Text>();
        pirzeText.text = "? ? ?";
    }

    void Start()
    {
        GambleInit(100000, 10000, 1000,false);
    }

    // 픽 버튼 함수
    void PickNum(int _index)
    {
        // 까기 시작했으면 픽 변경 X
        if (isStart)
            return;

        // 2개의 숫자를 선택했는가?
        if(listPick.Count == 2)
        {
            // 선택한 2개의 숫자 중에 고른 것이 아니면 리턴
            if (isNumPick[_index] == false)
                return;
        }

        // 픽 변경
        isNumPick[_index] = !isNumPick[_index];

        // 픽에 따른 스프라이트 변경
        if (isNumPick[_index])
        {
            objSelect[_index].GetComponent<Image>().sprite = imagePick;
            listPick.Add(_index);
        }
        else
        {
            objSelect[_index].GetComponent<Image>().sprite = imageNoPick;
            listPick.Remove(_index);
        }
    }

    // 번호 확인 함수
    void PickGambleNum(int _index)
    {

        // 번호 두개를 선택 하지 않았으면 리턴
        if (listPick.Count != 2)
        {
            Debug.Log("번호 두개 선택해라!");
            return;
        }

        // 한번 확인 했으면 리턴
        if (isGamblePick[_index])
        {
            Debug.Log("한번 확인 했다!");
            return;
        }

        isGamblePick[_index] = true;
        isStart = true;

        // 번호 공개
        objGamble[_index].transform.FindChild("Text").gameObject.GetComponent<Text>().text = listGambleNum[_index].ToString();
       

        // 번호에 따른 배경 화면 변경
        if (listPick.Exists(delegate (int pick) { return pick == listGambleNum[_index]; }))
        {
            objGamble[_index].GetComponent<Image>().sprite = imageYesPick;
            isWin[_index] = true;
            Debug.Log("같은 번호가 있다.");

        }
        else
        {
            objGamble[_index].GetComponent<Image>().sprite = imagePick;
            isWin[_index] = false;
            Debug.Log("같은 번호가 없다.");

        }
        CheckWin();
    }

    // 복권 번호 생성
    void NumGenerator()
    {
        RandomNum();
        RandomNum();

        listGambleNum.Sort();


        objGamble[0].transform.FindChild("Text").gameObject.GetComponent<Text>().text = "?";
        objGamble[1].transform.FindChild("Text").gameObject.GetComponent<Text>().text = "?";
    }

    // 같지 않는 번호 생성
    void RandomNum()
    {
        if (listGambleNum.Count == 2)
            return;

        int num = Random.Range(0, 9);

        if (listGambleNum.Exists(delegate (int pick) { return pick == num; }))
        { 
            RandomNum();
            return;
        }            

        listGambleNum.Add(num);
    }

    // 상금 체크
    void CheckWin()
    {
        int price = 0;

        for (int i = 0; i < 2; i++)
        {
            if (isWin[i] == true)
                price++;
        }

        rankings = price;
                
        Debug.Log("rankings : " + rankings);
        if (isDiaLotto)
            pirzeText.text = pirzeReward[price].ToString() + " D";
        else
            pirzeText.text = pirzeReward[price].ToString() + " C";
    }

    // 상금 얻기
    void GetPrize()
    {
        Debug.Log("도박은 위험해!");
        if (isGamblePick[0] == true && isGamblePick[1] == true)
        {
            if (isDiaLotto)
            {
                GameDataManager.Instance.userData.currentDia += pirzeReward[rankings];
                GameDataManager.Instance.UpdateText();
            }
            else
            {
                GameDataManager.Instance.userData.currentCoin += pirzeReward[rankings];
                GameDataManager.Instance.UpdateText();
            }
            gameObject.SetActive(false);
        }
    }
}
