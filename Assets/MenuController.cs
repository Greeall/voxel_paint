using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject content;

	public Text testo;

	public Image menu;
	// Use this for initialization
	void Start () {
		
//		Debug.Log(menu.GetComponent<RectTransform>().offsetMin);

		menu.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width / 20f, Screen.height / 5f);
		menu.GetComponent<RectTransform>().offsetMax = new Vector2( - Screen.width / 20f, - Screen.height / 10f );
		DisplayItems();

	}
	



	void DisplayItems()
	{
		float x = 0;
		float itemWidth = (Screen.width - (Screen.width/20f * 2))/3f;
		float itemPadding = itemWidth / 3f;
		float y = - itemPadding;


		//int halfOfItems = (ItemController.I.models.Count%2 == 0) ? ItemController.I.models.Count / 2 : ItemController.I.models.Count / 2 + 1;
		float contentHeight = 5 * (itemWidth + itemPadding);

		//contentHeight = (contentHeight < Screen.height/ 5f) ? Screen.height / 5 : contentHeight;
		
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( content.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(itemPadding, -(contentHeight + itemPadding));
		testo.text = PlayerPrefs.GetInt("levelsCount", -3).ToString();
		for(int i = 0; i < ItemController.I.allModels.Count; i++)
		{

			GameObject a = Instantiate(itemPrefab, new Vector3(x,y,0), transform.rotation) as GameObject;
			a.transform.SetParent(content.transform);
			a.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth,itemWidth);
			int number = i;
			a.GetComponent<Button>().onClick.AddListener(() => OpenLevel(number));
			a.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
			
			a.GetComponentInChildren<Text>().text = CountUpVoxels(i).ToString();
			//Sprite img = GetSpriteFromResources(i, "img");
			//a.GetComponent<Image>().sprite = img;

			x += itemWidth + itemPadding;
			if(i%2 == 1)
			{
				y -= Screen.width/3 + Screen.width/3/5f;
				x = 0;
			}
		}
	}

	int CountUpVoxels(int i)
	{
		int quantity = 0;
		foreach(Layer l in ItemController.I.allModels[i]._model)
			quantity += l.layer.Count;
		return quantity;
	}


	void DisplayHomeItems()
	{
		float x = 0;
		float itemWidth = (Screen.width - (Screen.width/20f * 2))/3f;
		float itemPadding = itemWidth / 3f;
		float y = - itemPadding;


		//int halfOfItems = (ItemController.I.models.Count%2 == 0) ? ItemController.I.models.Count / 2 : ItemController.I.models.Count / 2 + 1;
		float contentHeight = 5 * (itemWidth + itemPadding);

		//contentHeight = (contentHeight < Screen.height/ 5f) ? Screen.height / 5 : contentHeight;
		
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( content.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(itemPadding, -(contentHeight + itemPadding));
		testo.text = PlayerPrefs.GetInt("homeLevelsCount", -1).ToString();
		for(int i = 0; i < ItemController.I.homeModels.Count; i++)
		{

			GameObject a = Instantiate(itemPrefab, new Vector3(x,y,0), transform.rotation) as GameObject;
			a.transform.SetParent(content.transform);
			a.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth,itemWidth);
			int number = ItemController.I.homeModels[i];
			a.GetComponent<Button>().onClick.AddListener(() => OpenLevel(number));
			a.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
			
			Sprite img = GetSpriteFromResources(ItemController.I.homeModels[i], "homeImg");
			a.GetComponent<Image>().sprite = img;

			x += itemWidth + itemPadding;
			if(i%2 == 1)
			{
				y -= Screen.width/3 + Screen.width/3/5f;
				x = 0;
			}
		}
	}
	public void OpenLevel(int y)
	{
		ItemController.I.selectedItem = y;
		//Debug.Log(ItemController.I.selectedItem);
		ItemController.I.allModels[y]._isBeginning = true;
		ItemController.I.AddLevelToHomeMenu();
		PlayerPrefs.SetInt("SelectedItem", y);
		Application.LoadLevel("VoxelPaint");
	}

	public void OpenHomeItems()
	{
		DestroyItems();
		DisplayHomeItems();
	}

	public void OpenAllItems()
	{
		DestroyItems();
		DisplayItems();
	}

	void DestroyItems()
	{
		Transform[] items = content.GetComponentsInChildren<Transform>();
		foreach(Transform item in items)
		{
			if(item.gameObject.GetInstanceID() != content.GetInstanceID())
				Destroy(item.gameObject);
		}
	}

	 Sprite GetSpriteFromResources(int i, string imgPath)
	 {
	 	Sprite img;
		string path = imgPath + i;
 		Texture2D tex = null;
		tex = Resources.Load<Texture2D>(path);
		img = Sprite.Create(tex, new Rect(0,0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); 
		return img;
	}

	
}
