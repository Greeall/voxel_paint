using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSize : MonoBehaviour {

	public float coeff = 1f;

	void Start()
	{
		GetComponent<Text>().fontSize = (int)(Screen.width / 10f * coeff);
	}
}
