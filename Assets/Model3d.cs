using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Model3d : MonoBehaviour {

	public static int readyVoxels = 0;
	public Transform platformParent;

	public Camera mainCamera;

	public GameObject canvas;

	public GameObject floor;

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

	public bool isVIP; //to datas

	

	public GameObject platformPrefab;

	public ParticleSystem firework;


	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () {
		
		int changed = ItemController.I.selectedItem;
		
		colors = new List<VoxelColor>(ItemController.I.allModels[changed]._colors);
		model = new List<Layer>(ItemController.I.allModels[changed]._model);
		
		DisplayReadyVoxels();

		if(!ItemController.I.allModels[changed]._isFinished)
			DisplayLayer(staticLayer); 

		
	
	//	SaveToTxt();
	}

	void DisplayReadyVoxels()
	{
		bool firstUnreadyVoxelIsFound = false;
		int i = 0;
		int _readyVxls = 0;
		
		foreach(Layer l in model)
		{
			_readyVxls = 0;
			foreach(VoxelPlatform v in l.layer)
			{
				if(v.isDrawing) //вносить изменения сюда а не только в итемконтроллер или добывать данные из итемконтроллера???
				{
					GameObject voxel = Instantiate(voxelPrefab, new Vector3(v.position.x, i, v.position.y), voxelPrefab.transform.rotation) as GameObject;
					voxel.GetComponentInChildren<Renderer>().material.color = v.FindColor(colors);
					voxel.transform.localScale = new Vector3(1,1,1);
					_readyVxls += 1;
				}
				else if(!firstUnreadyVoxelIsFound)
				{
					firstUnreadyVoxelIsFound = true;
					staticLayer = i;
				}
			}
			i++;
			if(firstUnreadyVoxelIsFound)
				break;
		}

		readyVoxels = _readyVxls;
	}
	
	void SaveToTxt()
	{
		Datas data = new Datas(colors, model, false, false, mainPosition, mainRotation, mainZoom, isVIP);
		string txt = JsonUtility.ToJson(data);
		System.IO.File.WriteAllText("modello.txt", txt);
	}

	void Update () 
	{
		if(Input.GetKeyDown("e"))
			Ending();

		if(staticLayer < model.Count)
		{
			if(CheckAllVoxelOnLayer() && readyVoxels > 0)
			{
				ItemController.I.allModels[ItemController.I.selectedItem]._model[staticLayer].isDrawing = true;
				readyVoxels = 0;
				staticLayer += 1;
				if(staticLayer < model.Count)
					DisplayLayer(staticLayer);
			}
		}
		if(staticLayer == model.Count && ItemController.I.allModels[ItemController.I.selectedItem]._isFinished != true)
		{
			ItemController.I.allModels[ItemController.I.selectedItem]._isFinished = true;
			ItemController.I.allModels[ItemController.I.selectedItem]._isBeginning = false;
			Ending();
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

	void Ending()
	{
		StartCoroutine(modelMainPosition.I.Smooth());
		float zoom = ItemController.I.allModels[ItemController.I.selectedItem]._mainZoom;
		floor.SetActive(false);
		canvas.SetActive(false);
		firework.gameObject.transform.localScale = new Vector3(zoom * 2/ 10f, zoom * 2/ 10f, zoom * 2/ 10f);
		StartCoroutine(Rotation());
		
		
	}

	IEnumerator Rotation()
	{
		float time = 2f;
		int steps = 40;
		Color lerpedColor;
		for(int i = 0; i < steps; i++)
		{
			lerpedColor = Color.Lerp(Color.white, new Color(0.141f, 0.019f, 0.152f, 1f), (float)i/steps);
			mainCamera.backgroundColor = lerpedColor;
			float zoom = ItemController.I.allModels[ItemController.I.selectedItem]._mainZoom;
			float z = Mathf.Lerp(zoom, 2 * zoom, (float)i/steps);
			mainCamera.orthographicSize = z;
			yield return new WaitForSeconds (time/steps);
		}
		firework.gameObject.SetActive(true);
		
	}
}

