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

	public int modelsCount;

	public static ItemController I;
	
	void Awake()
	{
		I = this;
		ItemController.I.homeModels = new List<int>();
		ItemController.I.allModels = new List<Datas>();

		if(ES2.Exists("SelectedItem"))
			selectedItem = ES2.Load<int>("SelectedItem");
		else
			selectedItem = 0;

		if(ES2.Exists("firsLaunch"))
		{
			ReadLevelsFromPref();		
		}
		else
		{
			ReadLevelsFromStore();
		}

		ReadHomeLevelsFromStore();
	}
	
	void Start()
	{
		//SaveHomeItemsToPref();
	}


	void ReadLevelsFromPref()
	{
		for(int i = 0; i < modelsCount; i++)
		{
			if(ES2.Exists("model" + i))
				allModels.Add(ES2.Load<Datas>("model" + i));
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
		
		ES2.Save(1, "firsLaunch");
		SaveAllModelsToPref1();
	}

	

	public void SaveAllModelsToPref1()
	{
		for(int i = 0; i < modelsCount; i++)
			ES2.Save(allModels[i], "model" + i);		
	}




	public void ReadHomeLevelsFromStore()
	{
		int count;
		if(ES2.Exists("homeLevelsCount"))
			count = ES2.Load<int>("homeLevelsCount");
		else
			count = 0;

	
		for(int i = 0; i < count; i ++)
		{
			if(ES2.Exists("homeLevel" + i))
				homeModels.Add(ES2.Load<int>("homeLevel" + i));
			else
				homeModels.Add(0);
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
		ES2.Save(homeModels.Count, "homeLevelsCount");
	
		foreach(int item in homeModels)
		{
			ES2.Save(item, "homeLevel" + i);
			i++;
		}
	}
}