using UnityEngine;
using Unity.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Collections;


public class AsyncCapture : MonoBehaviour
{
    Queue<AsyncGPUReadbackRequest> _requests = new Queue<AsyncGPUReadbackRequest>();
	int size = 512;

    float time = 0;

	Camera screenCamera;

	void Start() {
		screenCamera = GetComponent<Camera>();
	}
    void Update()
    {
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
                var buffer = req.GetData<Color32>();

                StartCoroutine(SaveBitmap(buffer, screenCamera.pixelWidth, screenCamera.pixelHeight));

                _requests.Dequeue();
            }
            else
            {
                break;
            }
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        time += Time.deltaTime;
        if(time > 2f) {
    		AddRequest(source);
            time = 0;        
        }

        Graphics.Blit(source, destination);
    }

	void AddRequest(RenderTexture source) {
		// Debug.Log("render");
        if (_requests.Count == 0)
            _requests.Enqueue(AsyncGPUReadback.Request(source));
        // else
        //     Debug.Log("Too many requests.");
	}

    IEnumerator SaveBitmap(NativeArray<Color32> buffer, int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
		screenCamera.targetTexture = rt;
		screenCamera.Render();
		RenderTexture.active = rt;
        var stex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        stex.SetPixels32(buffer.ToArray());
        stex.Apply();
        yield return new WaitForSeconds(0.25f);
        var tex = new Texture2D(width, width, TextureFormat.RGBA32, false);
        tex.SetPixels(0, 0, width, width, stex.GetPixels(0, (height - width)/2, width, width, 0), 0);
        tex.Apply();
        yield return new WaitForSeconds(0.25f);
		Debug.Log(Application.persistentDataPath + "/homeImg"+ ItemController.I.selectedItem + ".jpg");
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/homeImg" + ItemController.I.selectedItem + ".jpg", ImageConversion.EncodeToJPG(tex) );
        Debug.Log("item " + ItemController.I.selectedItem);
        yield return new WaitForSeconds(0.25f);
        Destroy(tex);
    }
}