using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSize : MonoBehaviour {

	public float coeff = 1;

	float width = Screen.width;
	// Use this for initialization
	void Start () {
		GetComponent<Text>().fontSize = (int)((width / 17f) * coeff);
	//	this.GetComponent<Text>().fontSize = 30;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
