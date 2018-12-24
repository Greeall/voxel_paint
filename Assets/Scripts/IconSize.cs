using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSize : MonoBehaviour {

	public float coeff = 1f;
	// Use this for initialization
	void Start () {
		float width = Screen.width / 10f * coeff;
		GetComponent<RectTransform>().sizeDelta = new Vector2(width, width); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
