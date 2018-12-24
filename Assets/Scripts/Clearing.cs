using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using UnityEditor;
using UnityEngine.SceneManagement;

public class Clearing : MonoBehaviour {


	public GameObject offerAboutClearing;
	public GameObject parentVoxels;
	public GameObject parentPlarforms;

	public void OpenOffer()
	{
		offerAboutClearing.SetActive(true);
	}

	public void CloseOffer()
	{
		offerAboutClearing.SetActive(false);
	}

	public void Again()
	{
		Datas model = ItemController.I.allModels[ItemController.I.selectedItem];
		foreach(Layer l in model._model)
		{
			l.isDrawing = false;
			foreach(VoxelPlatform p in l.layer)
			{
				p.isDrawing = false;
			}
		}
		model._isBeginning = false;
		model._isFinished = false;

		ItemController.I.allModels[ItemController.I.selectedItem] = model;
		ItemController.I.SaveAllModelsToPref1();
		ItemController.I.DeleteLevelFromHomeMenu();
		SceneManager.LoadScene("Menu");
	}
}
