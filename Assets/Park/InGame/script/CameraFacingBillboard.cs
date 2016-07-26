using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {
    
    ///////////////////////////////////////////// 
    // 일반적 빌보딩 로직은 아님

    // 포지션 y값 offset용
    public float m_fOffsetY;



	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // 위치는 월드 좌표에서 조정되도록한다
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + m_fOffsetY, transform.parent.position.z);

        // 회전값은 항상 초기화.. 카메라는 가만있으니깐
        transform.rotation = Quaternion.identity;
    }
}
