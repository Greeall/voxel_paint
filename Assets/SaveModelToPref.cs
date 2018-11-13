using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveModelToPref : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Save());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Save()
	{
		for(;;)
		{
			string m = JsonUtility.ToJson(ItemController.I.allModels[ItemController.I.selectedItem]);
			yield return new WaitForSeconds(1f);
			PlayerPrefs.SetString("model"+ ItemController.I.selectedItem, m);
			yield return new WaitForSeconds(1f);
		}
	}
}
