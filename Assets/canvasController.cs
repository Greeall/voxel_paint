using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasController : MonoBehaviour {

	public Button homeImg;
	public Button catalogImg;
	// Use this for initialization
	void Start () {
		float widht = Screen.width / 5f;
		float w = widht / 60;
		//1 = 60
		homeImg.transform.localScale = new Vector3(w, w, 0);
		catalogImg.transform.localScale = new Vector3(w, w, 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
