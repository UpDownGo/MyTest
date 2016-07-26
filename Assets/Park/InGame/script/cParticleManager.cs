using UnityEngine;
using System.Collections;

public class cParticleManager : MonoBehaviour {

    // 자기 파티클을 담을 변수
    private ParticleSystem m_psParticle;

	// Use this for initialization
	void Start () {
        m_psParticle = gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!m_psParticle.IsAlive())
            Destroy(gameObject);
	}
}
