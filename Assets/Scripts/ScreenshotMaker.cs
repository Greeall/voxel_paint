using UnityEngine;
//using Unity.Collections;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class ScreenshotMaker : MonoBehaviour {

	//ДОРАБОТАТЬ В БУДУЩЕМ
	Camera screenCamera;

//	float time = 0;

//	Queue<AsyncGPUReadbackRequest> _requests = new Queue<AsyncGPUReadbackRequest>();

	

	int size = 512;

	// Use this for initialization
	void Start () {
		screenCamera = GetComponent<Camera>();
		screenCamera.transform.position = ItemController.I.allModels[ItemController.I.selectedItem]._mainPosition;		//
		screenCamera.transform.eulerAngles = ItemController.I.allModels[ItemController.I.selectedItem]._mainRotation;   //
		screenCamera.transform.Translate(Vector3.back * 100f);
		ScreenShot();
	}

	void Update() {
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				ItemController.I.SaveAllModelsToPref1();
				Application.LoadLevel("Menu");
			}
		}
	}

	void OnApplicationPause(bool pauseStatus)
    {
       ScreenShot();
    }
	
	void ScreenShot()
	{
		//yield return new WaitForEndOfFrame();

		RenderTexture rt = new RenderTexture(size, size, 24);
		screenCamera.targetTexture = rt;
		screenCamera.Render();
		RenderTexture.active = rt;
		Texture2D virtualPhoto = new Texture2D(size, size, TextureFormat.RGB24, false);

		virtualPhoto.ReadPixels( new Rect(0, 0, size, size), 0, 0);

		//yield return new WaitForSeconds(0.5f);
		
		Debug.Log(Application.persistentDataPath + "/homeImg"+ ItemController.I.selectedItem + ".png");
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/homeImg" + ItemController.I.selectedItem + ".png", virtualPhoto.EncodeToJPG() );
	}


}
