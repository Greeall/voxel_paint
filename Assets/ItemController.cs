using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.IO;

public class ItemController : MonoBehaviour {

	[HideInInspector] public int selectedItem = 0;
	[HideInInspector] public List<Datas> allModels;
	[HideInInspector] public List<int> homeModels;



	public Text testo;

	public int modelsCount;

	public static ItemController I;
	
	void Awake()
	{
		I = this;
		ItemController.I.homeModels = new List<int>();
		ItemController.I.allModels = new List<Datas>();
		selectedItem = PlayerPrefs.GetInt("SelectedItem", 0);
		//PlayerPrefs.SetInt("firstLaunch", 0);
		if(PlayerPrefs.GetInt("firstLaunch", 0) == 0)
		{
			ReadLevelsFromStore();
			//Debug.Log("read from store");
		}
		else
		{
			ReadLevelsFromPref();
			//Debug.Log("read from pref");
		}

		ReadHomeLevelsFromStore();
	}

	void Update()
	{
		SaveAllModelsToPref();
		if(Input.GetKeyDown("b"))
		{
			LookDrawnVoxel();
		}
	}

	void LookDrawnVoxel()
	{
		foreach(Layer l in allModels[0]._model)
		{
			foreach(VoxelPlatform p in l.layer)
			{
				Debug.Log(p.isDrawing);
			}
		}
	}

	
	void Start()
	{
		//SaveHomeItemsToPref();
	}

	void ReadLevelsFromPref()
	{
		for(int i = 0; i < modelsCount; i++)
		{
			string m = PlayerPrefs.GetString("model"+i, "");
			allModels.Add(JsonUtility.FromJson<Datas>(m));
		}
	}

	

	public void ReadLevelsFromStore()
	{
		for(int i = 0; i < modelsCount; i++)
		{
			TextAsset level = Resources.Load<TextAsset>("model" + i);
			string levelTxtString = level.text;
			allModels.Add(JsonUtility.FromJson<Datas>(levelTxtString));
		}
		PlayerPrefs.SetInt("firstLaunch", 1);
		SaveAllModelsToPref();
	}

	void SaveAllModelsToPref()
	{
		for(int i = 0; i < modelsCount; i++)
		{
			string m = JsonUtility.ToJson(allModels[i]);
			PlayerPrefs.SetString("model"+i, m);
		}
	}

	public void ReadHomeLevelsFromStore()
	{
		int count = PlayerPrefs.GetInt("homeLevelsCount", 0);
		for(int i = 0; i < count; i ++)
		{
			homeModels.Add(PlayerPrefs.GetInt("homeLevel" + i, 0));
		}
	}


	public void AddLevelToHomeMenu()
	{
		if(!homeModels.Contains(selectedItem))
		{
			homeModels.Add(selectedItem);
		}
		SaveHomeItemsToPref();
	}

	public void DeleteLevelFromHomeMenu()
	{
		/*foreach(int i in homeModels)
		{
			if(selectedItem == i)
				homeModels.Remove(i);
		}*/


		for(int i = 0; i < homeModels.Count; i ++)
		{
			if(selectedItem == homeModels[i])
				homeModels.Remove(homeModels[i]);
			
		}

		
		SaveHomeItemsToPref();
	}

	void SaveHomeItemsToPref()
	{
		int i = 0;
		PlayerPrefs.SetInt("homeLevelsCount", homeModels.Count);
		foreach(int item in homeModels)
		{
			PlayerPrefs.SetInt("homeLevel" + i, item);
			i++;
		}
	}

	
}

