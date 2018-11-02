using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuController : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject content;

	public GameObject advertising;


	public Sprite ready;
	public Sprite inProcess;

	public Sprite crown;

	public Text testo;

	public Image menu;
	// Use this for initialization
	void Start () {
		
//		Debug.Log(menu.GetComponent<RectTransform>().offsetMin);

		menu.GetComponent<RectTransform>().offsetMin = new Vector2(Screen.width / 20f, Screen.height / 5f);
		menu.GetComponent<RectTransform>().offsetMax = new Vector2( - Screen.width / 20f, - Screen.height / 10f );
		DisplayItems();

	}

	void Update()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
	}



	void DisplayItems()
	{
		float x = 0;
		float itemWidth = (Screen.width - (Screen.width/20f * 2))/3f;
		float itemPadding = itemWidth / 3f;
		float y = - itemPadding;

		float halfOfItems = (ItemController.I.allModels.Count%2 == 0) ? itemWidth/2.7f : itemWidth/2.7f + itemWidth + itemPadding;
		
		float contentHeight = (ItemController.I.allModels.Count / 2) * (itemWidth + itemPadding) + halfOfItems;
	

		
		
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( content.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(itemPadding, -(contentHeight + itemPadding));
		testo.text = PlayerPrefs.GetInt("levelsCount", -3).ToString();
		for(int i = 0; i < ItemController.I.allModels.Count; i++)
		{
			int number = i;
			GameObject level = Instantiate(itemPrefab, new Vector3(x,y,0), transform.rotation) as GameObject;
			SetMiniIcons(level, number);
			

			level.transform.SetParent(content.transform);
			level.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth,itemWidth);
		
			level.GetComponent<Button>().onClick.AddListener(() => TryOpenLevel(number));
			level.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
			
			level.GetComponentInChildren<Text>().text = CountUpVoxels(i).ToString();

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

		
		float halfOfItems = (ItemController.I.homeModels.Count%2 == 0) ? itemWidth/2.7f : itemWidth/2.7f + itemWidth + itemPadding;
		
		float contentHeight = (ItemController.I.homeModels.Count / 2) * (itemWidth + itemPadding) + halfOfItems;
		

		
		
		content.GetComponent<RectTransform>().sizeDelta = new Vector2( content.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
	
		content.GetComponent<RectTransform>().localPosition = new Vector2(itemPadding, -(contentHeight + itemPadding));
		testo.text = PlayerPrefs.GetInt("homeLevelsCount", -1).ToString();
		for(int i = 0; i < ItemController.I.homeModels.Count; i++)
		{
			Debug.Log("Home number - " + ItemController.I.homeModels[i] + "; i - " + i);
			GameObject a = Instantiate(itemPrefab, new Vector3(x,y,0), transform.rotation) as GameObject;

			

			a.transform.SetParent(content.transform);
			a.GetComponent<RectTransform>().sizeDelta = new Vector2(itemWidth,itemWidth);
			int number = ItemController.I.homeModels[i];

			Text[] imgs = a.GetComponentsInChildren<Text>();
			imgs[1].text = Process(number);

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
	public void TryOpenLevel(int y)
	{
		Datas item = ItemController.I.allModels[y];
//		Debug.Log("vip - " + item._isVip + " beg - " + item._isBeginning + " end - " + item._isFinished);
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
		PlayerPrefs.SetInt("SelectedItem", y);
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
		Debug.Log(ItemController.I.allModels[i]._isBeginning);
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
