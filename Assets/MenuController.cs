using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject content;
	public Scrollbar scrollPos;

	public Text test;

	public Image menu;
	// Use this for initialization
	void Start () {
		
		Debug.Log(menu.GetComponent<RectTransform>().offsetMin);

		menu.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width / 20f, Screen.height / 5f);
		menu.GetComponent<RectTransform>().offsetMax = new Vector2( - Screen.width / 20f, - Screen.height / 10f );
		DisplayItem();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DisplayItem()
	{
		float x = 0;
	
		float itemWidth = (Screen.width - (Screen.width/20f * 2))/3f;
		test.GetComponent<Text>().text = (Screen.width - (menu.GetComponent<RectTransform>().offsetMin.x * 2)).ToString();
		float itemPadding = itemWidth / 3f;
		float y = - itemPadding;

		//int halfOfItems = (ItemController.I.models.Count%2 == 0) ? ItemController.I.models.Count / 2 : ItemController.I.models.Count / 2 + 1;
		float contentHeight = 5 * (itemWidth + itemPadding);

		//contentHeight = (contentHeight < Screen.height/ 5f) ? Screen.height / 5 : contentHeight;
		
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( content.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(itemPadding, -(contentHeight + itemPadding));
		
		for(int i = 0; i < ItemController.I.models.Count; i++)
		{

			GameObject a = Instantiate(itemPrefab, new Vector3(x,y,0), transform.rotation) as GameObject;
			a.transform.SetParent(content.transform);
			a.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth,itemWidth);
			int number = i;
			a.GetComponent<Button>().onClick.AddListener(() => Open(number));
			a.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
			
			Sprite img = GetSpriteFromResources(i);
			a.GetComponent<Image>().sprite = img;

			x += itemWidth + itemPadding;
			if(i%2 == 1)
			{
				y -= Screen.width/3 + Screen.width/3/5f;
				x = 0;
			}
		}
	}

	public void Open(int y)
	{
		ItemController.I.selectedItem = y;
		Application.LoadLevel("VoxelPaint");
	}

	Sprite GetSpriteFromResources(int i)
	{
		Sprite img;
		string path = "img" + i;
 		Texture2D tex = null;
		tex = Resources.Load<Texture2D>(path);
		img = Sprite.Create(tex, new Rect(0,0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); 
		return img;
	}
}
