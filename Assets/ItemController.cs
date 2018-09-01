using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class ItemController : MonoBehaviour {



	public int selectedItem = 0;
	public List<Datas> models;

	
	

	public static ItemController I;

	// Use this for initialization
	
	
	void Awake()
	{
		I = this;
		ItemController.I.models = new List<Datas>();
		ReadLevelsFromStore();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void ReadLevelsFromStore()
	{
		int count = PlayerPrefs.GetInt("levelsCount", 0);
		for(int i = 0; i < count; i ++)
		{
			string levelTxtString = PlayerPrefs.GetString("level" + i, "");
			ItemController.I.models.Add(JsonUtility.FromJson<Datas>(levelTxtString));
		}
	}

	
}

