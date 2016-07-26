using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

    // 오토공들의 리스트
    private List<GameObject> m_listAutoSplashObject = new List<GameObject>();

    // 현재 챕터
    private int m_iCurrentChapter;

    // 버튼 객체
    //public GameObject[] m_objButton = new GameObject[3];

    // 스팟라이트
    public GameObject m_ltSpotLight;

    // 디렉션라이트
    public GameObject m_ltDirLight;

    // 다이아타임?
    private bool m_bIsDiaTime;

    // 피버타임?
    private bool m_bIsFever1;
    private bool m_bIsFever2;
    private bool m_bIsFever3;
    private bool m_bIsFeverTime;

    // 단계
    private int m_iStep;

    // 사운드
    private AudioSource m_asBGM;
    private AudioSource m_asEffect;

    public AudioClip m_acFever1Sptep;
    public AudioClip m_acFever2Sptep;
    public AudioClip m_acFever3Sptep;
    public AudioClip m_acFever4Sptep;

    public AudioClip m_acFeverBGM;
    public AudioClip m_acDiaBGM;

    // ui text
    public GameObject[] m_objFeverText = new GameObject[4];

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
        //////////////////////////////////////////////////////////////////////////////////
        //단순 디버깅용
        if (Input.GetKeyDown(KeyCode.Alpha0))
            m_iCurrentChapter = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            m_iCurrentChapter = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            m_iCurrentChapter = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            m_iCurrentChapter = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            m_iCurrentChapter = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            m_iCurrentChapter = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            m_iCurrentChapter = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            m_iCurrentChapter = 7;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            m_iCurrentChapter = 8;
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            m_iCurrentChapter = 9;
        else if (Input.GetKeyDown(KeyCode.Q))
            DiaTimeOnOff(true);
        else if (Input.GetKeyDown(KeyCode.W))
            DiaTimeOnOff(false);

        if (m_iCurrentChapter >= 3)
        {
            if (Input.GetButton("Jump"))
                IncreaseGauge();

            // 챕터 2 이후로만 피버 적용이 되므로
            DecreaseGauge();
            CreateAndRemoveForSplashObject();

            PlayFeverSoundAndTextAnim();
        }
            
        /////////////////////////////////////////////////////////////////////////////////////

        
        //ControlButton();
    }

    // 초기화
    void Initialize()
    {
        // 초기값들 저장
        m_fGaugeIncreaseSpeedInit = m_fGaugeIncreaseSpeed;
        m_fGaugeDecreaseSpeedInit = m_fGaugeDecreaseSpeed;

        // 게이지는 기본적으로 0으로 설정
        m_fGauge = 0.0f;

        // 현재 챕터 일단 받아올때까지는 0으로
        m_iCurrentChapter = 0;

        // 리스트에 공 미리 채우기
        for (int i = 0; i < 4; ++i)
        {
            m_listSplashObject.Add((GameObject)Instantiate(m_prfSplashObject[0], m_objRespawnPos.transform.position, Quaternion.identity));
            m_listSplashObject[i].SetActive(false); // 일단 전부 비활성화
        }

        // 현재 단계
        m_iStep = 0;

        // bool 값들
        m_bIsDiaTime = false;

        m_bIsFever1 = false;
        m_bIsFever2 = false;
        m_bIsFever3 = false;
        m_bIsFeverTime = false;


        // 오디오 소스를 두개 쓸거이므로 여기서 컴포넌트 추가한다
        m_asBGM = gameObject.AddComponent<AudioSource>();
        m_asEffect = gameObject.AddComponent<AudioSource>();
        m_asBGM.loop = true;
        m_asEffect.loop = false;
        m_asEffect.pitch = 1.4f;

        // 비지엠 키고 시작
        m_asBGM.volume = 0.1f;
        m_asBGM.clip = m_acFeverBGM;
        m_asBGM.Play();
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
        // 증가 속도를 구간마다 느리게 한다
        // 실제는 1.25까지이지만 화면에선 1.0까지만 보이게 할것임
        // 풀게이지상태에서의 텀을 주기위함?
        if (m_fGauge >= 1.0f)
            m_fGauge = 1.25f; // 만렙일때 어느정도 텀을 줍시다
        else if (m_fGauge >= 0.75f)
            m_fGaugeIncreaseSpeed = m_fGaugeIncreaseSpeedInit * m_fGaugeIncreaseAccel * m_fGaugeIncreaseAccel * m_fGaugeIncreaseAccel; // 상승속도 감소(가속도가 1미만이므로 곱셈) 
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

            m_iStep = 4;

            //m_bIsFever1 = false;
            //m_bIsFever2 = false;
            //m_bIsFever3 = false;
            //m_bIsFeverTime = false;
        }
        else if (m_fGauge >= 0.75f)
        {
            if (m_listSplashObject[3].activeInHierarchy == true)
                m_listSplashObject[3].SetActive(false);
            if (m_listSplashObject[2].activeInHierarchy == false)
                m_listSplashObject[2].SetActive(true);

            m_iStep = 3;

            //m_bIsFever1 = false;
            //m_bIsFever2 = false;
            //m_bIsFever3 = false;
            m_bIsFeverTime = false;
        }
        else if (m_fGauge >= 0.5f)
        {
            if (m_listSplashObject[2].activeInHierarchy == true)
                m_listSplashObject[2].SetActive(false);
            if (m_listSplashObject[1].activeInHierarchy == false)
                m_listSplashObject[1].SetActive(true);

            m_iStep = 2;

            //m_bIsFever1 = false;
            //m_bIsFever2 = false;
            m_bIsFever3 = false;
            m_bIsFeverTime = false;

        }
        else if (m_fGauge >= 0.25f)
        {
            if (m_listSplashObject[1].activeInHierarchy == true)
                m_listSplashObject[1].SetActive(false);
            if (m_listSplashObject[0].activeInHierarchy == false)
                m_listSplashObject[0].SetActive(true);

            m_iStep = 1;

            //m_bIsFever1 = false;
            m_bIsFever2 = false;
            m_bIsFever3 = false;
            m_bIsFeverTime = false;
        }
        else if (m_fGauge >= 0.0f)
        {
            for (int i = 0; i < 4; ++i)
            {
                if(m_listSplashObject[i].activeInHierarchy == true)
                    m_listSplashObject[i].SetActive(false);
            }

            m_iStep = 0;

            m_bIsFever1 = false;
            m_bIsFever2 = false;
            m_bIsFever3 = false;
            m_bIsFeverTime = false;
        }
    }

    // 게이지 비율 리턴
    public float GetGaugeRatio()
    {
        // 화면에 그리기 위한 값이므로 1.0까지만 제한
        if (m_fGauge >= 1.0f) return 1.0f;
        else return m_fGauge;
    }

    // 오토공 생성 ( 처음에 가져올때 )
    public void CreateSplashObject(int _index, float _time)
    {
        GameObject obj = (GameObject)Instantiate(m_prfSplashObject[_index], m_objRespawnPos.transform.position, Quaternion.identity);
        cSplashObject scp = obj.GetComponent<cSplashObject>();
        scp.UpdateUserData(_time);

        // 오토 공 리스트에 포함
        m_listAutoSplashObject.Add(obj);
    }

    // 오토공 생성 ( 버튼으로 가져올때 )
    // 첫 생성이므로 
    public cSplashObject CreateSplashObject(int _index)
    {        
        GameObject obj = (GameObject)Instantiate(m_prfSplashObject[_index], m_objRespawnPos.transform.position, Quaternion.identity);

        // 오토 공 리스트에 포함
        m_listAutoSplashObject.Add(obj);
        return obj.GetComponent<cSplashObject>();
    }

    // 챕터 변경 시, 처음 시작시 챕터 가져온다
    public void SetCurrentChapter(int _chapter)
    {
        m_iCurrentChapter = _chapter;
    }

    // 현재 챕터 가져온다
    public int GetCurrentChapter()
    {
        return m_iCurrentChapter;
    }

    // 오토공 죽는다는 메세지를 받는 함수
    public void DeadNorify(GameObject _obj)
    {
        // 해당 인덱스를 리스트에서 제거
        m_listAutoSplashObject.Remove(_obj);

        //  해당 오브젝트 파괴
        DestroyObject(_obj);
    }

    // 다이아 타임
    public void DiaTimeOnOff(bool _val)
    {
        m_ltDirLight.SetActive(!_val);
        m_ltSpotLight.SetActive(_val);
        m_bIsDiaTime = _val;

        // 다이아타임용 브금 플레이
        if(_val)
        {
            if (m_asBGM.isPlaying) m_asBGM.Stop();
            m_asBGM.clip = m_acDiaBGM;
            m_asBGM.Play();
        }
        else
        {
            if (m_asBGM.isPlaying) m_asBGM.Stop();
            m_asBGM.clip = m_acFeverBGM;
            m_asBGM.Play();
        }
    }

    // 현재 다이아타임인가?
    public bool GetIsDiaTime()
    {
        return m_bIsDiaTime;
    }


    // 피버 사운드
    public void PlayFeverSoundAndTextAnim()
    {
        if(m_iStep == 1 && !m_bIsFever1)
        {
            m_asEffect.clip = m_acFever1Sptep;
            m_asEffect.Play();
            m_bIsFever1 = true;

            m_objFeverText[0].SetActive(true);
        }
        else if (m_iStep == 2 && !m_bIsFever2)
        {
            m_asEffect.clip = m_acFever2Sptep;
            m_asEffect.Play();
            m_bIsFever2 = true;

            m_objFeverText[1].SetActive(true);
        }
        else if (m_iStep == 3 && !m_bIsFever3)
        {
            m_asEffect.clip = m_acFever3Sptep;
            m_asEffect.Play();
            m_bIsFever3 = true;

            m_objFeverText[2].SetActive(true);
        }
        else if (m_iStep == 4 && !m_bIsFeverTime)
        {
            m_asEffect.clip = m_acFever4Sptep;
            m_asEffect.Play();
            m_bIsFeverTime = true;

            m_objFeverText[3].SetActive(true);
        }
    }
    

    // 유저의 데이터 업데이트
    public void UpdateUserData()
    {

    }


    // 걍 디버깅용
    public int GetListCount()
    {
        return m_listSplashObject.Count;
    }
}