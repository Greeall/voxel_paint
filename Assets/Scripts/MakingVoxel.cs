using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingVoxel : MonoBehaviour {

	public GameObject voxelPrefab;

	public bool startCreating = false;

	public float speed = 3f;

	GameObject voxel;
	public bool isCreating = false;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.X))
			SmoothCreateVoxel();

		if(isCreating)
		{
			if(voxel.transform.localScale.y < 1)
			{
				voxel.transform.localScale = 
					new Vector3(voxel.transform.localScale.x, voxel.transform.localScale.y + Time.deltaTime * speed, voxel.transform.localScale.z);
			}
			else
			{
				voxel.transform.localScale = 
					new Vector3(voxel.transform.localScale.x, 1f, voxel.transform.localScale.z);
				isCreating = false;
				Destroy(gameObject);
			}
		}		
	}

	public void SmoothCreateVoxel()
	{
		voxel = Instantiate(voxelPrefab, new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z), voxelPrefab.transform.rotation) as GameObject;
		voxel.transform.SetParent(Settings.I.parentForVoxels.transform);
		voxel.GetComponentInChildren<Renderer>().material.color = transform.GetComponent<ColorVoxel>().color;
		isCreating = true;
	}
}
