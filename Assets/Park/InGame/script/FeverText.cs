using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeverText : MonoBehaviour {

    private Image m_imgText;

    private bool m_bIsFulled;

    private float m_fAlpha;

    public float m_fUpSpeed;
    public float m_fDownSpeed;

	// Use this for initialization
	void Start () {
        m_imgText = gameObject.GetComponent<Image>();

        m_bIsFulled = false;
        m_fAlpha = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_fAlpha >= 1.0f) m_bIsFulled = true;
        else if (m_fAlpha <= 0.0f && m_bIsFulled)
        {
            m_bIsFulled = false;
            gameObject.SetActive(false);
        }

        if(m_bIsFulled) m_fAlpha -= Time.fixedDeltaTime * m_fDownSpeed;
        else m_fAlpha += Time.fixedDeltaTime * m_fUpSpeed;

        m_imgText.color = new Color(m_imgText.color.r, m_imgText.color.g, m_imgText.color.b, m_fAlpha);
    }
}
