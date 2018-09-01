using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelDrawing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetKey(KeyCode.Mouse0) || Input.touchCount == 1) && Nib.painting)
		{
			
			//Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
       		RaycastHit hit;
			
        	if (Physics.Raycast(raycast, out hit))
        	{
			            	
				MakingVoxel making_voxel = hit.transform.gameObject.GetComponent<MakingVoxel>();
				
				if(making_voxel)
            	{	
                	if(Nib.nibColor ==  hit.transform.GetComponent<ColorVoxel>().color && !hit.transform.GetComponent<MakingVoxel>().startCreating)
					{
						hit.transform.GetComponent<MakingVoxel>().startCreating = true;
						Debug.Log("1");
						Model3d.readyVoxels++;
						StartCoroutine(making_voxel.SmoothCreateVoxel());
					}
            	}
			}
		}
	

		else if(Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
		{
			
			
			// Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
			
       		RaycastHit hit;
        	if (Physics.Raycast(raycast, out hit))
        	{
				MakingVoxel making_voxel = hit.transform.gameObject.GetComponent<MakingVoxel>();
				if(making_voxel)
            	{
                	if(Nib.nibColor == hit.transform.GetComponent<ColorVoxel>().color &&  !hit.transform.GetComponent<MakingVoxel>().startCreating)
					{
						Nib.painting = true;
						hit.transform.GetComponent<MakingVoxel>().startCreating = true;
						Debug.Log("2");
						Model3d.readyVoxels++;
						StartCoroutine(making_voxel.SmoothCreateVoxel());
					}
            	}
			}
		}

		if(!Input.GetKey(KeyCode.Mouse0))
		{
			Nib.painting = false;
		}
		
	}
}
