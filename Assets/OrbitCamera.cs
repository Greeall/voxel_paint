using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrbitCamera : MonoBehaviour {

	[SerializeField] private Transform obj;
	
	[SerializeField] private Transform generator;
	float offset;
	float restrictedOffset;
	
	public float speed = 4f;
	
	Camera _camera;

	
	Touch touch0, touch1;
	void Awake () {
		offset = Vector3.Distance(obj.transform.position, transform.position); //distance between camera and human
		restrictedOffset = offset;									// min distance = distance
		transform.LookAt(obj); 										//camera look at human
		_camera = GetComponent<Camera>();
	}

	void Update()
	{

	}	
	void LateUpdate () {
		
		float distance = Vector3.Distance(obj.transform.position, transform.position); //distance beetwen camera and human
		Vector3 translate = Vector3.zero; 

	/*	if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			GraphicRaycaster m_Raycaster = GetComponent<GraphicRaycaster>();
    		PointerEventData m_PointerEventData;
			m_PointerEventData = new PointerEventData(EventSystem.current);
            m_PointerEventData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(m_PointerEventData, results);

			float angle = transform.localEulerAngles.x;  //
			angle = (angle > 180) ? angle - 360 : angle; //read rotation.x with sign minus
			Vector3 _offset = Input.GetMouseButton(0) ? Vector3.one : -Vector3.one;
			Debug.Log(angle);
			if((angle > 12 && _offset.y > 0) ||
			   (angle < 88 && _offset.y < 0)   )
					transform.Rotate( - _offset.y * Time.deltaTime * speed * 4, 0, 0, Space.Self);
		}*/
		
		if(Input.touchCount == 1 && !Nib.painting && !modelMainPosition.I.isReductionProcess)
		{
			GraphicRaycaster m_Raycaster = GetComponent<GraphicRaycaster>();
    		PointerEventData m_PointerEventData;
			m_PointerEventData = new PointerEventData(EventSystem.current);
            m_PointerEventData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(m_PointerEventData, results);

			if(results.Count < 1)
			{
				Touch _touch = Input.GetTouch(0);
				Vector2 _offset = Vector2.zero;
				if(_touch.phase == TouchPhase.Moved)
					_offset = _touch.deltaPosition;
				else
					_offset = Vector2.zero;
		
				Vector3 rot = new Vector3(0f, _offset.x, 0);
				transform.Rotate(rot * Time.deltaTime * speed, Space.World);
				float angle = transform.localEulerAngles.x;  

			if((angle > 12 && _offset.y > 0) ||
			   (angle < 88 && _offset.y < 0)   )
					transform.Rotate( - _offset.y * Time.deltaTime * speed, 0, 0, Space.Self);
			}
		}


		if(Input.GetAxis("Mouse ScrollWheel") < 0 &&  _camera.orthographicSize < 10) //zoom ++
			_camera.orthographicSize++;
		else if(Input.GetAxis("Mouse ScrollWheel") > 0 && _camera.orthographicSize > 1) //zoom --
			_camera.orthographicSize--;

		if(Input.touchCount == 2)
		{
			float outDiff;
			if(isZoom(out outDiff))
			{
				_camera.orthographicSize += outDiff * Time.deltaTime;
				_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 1, 10);
			}
			else
			{
				Touch _touch = touch0;
				Vector2 direction =  _touch.deltaPosition;
				obj.transform.Translate( - transform.TransformDirection(direction * Time.deltaTime), Space.World);
			}	
		}

		Vector3 pos = obj.position - transform.forward * Mathf.Min(offset, restrictedOffset); //change min from 
		transform.position = pos + translate;
	}

	bool isZoom(out float delDiff)
	{
		float pixelBuffer = 5f;
		touch0 = Input.GetTouch(0);
		touch1 = Input.GetTouch(1);

		Vector2 offset0 = touch0.position - touch0.deltaPosition;
		Vector2 offset1 = touch1.position - touch1.deltaPosition;

		float preDistance =  (offset0 - offset1).magnitude;
		float nowDistance = (touch0.position - touch1.position).magnitude;

		float deltaDifference = preDistance - nowDistance;
		

		if(Mathf.Abs(deltaDifference) > pixelBuffer)
		{
			delDiff = deltaDifference;
			return true;
		}
		else
		{			
			delDiff = 0f;
			return false;
		}
	}

}
