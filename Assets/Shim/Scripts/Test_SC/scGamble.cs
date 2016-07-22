using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.


public class scGamble : MonoBehaviour {

    Button btnOdd;
    Button btnEven;
    Button btnNumber;
    Button btnGetCoin;

    void Awake()
    {
        btnOdd = transform.FindChild("Odd_Num").gameObject.GetComponent<Button>();
        btnEven = transform.FindChild("Even_Num").gameObject.GetComponent<Button>();
        btnNumber = transform.FindChild("Number").gameObject.GetComponent<Button>();
        btnGetCoin = transform.FindChild("Btn_GetPrize").gameObject.GetComponent<Button>();
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
