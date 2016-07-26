using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class cSplashObject : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // inspector 초기화 항목

    // 사운드
    private AudioSource m_asCollsion;
    public AudioClip m_acCollisionSound;
    public AudioClip m_ac3Cusion;

    // 파티클
    public GameObject m_prfSplashParticle;

    // 3쿠 파티클
    public GameObject m_prf3CuParticle;

    // 타입
    // 0 -> 메인공, 애기(시간 재지않음)
    // 1 -> 다이아
    // 2 -> 하트
    // 3 -> 별
    public int m_iType;

    // max 활동시간
    public float m_fMaxActingTime;

    // 슬라이더 UI
    public Slider m_uiTimeBar;

    // 3쿠션 텍스트
    public GameObject m_ui3Cu;

    public float m_f3cuTimerMax;
    private float m_f3cuTimer;
    private bool m_b3cuTime;

/////////////////////////////////////////////////////////////////////////////////////////////////////
// 초기화 항목 아님

    // 이전 충돌체
    private GameObject m_objPreCollider;

    // 쿠션 리스트
    private Queue<GameObject> m_listFace = new Queue<GameObject>();


/////////////////////////////////////////////////////////////////////////////////////////////////////
// Awake() 초기화용 변수?
// 가져와야하는 항목

    // 돈/히트
    private int m_iMoneyPerHit; // = GameDataManager.Instance.userData.hitPoint[0];

    // 남은 활동시간
    private float m_fRemainActingTime;

	// Use this for initialization
	void Awake () {

        // 일단 풀시간으로
        m_fRemainActingTime = m_fMaxActingTime;

        m_asCollsion = gameObject.AddComponent<AudioSource>();

        m_f3cuTimer = 0.0f;
        m_b3cuTime = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // 남은 시간 검사
        if(m_iType != 0)
            UpdateActingTime();

        if (m_b3cuTime) m_f3cuTimer += Time.fixedDeltaTime;
        if (m_f3cuTimer >= m_f3cuTimerMax)
        {
            m_f3cuTimer = 0.0f;
            m_b3cuTime = false;
        }
    }

    // 충돌박스 진입 
    void OnCollisionEnter(Collision _other)
    {
        CollisionCheck(_other);
    }

    // splash object 생성
    public void CreateSplashObject(GameObject _prf, Vector3 _pos)
    {
        Instantiate(_prf, _pos, Quaternion.identity);
    }

    // 충돌 사운드 플레이
    // 한번만 플레이
    void PlayCollisionSound()
    {
        AudioSource.PlayClipAtPoint(m_acCollisionSound, transform.position);
    }

    // 충돌 파티클 생성
    // 한번만 플레이
    void CreateSplashParticle()
    {
        Instantiate(m_prfSplashParticle, transform.position, Quaternion.identity);
    }

    // 3쿠 파티클
    void Create3CuParticle()
    {
        Instantiate(m_prf3CuParticle, transform.position, Quaternion.identity);
    }


    // 충돌체크
    void CollisionCheck(Collision _other)
    {
        // 충돌체가 없다면 리턴
        if (_other == null) return;

        // 충돌 판정
        if (m_objPreCollider == null)
            m_objPreCollider = _other.gameObject; // 첫충돌시 저장
        else if (m_objPreCollider != null)
        {
            if (m_objPreCollider == _other.gameObject) return;   // 이전 충돌체와 같다면 충돌 아닌걸로
            else if(m_objPreCollider != _other.gameObject)
                m_objPreCollider = _other.gameObject;           // 이전 충돌체와 다르다면 이전 충돌체로 저장
        }
        //////////////////////////////////////////////////////////
        // 위에서 무조건 중복 충돌 무효로 처리


        // 충돌!
        if (cInGameData.Instance.GetIsDiaTime())
            DiaCollision(_other);
        else
            Collision(_other);
    }
   
    // 다이아용 충돌
    void DiaCollision(Collision _other)
    {
        int sum = 0;

        if (_other.gameObject.tag == "face")
            sum = 1;

        // 다이아 전달

    }

   // 충돌!
   void Collision(Collision _other)
    {
        // 돈계산용
        int sum = 0;


        // 6챕터 이후는 공끼리도 충돌
        if (cInGameData.Instance.GetCurrentChapter() > 6)
        {
            if (_other.gameObject.tag == "main ball" || _other.gameObject.tag == "splash object")    // 공끼리
            {
                sum = m_iMoneyPerHit + _other.gameObject.GetComponent<cSplashObject>().GetMoneyPerHit();

                // 파티클 생성
                CreateSplashParticle();

                // 사운드 생성
                PlayCollisionSound();
            }
            else if(_other.gameObject.tag == "face")// 벽이라면
            {
                // 하지만 7이 넘으면
                if (cInGameData.Instance.GetCurrentChapter() > 7 && gameObject.tag == "player ball")
                {
                    if (m_listFace.Count >= 2)  // 충돌체 리스트가 2개이상 쌓여 있다면
                    {
                        if (m_listFace.Contains(_other.gameObject))    // 리스트에 지금 개체가 있는지 확인, 앞에꺼 두개는 collision check에서 중복 체크하고 있음
                        {
                            m_listFace.Dequeue();   // 있다면 앞에꺼 디큐

                            sum = m_iMoneyPerHit;

                            // 파티클 생성
                            CreateSplashParticle();

                            // 사운드 생성
                            PlayCollisionSound();
                        }
                        else if(m_listFace.Contains(_other.gameObject) == false && m_b3cuTime == false)// 없으면 3쿠션!
                        {
                            // 일단 리스트 클리어
                            m_listFace.Clear();

                            // 돈 3배
                            sum = m_iMoneyPerHit * 3;

                            // 파티클
                            Create3CuParticle();

                            // 사운드 생성
                            PlayCollisionSound();

                            // 음성 사운드
                            m_asCollsion.clip = m_ac3Cusion;
                            m_asCollsion.pitch = 1.1f;
                            m_asCollsion.Play();

                            m_b3cuTime = true;
                            m_ui3Cu.SetActive(true);
                        }
                    }
                    else if(m_listFace.Count < 3) // 리스트가 충분히 쌓여 있지 않으면
                    {
                        m_listFace.Enqueue(_other.gameObject);

                        sum = m_iMoneyPerHit;

                        // 파티클 생성
                        CreateSplashParticle();

                        // 사운드 생성
                        PlayCollisionSound();
                    }
                }
                else
                    sum = m_iMoneyPerHit;
            }
        }
        else // 6챕터 전까지는 face만 충돌
        {
            if(_other.gameObject.tag == "face")
            {
                sum = m_iMoneyPerHit;

                // 파티클 생성
                CreateSplashParticle();

                // 사운드 생성
                PlayCollisionSound();
            }  
        }



        // 계산된 돈 전달...
        //GameDataManager.Instance.CoinSave(sum);


    }

    // 시간 검사
    void UpdateActingTime()
    {
        // 시간감소
        m_fRemainActingTime -= Time.fixedDeltaTime;

        // ui 업데이트
        if(m_uiTimeBar != null)
            m_uiTimeBar.value = m_fRemainActingTime / m_fMaxActingTime;

        // 시간 모두 소진시 죽음을 알린다
        if (m_fRemainActingTime <= 0.0f)
            cInGameData.Instance.DeadNorify(gameObject);
    }

    


    // 함수 수정되어야 함
    // 변경된 유저의 데이터 업데이트
    public void UpdateUserData(float _time)
    {
        m_fRemainActingTime = _time;
    }

    // 현재 히트당 돈 반환
    public int GetMoneyPerHit()
    {
        return m_iMoneyPerHit;
    }
}
