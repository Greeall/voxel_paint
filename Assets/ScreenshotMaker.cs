using UnityEngine;
//using Unity.Collections;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class ScreenshotMaker : MonoBehaviour {

	Camera screenCamera;

	float time = 0;

	Queue<AsyncGPUReadbackRequest> _requests = new Queue<AsyncGPUReadbackRequest>();

	int size = 512;

	// Use this for initialization
	void Start () {
		screenCamera = GetComponent<Camera>();
		screenCamera.transform.position = ItemController.I.allModels[ItemController.I.selectedItem]._mainPosition;		//
		screenCamera.transform.eulerAngles = ItemController.I.allModels[ItemController.I.selectedItem]._mainRotation;   //
		screenCamera.transform.Translate(Vector3.back * 100f);
		// StartCoroutine(MakingScreen());
	}

	void Update() {
		//ProcessScreenshot();
		time += Time.deltaTime;
		if (time > 2f) {
			time = 0;

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
	
	IEnumerator ScreenShot()
	{
		yield return new WaitForEndOfFrame();

		RenderTexture rt = new RenderTexture(size, size, 24);
		screenCamera.targetTexture = rt;
		screenCamera.Render();
		RenderTexture.active = rt;
		Texture2D virtualPhoto = new Texture2D(size, size, TextureFormat.RGB24, false);

		virtualPhoto.ReadPixels( new Rect(0, 0, size, size), 0, 0);

		yield return new WaitForSeconds(0.5f);
		
		Debug.Log(Application.persistentDataPath + "/homeImg"+ ItemController.I.selectedItem + ".png");
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/homeImg" + ItemController.I.selectedItem + ".png", virtualPhoto.EncodeToJPG() );
	}
/*
	void OnRenderImage(RenderTexture source, RenderTexture destination)
     {
         if (_requests.Count < 1)
             _requests.Enqueue(AsyncGPUReadback.Request(source));
         else
             Debug.Log("Too many requests.");
   
         Graphics.Blit(source, destination);
     }

	 void ProcessScreenshot() {
		while (_requests.Count > 0)
		{
			var req = _requests.Peek();

			if (req.hasError)
			{
				Debug.Log("GPU readback error detected.");
				_requests.Dequeue();
			}
			else if (req.done)
			{
				Texture2D virtualPhoto = new Texture2D(size, size, TextureFormat.RGB24, false);
				virtualPhoto.LoadRawTextureData(req.GetData<Texture2DArray>());
				Debug.Log(Application.persistentDataPath + "/homeImg"+ ItemController.I.selectedItem + ".png");
				System.IO.File.WriteAllBytes(Application.persistentDataPath + "/homeImg" + ItemController.I.selectedItem + ".png", virtualPhoto.EncodeToJPG());
				_requests.Dequeue();
			}
			else
			{
				break;
			}
		}
	 }

	 void RequestingScreenshots()
	{
		if (_requests.Count < 1) {
			RenderTexture rt = new RenderTexture(size, size, 24);
			screenCamera.targetTexture = rt;
			screenCamera.Render();
             _requests.Enqueue(AsyncGPUReadback.Request(rt));
		} else { 
             Debug.Log("Too many requests.");
		}
	}*/

	IEnumerator MakingScreen()
	{
		for(;;)
		{
			StartCoroutine(ScreenShot());
			yield return new WaitForSeconds(2f);
		}
	}

	// async void MakingScreen()
	// {
	// 	for(;;)
	// 	{
	// 		ScreenShot();
	// 		await Task.Delay(TimeSpan.FromSeconds(1));
	// 	}
	// }
}
