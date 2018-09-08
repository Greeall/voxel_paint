using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class DataModel3d : Model3d {

	public GameObject platformPrefab;

	public static int readyVoxels = 0;
	public Transform platformParent;
	int layer;
	public Material platformMaterial;
	// Use this for initialization

	
void Start () {
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
		foreach(VoxelPlatform voxel_platform in model[i].layer)
		{
			Vector3 pos = new Vector3(voxel_platform.position.x, i + 0.001f, voxel_platform.position.y);
			GameObject platform = Instantiate(platformPrefab, pos, platformPrefab.transform.rotation);
			platform.transform.SetParent(platformParent);

			Material newMaterial = new Material(platformMaterial);
			newMaterial.mainTexture = voxel_platform.FindTexture(colors);
			platform.GetComponent<MeshRenderer>().material = newMaterial;

			platform.GetComponent<ColorVoxel>().color = voxel_platform.FindColor(colors);
		}
	}

}
*/