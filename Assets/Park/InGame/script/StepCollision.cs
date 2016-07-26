using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StepCollision : MonoBehaviour {

    BoxCollider2D m_collider;
    Image m_image;

	// Use this for initialization
	void Start () {
        m_collider = gameObject.GetComponent<BoxCollider2D>();
        m_image = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        m_collider.size = new Vector2(m_image.rectTransform.sizeDelta.x, m_image.rectTransform.sizeDelta.y);
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Asd");

    
    }
}
