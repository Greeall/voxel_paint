using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour {

	Camera screenCamera;
	
	// Use this for initialization
	void Start () {
		screenCamera = GetComponent<Camera>();
		Debug.Log("SELECTED = " + ItemController.I.allModels.Count);
		screenCamera.transform.position = ItemController.I.allModels[ItemController.I.selectedItem]._mainPosition;		//
		screenCamera.transform.eulerAngles = ItemController.I.allModels[ItemController.I.selectedItem]._mainRotation;   //
		screenCamera.transform.Translate(Vector3.back * 100f);
		StartCoroutine(MakingScreen());
	}
	


	IEnumerator MakingScreen()
	{
		for(;;)
		{
			ScreenShot();
			yield return new WaitForSeconds(15f);
		}
	}

	void ScreenShot()
	{
		int height = Screen.height ;
			int width = Screen.width; 
			int size = 512;
			RenderTexture rt = new RenderTexture(size, size, 24);
			screenCamera.targetTexture = rt;
			screenCamera.Render();
			RenderTexture.active = rt;
     		Texture2D virtualPhoto = new Texture2D(size, size, TextureFormat.RGB24, false);
     
     		virtualPhoto.ReadPixels( new Rect(0, 0, size, size), 0, 0);
			
     		byte[] bytes;
     		bytes = virtualPhoto.EncodeToPNG();
     
     		System.IO.File.WriteAllBytes("Assets\\Resources\\homeImg" + ItemController.I.selectedItem + ".png", bytes );
	}
}
