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
        DateTime makeDateTime;      

        float coolTime;

        float destroyTime;        

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
            destroyTime += _h * 360;
        }

        public ballType(int _type)
        {
            makeDateTime = DateTime.Today;
            SetType(_type);
        }

        public DateTime GetDataTime { get; set; }
                
        public void SetType(int _type)
        {
            switch (_type)
            {
                case 0:
                    SetCoolTime(0, 0, 5);
                    SetDestroyTime(0, 0, 2);
                    break;
                case 1:
                    SetCoolTime(0, 0, 10);
                    SetDestroyTime(0, 0, 3);
                    break;
                case 2:
                    SetCoolTime(0, 1, 30);
                    SetDestroyTime(0, 0, 5);
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

    }

    List<ballType> listBallType = new List<ballType>(); // 투입된 볼 종류 체크
    
    // 0:다이아, 1:하트, 2:별
    public Image[] imgBall = new Image[3];  // 쿨타임 이미지

    public Button[] btnBall = new Button[3]; // 쿨타임 버튼

    public Text[] textBall = new Text[3]; // 쿨타임 텍스트

    public int ballMaxCount; // 볼 최대로 넣을 수 있는 수

    bool[] isOnCoolTime = new bool[3]; // 쿨타임 확인

    float[] currCoolTime = new float[3]; // 쿨타임 계산
    

    void Awake()
    {

        btnBall[0].onClick.AddListener(() => { GetBall(0); });
        btnBall[1].onClick.AddListener(() => { GetBall(1); });
        btnBall[2].onClick.AddListener(() => { GetBall(2); });

    }

    void GetBall(int _type)
    {
        // 쿨타임인지 확인
        if (isOnCoolTime[_type])
            return;

        // 공 최대 수면 리턴
        if (listBallType.Count == ballMaxCount)
            return;

        // 해당 버튼 쿨 타임 시작
        isOnCoolTime[_type] = true;

        // 해당 버튼 리스트 추가
        ballType newBall = new ballType(_type);
        listBallType.Add(newBall);

        Debug.Log(_type + " CoolTime : " + newBall.GetCoolTime());

        currCoolTime[_type] = newBall.GetCoolTime();

        // 버튼 쿨타임 시작
        StartCoroutine(ButtonCoolStart(_type, newBall));

        // 공 쿨타임 시작
        StartCoroutine(ObjCoolStart(newBall));

        // 쿨타임 텍스트 표시
        StartCoroutine(CurrCoolTime(_type));

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

    IEnumerator CurrCoolTime(int _index)
    {
        while (currCoolTime[_index] > 0)
        {
            currCoolTime[_index] -= 1.0f;
            textBall[_index].text = "" + currCoolTime[_index].ToString();
            yield return new WaitForSeconds(1.0f);
        }
        yield break;
    }

    // 공 쿨타임 코루틴
    IEnumerator ObjCoolStart(ballType _obj)
    {
        while (_obj.GetDestroyTime() > 0)
        {
            _obj.DestroyTimeDecrease(Time.smoothDeltaTime);

            yield return null;
        }
        Debug.Log(_obj.GetDestroyTime() + " : Destroy");
        listBallType.Remove(_obj);
        yield break;
    }

   

}
