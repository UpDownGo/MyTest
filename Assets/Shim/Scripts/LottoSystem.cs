using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements; // UniyuAds
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class LottoSystem : MonoBehaviour
{

    class cLotto
    {
        string strName;

        int firstPrize;
        int secondPrize;
        int thirdPrize;

        int currCount;
        int maxCount = 5;

        public void InitPrize(int _first, int _second, int _third)
        {
            firstPrize = _first;
            secondPrize = _second;
            thirdPrize = _third;
        }
        public void InitCount()
        {
            currCount = maxCount;
        }
        public int GetFirstPrize()
        {            
           return firstPrize;            
        }
        public int GetSecondPrize()
        {
            return secondPrize;
        }
        public int GetThirdPrize()
        {
            return thirdPrize;
        }
    }

    int gambleIndex;

    cLotto coinLotto = new cLotto();
    cLotto diaLotto = new cLotto();
    cLotto gambleLotto = new cLotto();

    public GameObject[] objPage = new GameObject[2];

    public GameObject[] objLotto = new GameObject[3];
    Text[] coinPrize = new Text[5];
    Text[] diaPrize = new Text[5];
    Text[] gamblePrize = new Text[5];

    public scLotto scLotto;

    void Awake()
    {                                                      
        coinPrize[0] = objLotto[0].transform.FindChild("1stPrizeText").GetComponent<Text>();
        coinPrize[1] = objLotto[0].transform.FindChild("2ndPrizeText").GetComponent<Text>();
        coinPrize[2] = objLotto[0].transform.FindChild("3rdPrizeText").GetComponent<Text>();
        coinPrize[3] = objLotto[0].transform.FindChild("ExistText").GetComponent<Text>();
        coinPrize[4] = objLotto[0].transform.FindChild("TitleText").GetComponent<Text>();


        diaPrize[0] = objLotto[2].transform.FindChild("1stPrizeText").GetComponent<Text>();
        diaPrize[1] = objLotto[2].transform.FindChild("2ndPrizeText").GetComponent<Text>();
        diaPrize[2] = objLotto[2].transform.FindChild("3rdPrizeText").GetComponent<Text>();
        diaPrize[3] = objLotto[2].transform.FindChild("ExistText").GetComponent<Text>();
        diaPrize[4] = objLotto[2].transform.FindChild("TitleText").GetComponent<Text>();

        gamblePrize[0] = objLotto[1].transform.FindChild("1stPrizeText").GetComponent<Text>();
        gamblePrize[1] = objLotto[1].transform.FindChild("2ndPrizeText").GetComponent<Text>();
        gamblePrize[2] = objLotto[1].transform.FindChild("3rdPrizeText").GetComponent<Text>();
        gamblePrize[3] = objLotto[1].transform.FindChild("ExistText").GetComponent<Text>();
        gamblePrize[4] = objLotto[1].transform.FindChild("TitleText").GetComponent<Text>();
    }

    // Use this for initialization
    void Start()
    {
        PrizeInit();
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void GetCoinGamble()
    {

    }
    // 상금 초기화
    void PrizeInit()
    {
        DiaPrizeInit();
        CoinPrizeInit();
        GamblePrizeInit();
    }

    void TextUpdate()
    {
        coinPrize[0].text = string.Format("{0:##,###,###,###}", coinLotto.GetFirstPrize()) + " C";
        coinPrize[1].text = string.Format("{0:##,###,###,###}", coinLotto.GetSecondPrize()) + " C";
        coinPrize[2].text = string.Format("{0:##,###,###,###}", coinLotto.GetThirdPrize()) + " C";
        coinPrize[4].text = "CoinGamble";

        diaPrize[0].text = string.Format("{0:##,###,###,###}", diaLotto.GetFirstPrize()) + " D";
        diaPrize[1].text = string.Format("{0:##,###,###,###}", diaLotto.GetSecondPrize()) + " D";
        diaPrize[2].text = string.Format("{0:##,###,###,###}", diaLotto.GetThirdPrize()) + " D";
        diaPrize[4].text = "DiaGamble";

        gamblePrize[0].text = string.Format("{0:##,###,###,###}", gambleLotto.GetFirstPrize()) + " C";
        gamblePrize[1].text = string.Format("{0:##,###,###,###}", gambleLotto.GetSecondPrize()) + " C";
        gamblePrize[2].text = string.Format("{0:##,###,###,###}", gambleLotto.GetThirdPrize()) + " C";
        gamblePrize[4].text = "Gamble";

    }
    
    void DiaPrizeInit()
    {
        // 다이아 상금 조정
        int first = PrizeGenerator(500, 1000);
        int second = PrizeGenerator(100, 499);
        int third = PrizeGenerator(50, 99);

        diaLotto.InitPrize(first, second, third);
    }
    
    void CoinPrizeInit()
    {
        // 다이아 상금 조정
        int first = PrizeGenerator(10000, 5000);
        int second = PrizeGenerator(3000, 6000);
        int third = PrizeGenerator(200, 1000);

        coinLotto.InitPrize(first, second, third);
    }
    
    void GamblePrizeInit()
    {
        // 다이아 상금 조정
        int first = PrizeGenerator(10000, 5000);
        int second = PrizeGenerator(3000, 6000);
        int third = PrizeGenerator(200, 1000);

        gambleLotto.InitPrize(first, second, third);
    }
    
    // 당첨금 생성기
    int PrizeGenerator(int min, int max)
    {
        return Random.Range(min, max);
    }

    // 동영상 광고 로직
    public void ShowRewardedAd(int _gambleIndex)
    {
        gambleIndex = _gambleIndex;

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            Debug.Log("No more rewarded at UnityAds.");
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");

                if (gambleIndex == 1)
                    objPage[1].SetActive(true);
                else if (gambleIndex == 0)
                {
                    objPage[0].SetActive(true);
                    scLotto.GambleInit(coinLotto.GetFirstPrize(), coinLotto.GetSecondPrize(), coinLotto.GetThirdPrize(),false);
                }

                else if (gambleIndex == 2)
                {
                    objPage[0].SetActive(true);
                    scLotto.GambleInit(diaLotto.GetFirstPrize(), diaLotto.GetSecondPrize(), diaLotto.GetThirdPrize(),true);
                }

                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
