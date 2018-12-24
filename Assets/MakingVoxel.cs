using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingVoxel : MonoBehaviour {

	public GameObject voxelPrefab;

	public bool startCreating = false;

	public float speed = 3f;

	GameObject voxel;
	public bool isCreating = false;
	
	// Update is called once per frame
	void Update () {
		
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

		
/*	void OnMouseEnter()
	{
		if(Input.GetKey(KeyCode.Mouse0) && Nib.painting)
		{
			if(Nib.nibColor == GetComponent<ColorVoxel>().color)
			{
				Model3d.readyVoxels++;
				StartCoroutine(SmoothCreateVoxel());
				//Destroy(gameObject);
			}
		}
	}

	void OnMouseDown()
	{
		Nib.painting = true;
		if(Nib.nibColor == GetComponent<ColorVoxel>().color)
		{
			Model3d.readyVoxels++;
			StartCoroutine(SmoothCreateVoxel());
			
		}	
	}
	*/

	public void SmoothCreateVoxel()
	{
		voxel = Instantiate(voxelPrefab, new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z), voxelPrefab.transform.rotation) as GameObject;
		voxel.transform.SetParent(Settings.I.parentForVoxels.transform);
		voxel.GetComponentInChildren<Renderer>().material.color = transform.GetComponent<ColorVoxel>().color;
		isCreating = true;
	}

	/*public IEnumerator SmoothCreateVoxel()
	{
		
		float time = 0.005f / Time.deltaTime;
		//Debug.Log(time);
		int steps = 20;
		float y = 0;
		GameObject voxel = Instantiate(voxelPrefab, new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z), voxelPrefab.transform.rotation) as GameObject;
		voxel.transform.SetParent(Settings.I.parentForVoxels.transform);


		voxel.GetComponentInChildren<Renderer>().material.color = transform.GetComponent<ColorVoxel>().color;
		for(int i = 0; i < steps; i ++)
		{
			
			voxel.transform.localScale = 
				new Vector3(voxel.transform.localScale.x, y += 1f/steps, voxel.transform.localScale.z);
			yield return new WaitForSeconds(time/steps);
		}
		Destroy(gameObject);
	}*/
}
