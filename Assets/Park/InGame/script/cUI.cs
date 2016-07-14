using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cUI : MonoBehaviour {

    // fever gauge들의 부모
    public Transform m_objFeverGauge;

    // fever guage들의 Image 컴포넌트
    private Image m_imgFeverGauge;

    // fever gauge들의 maxium size
    private Vector2 m_v2gaugeMaxSize;


    
    

	// Use this for initialization
	void Awake () {
        // 게이지 모든 값 초기화
        m_imgFeverGauge = m_objFeverGauge.transform.FindChild("UI_feverGauge_content").GetComponent<Image>();
        m_v2gaugeMaxSize = m_imgFeverGauge.rectTransform.sizeDelta;
        m_imgFeverGauge.rectTransform.sizeDelta = new Vector2(0, m_v2gaugeMaxSize.y);
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        UpdateGauge(cInGameData.Instance.GetGaugeRatio());
	}

    // 게이지 업데이트
    void UpdateGauge(float _ratio)
    {
        // 길이 업데이트
        m_imgFeverGauge.rectTransform.sizeDelta = new Vector2(m_v2gaugeMaxSize.x * _ratio , m_v2gaugeMaxSize.y);

        // 컬러 업데이트
        if(_ratio < 1.0f)
            m_imgFeverGauge.color = new Color(1.0f, 1- _ratio, 1- _ratio);
        else if(_ratio >= 1.0f)
            m_imgFeverGauge.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    
}
