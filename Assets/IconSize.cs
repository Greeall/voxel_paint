using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSize : MonoBehaviour {

	public float coeff;
	// Use this for initialization
	void Start () {
		float widthHeight = Screen.width / coeff;
		GetComponent<RectTransform>().sizeDelta = new Vector2(widthHeight, widthHeight);
	}
}
