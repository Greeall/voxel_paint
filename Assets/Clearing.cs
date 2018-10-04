using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using UnityEditor;
using UnityEngine.SceneManagement;

public class Clearing : MonoBehaviour {


	public GameObject offerAboutClearing;
	public Text testo;
	public GameObject parentVoxels;
	public GameObject parentPlarforms;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


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

	//	testo.text = "before";
		File.Delete(Application.persistentDataPath + "/homeImg" + ItemController.I.selectedItem + ".png");
	//	File.Delete(Application.persistentDataPath + "/a");
	//	testo.text = "after";
		//AssetDatabase.DeleteAsset("Resources/homeImg"+ItemController.I.selectedItem); !1!!!! 
	//	DeleteVoxelsAndPlatforms();
	//	CloseOffer();
		//delete from home
		ItemController.I.DeleteLevelFromHomeMenu();
		SceneManager.LoadScene("Menu");
	}

	void DeleteVoxelsAndPlatforms()
	{
		Transform[] voxels = parentVoxels.GetComponentsInChildren<Transform>();
		for(int i = 1; i < voxels.Length; i++)
			Destroy(voxels[i].gameObject);

		Transform[] platforms = parentPlarforms.GetComponentsInChildren<Transform>();
		int firstLayer = ItemController.I.allModels[ItemController.I.selectedItem]._model[0].layer.Count;
		for(int i = 1 + firstLayer; i < platforms.Length; i++)
			Destroy(platforms[i].gameObject);
		

		//Model3d a = new Model3d();
		//a.DisplayLayer(1);
	}
}
