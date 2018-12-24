using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertScrollController : MonoBehaviour {

	ScrollRect _scrlRect;

	RectTransform colorsPanel;

	void Start () {
		_scrlRect = GetComponent<ScrollRect>();
		RectTransform[] c = GetComponentsInChildren<RectTransform>();
		colorsPanel = c[1];
	}
}
