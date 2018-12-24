using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneLine : MonoBehaviour {

	public RectTransform obj;

	void Start ()
	{
		RectTransform rct = GetComponent<RectTransform>();
		rct.sizeDelta = obj.sizeDelta;
	}
}
