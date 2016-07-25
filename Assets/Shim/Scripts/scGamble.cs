using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class scGamble : MonoBehaviour {
    public Sprite spriteBlack;
    public Sprite spriteBlue;
    public Sprite spriteRed;


    Button[] btnPickNum = new Button[2];
    Button btnGambleNumber;
    Button btnGetCoin;

    Image[] imagePickNum = new Image[2];
    Image imageGambleNumber;
    Image imageGetCoin;


    GameObject[] objPickNum = new GameObject[2];
    GameObject objGambleNumber;
    GameObject objGetCoin;
        
    int prizeReward;

    bool[] isPick = new bool[2];
    bool isCheckGambleNum = false;

    int gambleNum;

    void Awake()
    {

       // spriteBlack = Resources.Load("Btn_Black", typeof(Sprite)) as Sprite;
       // spriteBlue = Resources.Load("Images/Btn_Blue") as Sprite;
       // spriteRed = Resources.Load("Images/Btn_Red") as Sprite;        

        objPickNum[1] = transform.FindChild("Odd_Num").gameObject;
        objPickNum[0] = transform.FindChild("Even_Num").gameObject;
        objGambleNumber = transform.FindChild("Number").gameObject;
        objGetCoin = transform.FindChild("Btn_GetPrize").gameObject;

        btnPickNum[1] = objPickNum[1].GetComponent<Button>();
        btnPickNum[0] = objPickNum[0].GetComponent<Button>();
        btnGambleNumber = objGambleNumber.GetComponent<Button>();
        btnGetCoin = objGetCoin.gameObject.GetComponent<Button>();

        imagePickNum[0] = objPickNum[0].GetComponent<Image>();
        imagePickNum[1] = objPickNum[1].GetComponent<Image>();
        imageGambleNumber = objGambleNumber.GetComponent<Image>();
        imageGetCoin = objGetCoin.gameObject.GetComponent<Image>();

        btnGetCoin.onClick.AddListener(() => { GetPrize(); });
        btnGambleNumber.onClick.AddListener(() => { CheckGambleNum(); });
        btnPickNum[0].onClick.AddListener(() => { PickNumber(0); });
        btnPickNum[1].onClick.AddListener(() => { PickNumber(1); });

        InitGambel(10000);
    }
    
    // 도박 초기화
    public void InitGambel(int _prize)
    {
        NumberGenerator(); // 겜블 번호 초기화

        imagePickNum[0].sprite = spriteBlue;
        imagePickNum[1].sprite = spriteBlue;
        imageGambleNumber.sprite = spriteBlue;
        isPick[0] = false ;
        isPick[1] = false;
        isCheckGambleNum = false;
        
        objGambleNumber.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "?";
        
        prizeReward = _prize;

        gameObject.transform.FindChild("Prize").FindChild("Text").gameObject.GetComponent<Text>().text = prizeReward.ToString() + " C";
    }

    // 상금 받기
    void GetPrize()
    {
        if (!isCheckGambleNum)
            return;
        GameDataManager.Instance.userData.currentCoin += prizeReward;
        gameObject.SetActive(false);
    }

    // 겜블 번호 확인
    void CheckGambleNum()
    {
        // 홀짝을 골랐는가?
        if (!isPick[0] && !isPick[1])
            return;

        // 공개 했는가?
        if(isCheckGambleNum)
            return;
        
        bool isOdd = System.Convert.ToBoolean(gambleNum % 2);

        if (isOdd) // 홀수냐
        {
            if (isPick[0]) // 짝 선택?            
                imageGambleNumber.sprite = spriteBlack;
            else if (isPick[1]) // 홀 선택?
                imageGambleNumber.sprite = spriteRed;
        }

        else // 짝수냐
        {
            if (isPick[0]) // 짝 선택?            
                imageGambleNumber.sprite = spriteRed;
            else if (isPick[1]) // 홀 선택?
                imageGambleNumber.sprite = spriteBlack;
        }

        isCheckGambleNum = true;
        
        // 겜블번호 공개
        objGambleNumber.transform.FindChild("Text").gameObject.GetComponent<Text>().text = gambleNum.ToString();
    }

    void PickNumber(int _index)
    {

        if (isCheckGambleNum)
            return;

            
        if (System.Convert.ToBoolean(_index))
            Debug.Log("홀수");
        else
            Debug.Log("짝수");


        int anotherIndex = (_index + 1) % 2;

        isPick[anotherIndex] = isPick[_index];
        isPick[_index] = !isPick[_index];

        if (isPick[_index])
            imagePickNum[_index].sprite = spriteRed;
        else
            imagePickNum[_index].sprite = spriteBlue;

        if (isPick[anotherIndex])
            imagePickNum[anotherIndex].sprite = spriteRed;
        else
            imagePickNum[anotherIndex].sprite = spriteBlue;

    }

    void NumberGenerator()
    {
        gambleNum = Random.Range(0, 10);
    }     
}
