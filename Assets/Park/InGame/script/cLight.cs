using UnityEngine;
using System.Collections;

public class cLight : MonoBehaviour {

    private Light m_lgMyLight;

	// Use this for initialization
	void Start () {
        m_lgMyLight = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        m_lgMyLight.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        transform.Rotate(new Vector3(Random.Range(0.0f, 20.0f), Random.Range(0.0f, 20.0f), Random.Range(0.0f, 20.0f)));
    }
}
