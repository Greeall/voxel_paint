using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingVoxel : MonoBehaviour {

	public GameObject voxelPrefab;

	public bool startCreating = false;

	// Use this for initialization
	void Start () {
//		Debug.Log("start making voxel");
	}
	
	// Update is called once per frame
	void Update () {
	
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

	

	public IEnumerator SmoothCreateVoxel()
	{
		
		float time = 0.3f;
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
	}
}
