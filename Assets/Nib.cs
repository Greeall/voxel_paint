using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nib : MonoBehaviour {

	public GameObject colorNumberPrefab;


	public Model3d modelGenerator;
	public static int nib = 0;

	public static Color nibColor = Color.clear;
	public static bool painting = false;

	//public GameObject reductionButton;

	void Start () {
		DisplayPalette();
		RectTransform _rect = GetComponent<RectTransform>();
		_rect.sizeDelta = new Vector2(_rect.sizeDelta.x, Screen.height/ 9f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!Input.GetKey(KeyCode.Mouse0))
		{
			painting = false;
		}
	}

	void DisplayPalette()
	{
		
		float height =  Screen.height/ 10f;
		float width = height;
		float x = 11f + width/2f;
		float y = GetComponent<RectTransform>().sizeDelta.y / 2f;
		float offset = width + width/10;
		int i = 1;
		foreach(VoxelColor c in ItemController.I.allModels[ItemController.I.selectedItem]._colors)
		{
			Vector3 pos = new Vector3(x,y,0f);
			
			GameObject colorNumber = Instantiate(colorNumberPrefab, pos, transform.rotation);

			colorNumber.GetComponent<Button>().onClick.AddListener(() => ChangeColor(c.color, colorNumber));
			colorNumber.GetComponent<Button>().GetComponentInChildren<Text>().text = i.ToString();
			if(c.color.grayscale < 0.5f)
				colorNumber.GetComponent<Button>().GetComponentInChildren<Text>().color = Color.white;
			Color cellColor = new Color(c.color.r, c.color.g, c.color.b, 1f);
			colorNumber.GetComponent<Button>().GetComponent<Image>().color = cellColor;
			Debug.Log("color" + cellColor);
			colorNumber.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
			colorNumber.transform.SetParent(GetComponent<RectTransform>());
			colorNumber.transform.localPosition = pos;	
			x += offset;
			i++;
		}
	}

	void ChangeColor(Color i, GameObject b)
	{
		ReturnToNormalSize();
		b.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		nibColor =  i;
	}

	void ReturnToNormalSize()
	{
		Button [] clrs = GetComponentsInChildren<Button>();
		foreach(Button clr in clrs)
		{
			clr.transform.localScale = Vector3.one;
		}
	}
}
