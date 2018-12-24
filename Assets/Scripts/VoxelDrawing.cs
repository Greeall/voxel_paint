using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelDrawing : MonoBehaviour {

	void Update () {
		if((Input.GetKey(KeyCode.Mouse0) || Input.touchCount == 1) && Nib.painting)
		{
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
						Model3d.readyVoxels++;
						making_voxel.SmoothCreateVoxel();
						SetReadyVoxel(hit.transform.position);
					}
            	}
			}
		}
	

		else if(Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
		{
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
						Model3d.readyVoxels++;
						making_voxel.SmoothCreateVoxel();
						SetReadyVoxel(hit.transform.position);
					}
            	}
			}
		}

		if(!Input.GetKey(KeyCode.Mouse0))
		{
			Nib.painting = false;
		}
		
	}

	public void SetReadyVoxel(Vector3 pos)
	{
		List<VoxelPlatform> lay = ItemController.I.allModels[ItemController.I.selectedItem]._model[Model3d.staticLayer].layer;
		int i = 0;
		foreach(VoxelPlatform plat in lay)
		{
			if(pos.x == plat.position.x && pos.z == plat.position.y)
			{
				ItemController.I.allModels[ItemController.I.selectedItem]._model[Model3d.staticLayer].layer[i].isDrawing = true;
			}
			i++;
		}
	}
}
