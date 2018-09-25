using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertScrollController : MonoBehaviour {

	ScrollRect _scrlRect;
	float buttonsWidht;

	RectTransform colorsPanel;
	// Use this for initialization
	void Start () {
		_scrlRect = GetComponent<ScrollRect>();
		RectTransform[] c = GetComponentsInChildren<RectTransform>();
		colorsPanel = c[1];
		Debug.Log(c[1]);
		Debug.Log(buttonsWidht + 45.5f);
		
		// colorsPanel.sizde
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
