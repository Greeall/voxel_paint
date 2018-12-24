using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour {

	public Sprite img;


	public GameObject itemPrefab;
	public GameObject content;

	public GameObject advertising;


	public Sprite ready;
	public Sprite inProcess;

	public Sprite crown;

	public Image menu;

	public GameObject offerAboutClosing;

	void Start () {
		Invoke("DisplayItems", 0.01f);
	}

	void Update()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
				offerAboutClosing.SetActive(true);
		}
	}

	public void CloseApp()
	{
		Application.Quit();
	}

	public void CloseOfferAboutClosing()
	{
		offerAboutClosing.SetActive(false);
	}


	void DisplayItems()
	{
		float screenWidth = content.GetComponent<RectTransform>().rect.width;
		if(content.GetComponent<RectTransform>().rect.width > 0)
			PlayerPrefs.SetFloat("contentWidth", content.GetComponent<RectTransform>().rect.width);
		else
			screenWidth = PlayerPrefs.GetFloat("contentWidth", content.GetComponent<RectTransform>().rect.width);
		
		float itemWidth = screenWidth / 3f;
		float realItemWidth = Screen.width * 0.9f / 3f;
		float itemPadding = itemWidth / 3f;
		float y = -itemPadding;
		float x = itemPadding;

		
		float halfOfItems = (ItemController.I.allModels.Count%2 == 0) ? itemWidth/2.7f : itemWidth/2.7f + itemWidth + itemPadding;
		float contentHeight = (ItemController.I.allModels.Count / 2) * (itemWidth + itemPadding) + halfOfItems;
		
	
		if(ItemController.I.allModels.Count < 6) contentHeight = 3 * (itemWidth + itemPadding);
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( 0f, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(0f, -(contentHeight + itemPadding));

		for(int i = 0; i < ItemController.I.allModels.Count; i++)
		{
			int number = i;
			GameObject level = Instantiate(itemPrefab, new Vector3(0,0,0), transform.rotation) as GameObject;
			SetMiniIcons(level, number);
			

			level.transform.SetParent(content.transform);		
			level.GetComponent<Button>().onClick.AddListener(() => TryOpenLevel(number));
			level.GetComponentInChildren<Text>().text = CountUpVoxels(i).ToString();
			level.GetComponent<RectTransform>().sizeDelta = new Vector2(realItemWidth, realItemWidth);
			
			if(i%2 == 0)
			{
				x = itemPadding;
				if(i != 0) y -= itemPadding + itemWidth;
			}
			else
			{
				x = itemPadding*2 + itemWidth;
			}
			level.GetComponent<RectTransform>().localPosition = new Vector2(x, y);			
			
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
		float realItemWidth = Screen.width * 0.9f / 3f;
		float screenWidth = content.GetComponent<RectTransform>().rect.width;
		float itemWidth = screenWidth / 3f;
		float itemPadding = itemWidth / 3f;
		float y = -itemPadding;
		float x = itemPadding;

		
		float halfOfItems = (ItemController.I.homeModels.Count%2 == 0) ? itemWidth/2.7f : itemWidth/2.7f + itemWidth + itemPadding;
		float contentHeight = (ItemController.I.homeModels.Count / 2) * (itemWidth + itemPadding) + halfOfItems;
		
	
		if(ItemController.I.allModels.Count < 6) contentHeight = 3 * (itemWidth + itemPadding);
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( 0f, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(0f, -(contentHeight + itemPadding));
		
		for(int i = 0; i < ItemController.I.homeModels.Count; i++)
		{
			GameObject a = Instantiate(itemPrefab, new Vector3(x,y,0), transform.rotation) as GameObject;
			a.transform.SetParent(content.transform);
			//a.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth,itemWidth);
			int number = ItemController.I.homeModels[i];

			Text[] imgs = a.GetComponentsInChildren<Text>();
			imgs[1].text = Process(number);

			a.GetComponent<Button>().onClick.AddListener(() => OpenLevel(number));
			a.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
			a.GetComponent<RectTransform>().sizeDelta = new Vector2(realItemWidth, realItemWidth);
			
			//Sprite img1 = GetSpriteFromResources(ItemController.I.homeModels[i], "homeImg");
			a.GetComponent<Image>().sprite = img;

			if(i%2 == 0)
			{
				x = itemPadding;
				if(i != 0) y -= itemPadding + itemWidth;
			}
			else
			{
				x = itemPadding*2 + itemWidth;
			}
			a.GetComponent<RectTransform>().localPosition = new Vector2(x, y);			
		}
	}
	public void TryOpenLevel(int y)
	{
		Datas item = ItemController.I.allModels[y];
		if(item._isVip && !item._isBeginning && !item._isFinished)
		{
			if(CoinsController.I.coins - 2 >= 0)
			{
				CoinsController.I.coins -= 2;
				OpenLevel(y);
			}
			else
				advertising.SetActive(true);
		}
		else
		{
			OpenLevel(y);
		}
	}

	void OpenLevel(int y)
	{
		ItemController.I.selectedItem = y;
		ItemController.I.allModels[y]._isBeginning = true;
		ItemController.I.AddLevelToHomeMenu();
		ES2.Save(y, "SelectedItem");
		ItemController.I.SaveAllModelsToPref1();
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
		byte[] imgByte = File.ReadAllBytes(Application.persistentDataPath + "/" + imgPath + i + ".jpg");
		tex.LoadImage(imgByte);
		img = Sprite.Create(tex, new Rect(0,0, tex.width, tex.height), new Vector2(0.5f, 0.5f)); 
		return img;
	}

	void SetMiniIcons(GameObject level, int i)
	{
		if(ItemController.I.allModels[i]._isFinished)
		{
			Image[] imgs = level.GetComponentsInChildren<Image>();
			imgs[1].sprite = ready;
		}
		else if(ItemController.I.allModels[i]._isBeginning)
		{
			Image[] imgs = level.GetComponentsInChildren<Image>();
			imgs[1].sprite = inProcess;
		}
		if(ItemController.I.allModels[i]._isVip)
		{
			Image[] imgs = level.GetComponentsInChildren<Image>();
			imgs[2].sprite = crown;
		}
	}

	string Process(int i)
	{
		int count = 0;
		int all = CountUpVoxels(i);
		foreach(Layer l in ItemController.I.allModels[i]._model)
		{
			if(l.isDrawing)
				count += l.layer.Count;
			else
			{
				foreach(VoxelPlatform v in l.layer)
					if(v.isDrawing)
						count +=1;
				break;
			}
		}

		int procent = count * 100 / all;
		
		return procent + "%";
	}
	
}
