using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clearing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Again()
	{
		Datas model = ItemController.I.allModels[ItemController.I.selectedItem];
		foreach(Layer l in model._model)
		{
			l.isDrawing = false;
			foreach(VoxelPlatform p in l.layer)
			{
				p.isDrawing = false;
			}
		}
		model._isBeginning = false;
		model._isFinished = false;
		
		
	}
}
