using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model3d : MonoBehaviour {

	public static int readyVoxels = 0;
	public Transform platformParent;
	public static int staticLayer = 0;
	public Material platformMaterial;

	public GameObject voxelPrefab;

	//[SerializeField]
	public List<VoxelColor> colors;  //to datas
	//[SerializeField]
	public List<Layer> model;  //to datas

	public bool isBeginning = false; //to datas

	//public bool isReady = false;

	public Vector3 mainPosition; //to datas
	 
	public Vector3 mainRotation; //to datas

	public float mainZoom;  // to datas

	

	public GameObject platformPrefab;


	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () {
		
		int changed = ItemController.I.selectedItem;
		
		colors = new List<VoxelColor>(ItemController.I.allModels[changed]._colors);
		model = new List<Layer>(ItemController.I.allModels[changed]._model);
		
		
		DisplayReadyVoxels();
		DisplayLayer(staticLayer);
		
		//SaveToTxt();
	}

	void DisplayReadyVoxels()
	{
		bool firstUnreadyVoxelIsFound = false;
		int i = 0;
		
		foreach(Layer l in model)
		{
			foreach(VoxelPlatform v in l.layer)
			{
				if(v.isDrawing) //вносить изменения сюда а не только в итемконтроллер или добывать данные из итемконтроллера???
				{
					GameObject voxel = Instantiate(voxelPrefab, new Vector3(v.position.x, staticLayer, v.position.y), voxelPrefab.transform.rotation) as GameObject;
					voxel.GetComponentInChildren<Renderer>().material.color = v.FindColor(colors);
					voxel.transform.localScale = new Vector3(1,1,1);
					//Debug.Log("CREATING");//не візівается
				}
				else if(!firstUnreadyVoxelIsFound)
				{
					firstUnreadyVoxelIsFound = true;
					staticLayer = i;
				}
			}
			i++;
		}

	

	}
	
	void SaveToTxt()
	{
		Datas data = new Datas(colors, model, false, mainPosition, mainRotation, mainZoom);
		string txt = JsonUtility.ToJson(data);
		System.IO.File.WriteAllText("model.txt", txt);
	}

	// Update is called once per frame
	void Update () 
	{
		if(CheckAllVoxelOnLayer() && readyVoxels > 0)
		{
			readyVoxels = 0;
			staticLayer += 1;
			if(staticLayer < model.Count - 1)
			{
				//Debug.Log("LAYER - " + staticLayer);
				DisplayLayer(staticLayer);
			}
		}

		
		
	}

	

	bool CheckAllVoxelOnLayer()
	{
		if(readyVoxels == model[staticLayer].layer.Count)
			return true;
		else
			return false;
	}

	void DisplayLayer(int i)
	{
		foreach(VoxelPlatform v in model[i].layer)
		{
			Vector3 pos = new Vector3(v.position.x, i + 0.001f, v.position.y);
			GameObject platform = Instantiate(platformPrefab, pos, platformPrefab.transform.rotation);
			platform.transform.SetParent(platformParent);
			Material newMaterial = new Material(platformMaterial);
			newMaterial.mainTexture = v.FindTexture(colors);
			platform.GetComponent<MeshRenderer>().material = newMaterial;

			platform.GetComponent<ColorVoxel>().color = v.FindColor(colors);
		}
	}
}

