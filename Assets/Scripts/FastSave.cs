using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSave : MonoBehaviour {


	bool isProcess = false;

	void Update () 
	{
		if(Nib.painting && !isProcess)
		{
			StartCoroutine(Save());
			isProcess = true;
		}
		else if(!Nib.painting)
		{
			StopAllCoroutines();
			isProcess = false;
		}
	}

	IEnumerator Save()
	{
		for(;;)
		{
			ES2.Save(ItemController.I.allModels[ItemController.I.selectedItem], "model" + ItemController.I.selectedItem);
			yield return new WaitForSeconds(1f);
		}
	}
}
