using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Timers;
public class scTimer : MonoBehaviour {
    // 볼 Class
    class ballType
    {
        /// 생성자
        public ballType(int _type)
        {
            makeDateTime = DateTime.Today;
            SetType(_type);
        }

        /// <변수>        
        DateTime makeDateTime;      

        float coolTime;

        float destroyTime;

        cSplashObject ingameBall;
        /// </변수>
        /// 
        ///
        /// <함수>        
        void SetCoolTime(int _h, int _m, int _s)
        {
            coolTime += _s;
            coolTime += _m * 60;
            coolTime += _h * 360;            
        }

        void SetDestroyTime(int _h, int _m, int _s)
        {
            destroyTime += _s;
            destroyTime += _m * 60;
            destroyTime += _h * 3600;
        }

        public DateTime GetDataTime { get; set; }
        public void SetcSplashObject(cSplashObject _obj)
        {
            ingameBall = _obj;
        }
        public cSplashObject GetcSplashObject()
        {
            return ingameBall;
        }

        public void SaveDestroyTime(float _time)
        {
            destroyTime = _time;
        }

        public void SetType(int _type)
        {
            switch (_type)
            {
                case 0:
                    SetCoolTime(0, 0, 1);
                    SetDestroyTime(0, 0, 15);
                    break;
                case 1:
                    SetCoolTime(0, 0, 10);
                    SetDestroyTime(0, 0, 10);
                    break;
                case 2:
                    SetCoolTime(0, 0, 30);
                    SetDestroyTime(0, 0, 30);
                    break;
            }
        }

        public float GetCoolTime()
        {
            return coolTime;
        }

        public float GetDestroyTime()
        {
            return destroyTime;
        }

        public void DestroyTimeDecrease(float _time)
        {
            destroyTime -= _time;
        }
        /// </함수>
    }

    List<ballType> listBallType = new List<ballType>(); // 투입된 볼 종류 체크

    // 0:다이아, 1:하트, 2:별
    public Image[] imgBall = new Image[3];  // 쿨타임 이미지

    public Button[] btnBall = new Button[3]; // 쿨타임 버튼

    public Text[] textCoolTime = new Text[3]; // 쿨타임 텍스트

    public Text textBallCount ; // 쿨타임 텍스트

    public int ballMaxCount; // 볼 최대로 넣을 수 있는 수

    bool[] isOnCoolTime = new bool[3]; // 쿨타임 확인

    float[] currCoolTime = new float[3]; // 쿨타임 계산
    
    void Awake()
    {

        btnBall[0].onClick.AddListener(() => { GetBall(0); });
        btnBall[1].onClick.AddListener(() => { GetBall(1); });
        btnBall[2].onClick.AddListener(() => { GetBall(2); });

    }
    void Start()
    {
        textBallCount.text = listBallType.Count + " / " + ballMaxCount + " B";
    }

    // 게임 시작 시 점검 사항
    void CheckStart()
    {
        // 종료 시 저장된 공 확인
        if(GameDataManager.Instance.userData.saveBalltype.Length > 0)
        {
            for(int i = 0; i < GameDataManager.Instance.userData.saveBalltype.Length; i++)
            {
                // 저장된 공들 생성
                ballType newBall = new ballType(GameDataManager.Instance.userData.saveBalltype[i]);

                // 공들 소멸 시간 재셋팅
                newBall.SaveDestroyTime(GameDataManager.Instance.userData.saveBallDestroyTime[i]);

                // 리스트 추가
                listBallType.Add(newBall);

                // 루틴 시작
                StartCoroutine(ObjDestroyTimeStart(newBall));
            }
        }

        // 남아 있는 쿨타임 확인
        if(GameDataManager.Instance.userData.saveBallCoolTime[0] > 0)
        {
            isOnCoolTime[0] = true;
            currCoolTime[0] = GameDataManager.Instance.userData.saveBallCoolTime[0];

            //StartCoroutine(ButtonCoolReStart(0, currCoolTime[0]));

        }

        if (GameDataManager.Instance.userData.saveBallCoolTime[1] > 0)
        {
            isOnCoolTime[1] = true;
            currCoolTime[1] = GameDataManager.Instance.userData.saveBallCoolTime[1];
        }

        if (GameDataManager.Instance.userData.saveBallCoolTime[2] > 0)
        {
            isOnCoolTime[2] = true;
            currCoolTime[2] = GameDataManager.Instance.userData.saveBallCoolTime[2];
        }

    }

    // 버튼 함수
    void GetBall(int _type)
    {
        
        // 쿨타임인지 확인
        if (isOnCoolTime[_type])
            return;

        // 공 최대 수면 리턴
        if (listBallType.Count == ballMaxCount)
        {

            return;
        }

        

        // 해당 버튼 쿨 타임 시작
        isOnCoolTime[_type] = true;

        // 해당 버튼 리스트 추가
        ballType newBall = new ballType(_type);

        newBall.SetcSplashObject(cInGameData.Instance.CreateSplashObject(_type + 1));

        /////////////////////////////// 추가된 공의 소멸 시간 변경
        newBall.GetcSplashObject().m_fMaxActingTime = newBall.GetDestroyTime();
        newBall.GetcSplashObject().UpdateUserData(newBall.GetDestroyTime());
        ///////////////////////////////

        listBallType.Add(newBall);

        textBallCount.text = listBallType.Count + " / " + ballMaxCount + " B";

        Debug.Log(_type + " CoolTime : " + newBall.GetCoolTime());

        currCoolTime[_type] = newBall.GetCoolTime();

        // 버튼 쿨타임 시작
        StartCoroutine(ButtonCoolStart(_type, newBall));

        // 공 소멸 시간 시작
        StartCoroutine(ObjDestroyTimeStart(newBall));

        // 쿨타임 텍스트 표시
        StartCoroutine(CurrCoolTime(_type));

    }

    // 재시작시 버튼 쿨타임 코루틴
    IEnumerator ButtonCoolReStart(int _index, ballType _obj, float _time)
    {
        imgBall[_index].fillAmount = 0;
        while (imgBall[_index].fillAmount < 1)
        {
            // 쿨타임계산
            imgBall[_index].fillAmount += Time.smoothDeltaTime / _obj.GetCoolTime();
            yield return null;
        }

        isOnCoolTime[_index] = false;
        Debug.Log(_index + " : " + isOnCoolTime[_index]);
        yield break;
    }

    // 버튼 쿨타임 코루틴
    IEnumerator ButtonCoolStart(int _index, ballType _obj)
    {
        imgBall[_index].fillAmount = 0;
        while (imgBall[_index].fillAmount < 1)
        { 
            // 쿨타임계산
            imgBall[_index].fillAmount += Time.smoothDeltaTime / _obj.GetCoolTime();
            yield return null;
        }

        isOnCoolTime[_index] = false;
        Debug.Log(_index + " : " + isOnCoolTime[_index]);
        yield break;
    }

    // 쿨타임 표기 코루틴
    IEnumerator CurrCoolTime(int _index)
    {
        while (currCoolTime[_index] > 0)
        {
            currCoolTime[_index] -= 1.0f;

            textCoolTime[_index].text = currCoolTime[_index].ToString();
            yield return new WaitForSeconds(1.0f);
        }

        textCoolTime[_index].text = null;
        yield break;
    }

    // 공 소멸 시간 코루틴
    IEnumerator ObjDestroyTimeStart(ballType _obj)
    {
        while (_obj.GetDestroyTime() > 0)
        {
            _obj.DestroyTimeDecrease(Time.smoothDeltaTime);

            yield return null;
        }

        //Debug.Log(_obj.GetDataTime.ToString() + " : Destroy");

        listBallType.Remove(_obj);
        //cInGameData.Instance.DeadNorify(_obj.GetcSplashObject().gameObject);
        textBallCount.text = listBallType.Count + " / " + ballMaxCount + " B";
        yield break;
    }

   

}
