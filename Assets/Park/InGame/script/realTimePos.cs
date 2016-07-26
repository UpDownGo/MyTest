using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class realTimePos : MonoBehaviour {

    Text m_text;

	// Use this for initialization
	void Start () {
        m_text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        m_text.text = "curGauge" + " : " + cInGameData.Instance.GetGaugeRatio().ToString();
     
    }
}