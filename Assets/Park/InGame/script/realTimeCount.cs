using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class realTimeCount : MonoBehaviour {
    int m_cntCol;
    int m_cntShake;
    Text m_text;

	// Use this for initialization
	void Awake () {
        m_cntCol = 0;
        m_cntShake = 0;
        m_text = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        m_text.text = "col cnt : " + m_cntCol.ToString() + " // " + "shake cnt" + m_cntShake.ToString();
    }

    public void PlusCollisionCount()
    {
        m_cntCol++;
    }

    public void PlusShakeCount()
    {
        m_cntShake++; 
    }
}
