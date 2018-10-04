using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour {

	Camera screenCamera;
	
	// Use this for initialization
	void Start () {
		screenCamera = GetComponent<Camera>();
		screenCamera.transform.position = ItemController.I.allModels[ItemController.I.selectedItem]._mainPosition;		//
		screenCamera.transform.eulerAngles = ItemController.I.allModels[ItemController.I.selectedItem]._mainRotation;   //
		screenCamera.transform.Translate(Vector3.back * 100f);
		StartCoroutine(MakingScreen());

		
	}

	// void Update() {
	// 	if (Random.Range(1, 60) == 5) {
	// 		ScreenShot();
	// 	}
	// }
	


	IEnumerator MakingScreen()
	{
		for(;;)
		{
			ScreenShot();
			yield return new WaitForSeconds(1f);
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
		
		Debug.Log(Application.persistentDataPath + "/homeImg"+ ItemController.I.selectedItem + ".png");
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/homeImg" + ItemController.I.selectedItem + ".png", virtualPhoto.EncodeToJPG() );
	}
}
