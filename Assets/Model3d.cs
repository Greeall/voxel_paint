using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model3d : MonoBehaviour {

	public static int readyVoxels = 0;
	public Transform platformParent;
	int layer;
	public Material platformMaterial;

	//[SerializeField]
	public List<VoxelColor> colors;
	//[SerializeField]
	public List<Layer> model;

	public GameObject platformPrefab;


	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () {
		
		int changed = ItemController.I.selectedItem;
		
		colors = new List<VoxelColor>(ItemController.I.models[changed]._colors);
		model = new List<Layer>(ItemController.I.models[changed]._model);
		

		layer = 0;
		DisplayLayer(layer);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CheckAllVoxelOnLayer() && readyVoxels > 0)
		{
			readyVoxels = 0;
			layer += 1;
			if(layer < model.Count - 1)
			{
				Debug.Log("LAYER - " + layer);
				DisplayLayer(layer);
			}
		}
		
	}

	bool CheckAllVoxelOnLayer()
	{
		if(readyVoxels == model[layer].layer.Count)
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

