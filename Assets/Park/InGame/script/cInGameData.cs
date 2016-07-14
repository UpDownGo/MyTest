using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cInGameData : MonoBehaviour {

    // 싱글톤 인스턴스
    private static cInGameData _instance = null;

    public static cInGameData Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("cInGameData == null");
            return _instance;
        }
    }

    // 속도 관련 변수
    public float m_fGaugeIncreaseSpeed;      // 상승 속도
    public float m_fGaugeDecreaseSpeed;     // 하강 속도
    public float m_fGaugeIncreaseAccel;       // 상승 가속도 (0~1.0)
    public float m_fGaugeDecreaseAccel;     // 하강 가속도 (1.0~)

    private float m_fGaugeIncreaseSpeedInit;         // 상승 속도 초기값
    private float m_fGaugeDecreaseSpeedInit;        // 하강 속도 초기값

    // 게이지 ratio 값
    private float m_fGauge;

    // 리스폰 위치값만 가져온다
    public GameObject m_objRespawnPos;

    // 공 프리팹
    public GameObject[] m_prfSplashObject;

    // 메인공들의 리스트
    private List<GameObject> m_listSplashObject = new List<GameObject>();

    // 박스
    //public GameObject m_objBox;


    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Jump")) IncreaseGauge();
        DecreaseGauge();
        CreateAndRemoveForSplashObject();
    }

    // 초기화
    void Initialize()
    {
        // 초기값들 저장
        m_fGaugeIncreaseSpeedInit = m_fGaugeIncreaseSpeed;
        m_fGaugeDecreaseSpeedInit = m_fGaugeDecreaseSpeed;

        // 게이지는 기본적으로 0으로 설정
        m_fGauge = 0.0f;

        // 리스트에 공 미리 채우기
        for(int i=0; i<4; ++i)
            m_listSplashObject.Add((GameObject)Instantiate(m_prfSplashObject[0], m_objRespawnPos.transform.position, Quaternion.identity));
    }

    // 증가부분 초기화
    void InitializeIncrease()
    {
        m_fGaugeIncreaseSpeed = m_fGaugeIncreaseSpeedInit;
    }

    // 감소부분 초기화
    void InitializeDecrease()
    {
        m_fGaugeDecreaseSpeed = m_fGaugeDecreaseSpeedInit;
    }

    // 게이지 감소
    void DecreaseGauge()
    {
        // 일정하게 게이지 감소
        m_fGauge -= Time.fixedDeltaTime * m_fGaugeDecreaseSpeed * m_fGaugeDecreaseAccel;
        
        if(m_fGauge <= 0.0f)
        {
            m_fGauge = 0.0f;
            InitializeDecrease(); // 게이지가 0이면 속도도 초기화
        }
    }

    // 게이지 증가
    public void IncreaseGauge()
    {
        // 이전 게이지는 full인 상태로
        // 실제는 1.25까지이지만 화면에선 1.0까지만 보이게 할것임
        // 풀게이지상태에서의 텀을 주기위함?
        if (m_fGauge >= 1.0f)
        {
            m_fGauge = 1.25f; // 만렙일때 어느정도 텀을 줍시다
            
        }
        else if (m_fGauge >= 0.75f)
        {
            m_fGaugeIncreaseSpeed = m_fGaugeIncreaseSpeedInit * m_fGaugeIncreaseAccel * m_fGaugeIncreaseAccel * m_fGaugeIncreaseAccel; // 상승속도 감소(가속도가 1미만이므로 곱셈)  
            
        }
        else if (m_fGauge >= 0.5f)
            m_fGaugeIncreaseSpeed = m_fGaugeIncreaseSpeedInit * m_fGaugeIncreaseAccel * m_fGaugeIncreaseAccel;
        else if (m_fGauge >= 0.25f)
            m_fGaugeIncreaseSpeed = m_fGaugeIncreaseSpeedInit * m_fGaugeIncreaseAccel;
        else if (m_fGauge >= 0.0f)
            m_fGaugeIncreaseSpeed = m_fGaugeIncreaseSpeedInit;

        // 일정하게 게이지 상승
        m_fGauge += Time.fixedDeltaTime * m_fGaugeIncreaseSpeed;

        // 상승할때 감소 속도는 초기화
        InitializeDecrease();
    }

    // 0.25 증가마다 볼 생성
    void CreateAndRemoveForSplashObject()
    {
        if (m_fGauge >= 1.0f)
        {
            if (m_listSplashObject[3].activeInHierarchy == false)
                m_listSplashObject[3].SetActive(true);
        }
        else if (m_fGauge >= 0.75f)
        {
            if (m_listSplashObject[3].activeInHierarchy == true)
                m_listSplashObject[3].SetActive(false);
            if (m_listSplashObject[2].activeInHierarchy == false)
                m_listSplashObject[2].SetActive(true);
        }
        else if (m_fGauge >= 0.5f)
        {
            if (m_listSplashObject[2].activeInHierarchy == true)
                m_listSplashObject[2].SetActive(false);
            if (m_listSplashObject[1].activeInHierarchy == false)
                m_listSplashObject[1].SetActive(true);
        }
        else if (m_fGauge >= 0.25f)
        {
            if (m_listSplashObject[1].activeInHierarchy == true)
                m_listSplashObject[1].SetActive(false);
            if (m_listSplashObject[0].activeInHierarchy == false)
                m_listSplashObject[0].SetActive(true);
        }
        else if (m_fGauge >= 0.0f)
        {
            for (int i = 0; i < 4; ++i)
            {
                if(m_listSplashObject[i].activeInHierarchy == true)
                    m_listSplashObject[i].SetActive(false);
            }
        }
    }


    // 게이지 비율 리턴
    public float GetGaugeRatio()
    {
        // 화면에 그리기 위한 값이므로 1.0까지만 제한
        if (m_fGauge >= 1.0f) return 1.0f;
        else return m_fGauge;
    }

    public void CreateSplashObject(int _para)
    {
        if(_para == 4)
            for(int i=0; i<3; ++i)
                Instantiate(m_prfSplashObject[_para], m_objRespawnPos.transform.position, Quaternion.identity);
        else
            Instantiate(m_prfSplashObject[_para], m_objRespawnPos.transform.position, Quaternion.identity);
    }


    // 걍 디버깅용
    public int GetListCount()
    {
        return m_listSplashObject.Count;
    }
}