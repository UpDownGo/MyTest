using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class cSplashObject : MonoBehaviour {
    // 이전 충돌체
    private GameObject m_objPreCollider;
    
    // 저항(0 ~ 1.0)
    private float m_fDrag;

    // 사운드
    public AudioClip m_acCollisionSound;

    // 파티클
    public GameObject m_prfSplashParticle;

    
    ////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake() 초기화용 변수
    /// 단순 인터페이스 역할만
    /// </summary>
    // 돈/히트
    private int m_iMoneyPerHit = GameDataManager.Instance.userData.hitPoint[0];

	// Use this for initialization
	void Awake () {
        m_fDrag = gameObject.GetComponent<Rigidbody>().drag;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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
            if (m_objPreCollider == _other.gameObject) return;   // 이전 충돌체와 같다면 돈계산 하지 않음
            else m_objPreCollider = _other.gameObject;           // 이전 충돌체와 다르다면 이전 충돌체로 저장
        }

        // 충돌!
        Collision(_other);
    }

    // 충돌!
   void Collision(Collision _other)
    {
        // 파티클 생성
        CreateSplashParticle();

        // 사운드 생성
        PlayCollisionSound();

        // 돈계산
        // 공끼리라면 두 물체 히트당 돈의 합의 절반(소수 버림)
        int sum = 0;
        if (_other.gameObject.tag == "splash object")    // 공끼리
        {
            sum = m_iMoneyPerHit + _other.gameObject.GetComponent<cSplashObject>().GetMoneyPerHit();
            sum /= 2;
        }
        else// if (_other.gameObject.tag == "face") // 벽이라면        
            sum = m_iMoneyPerHit;


        // 계산된 돈 전달...
        GameDataManager.Instance.CoinSave(sum);


    }


    // 변경된 유저의 데이터 업데이트
    public void UpdateUserData()
    {

    }

    // 현재 히트당 돈 반환
    public int GetMoneyPerHit()
    {
        return m_iMoneyPerHit;
    }
}
