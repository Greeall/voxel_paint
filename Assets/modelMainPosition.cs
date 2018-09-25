using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modelMainPosition : MonoBehaviour {

	public Vector3 centralAxisPos;
	public Vector3 mainCameraRot;

	public Transform mainCamera;

	public Transform centralAxis;

	bool isActive = true;
	bool isReductionProcess = false;

	public Sprite black;
	public Sprite gray;


	// Use this for initialization
	void Start () 
	{
		AdjustToNormalPosition();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckOffset();
		if(!isReductionProcess)
			CheckButtonImage();
	}

	public void SizeReduction()
	{
		if(isActive)
			StartCoroutine(Smooth());
	}

	IEnumerator Smooth()
	{
		
		isReductionProcess = true;
		GetComponent<Image>().sprite = gray;
		float startTime = Time.time;

		float tim = 2f;
		int steps = 40;

		float xRot = mainCamera.eulerAngles.x;
 		xRot = (xRot > 180) ? xRot - 360 : xRot;

		float yRot = mainCamera.eulerAngles.y;
		yRot = (yRot > 180) ? yRot - 360 : yRot;


		float dis = Vector3.Distance(centralAxis.position, centralAxisPos);
		bool isPositionOffset = (dis != 0) ? true : false;

		float xx = xRot;
		float xy = yRot;
		
		//mainCamera.eulerAngles = new Vector3(xRot, 302, 0f);

		for(int i = 0; i < steps; i++)
		{
			float stepX = 1f / steps * i;
			if(isPositionOffset)
				centralAxis.position = Vector3.Lerp(centralAxis.position, centralAxisPos, stepX);

			
			xRot = Mathf.Lerp(xRot, mainCameraRot.x, stepX);
			yRot = Mathf.Lerp(yRot, mainCameraRot.y, stepX);
			
			mainCamera.eulerAngles = new Vector3(xRot, yRot, 0f);
			yield return new WaitForSeconds(tim/ steps);
		}
		isReductionProcess = false;
		isActive = false;
	}

	void CheckOffset()
	{
		if(centralAxis.position != centralAxisPos || mainCamera.eulerAngles != mainCameraRot)
			isActive = true;
	}

	void CheckButtonImage()
	{
		if(isActive)
		{
			GetComponent<Image>().sprite = black;
		}
		else
		{
			GetComponent<Image>().sprite = gray;
		}

	}

	void AdjustToNormalPosition()
	{
		mainCameraRot = ItemController.I.allModels[ItemController.I.selectedItem]._mainRotation;
		centralAxisPos = ItemController.I.allModels[ItemController.I.selectedItem]._mainPosition;
		centralAxis.position = centralAxisPos;
		mainCamera.eulerAngles = mainCameraRot;
		mainCamera.GetComponent<Camera>().orthographicSize = ItemController.I.allModels[ItemController.I.selectedItem]._mainZoom;
	}

}
