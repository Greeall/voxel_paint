using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class AdminPanel : MonoBehaviour {


    public Image _selectedImg;
	Texture2D img;
	
	Datas levelTxt;

	
	public Text forTxt;
	public Text attention;



	void Awake()
	{
		
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SaveLevelsToPrefs();
		if(Input.GetKeyDown("a"))
		{
			Debug.Log(ItemController.I.models.Count);
		}
		
	}

	

	public void AddImgLevel()
	{
		img = null;
		string path = EditorUtility.OpenFilePanel("Overwrite with txt", "", "png");
        if (path.Length != 0)
        {
			byte[] fileContent = File.ReadAllBytes(path);
			img = new Texture2D(122,122);
			img.LoadImage(fileContent);
			Sprite imgSprite = Sprite.Create (img, new Rect (0,0, img.width, img.height), new Vector2 (0,0));
			_selectedImg.sprite = imgSprite;			
		}

     }

	
	

	public void AddLevelTxt()
	{
		Text _text = Selection.activeObject as Text;

		string path = EditorUtility.OpenFilePanel("Overwrite with txt", "", "txt");
        if (path.Length != 0)
        {
			string content = File.ReadAllText(path);
			levelTxt = JsonUtility.FromJson<Datas>(content);
			forTxt.text = path;
		}

		 
	}

	public void SaveModel()
	{
		if(levelTxt.isExist()  && img == true)
		{
			ItemController.I.models.Add(levelTxt);
			int count = ItemController.I.models.Count;
			byte[] bytes = img.EncodeToPNG();
        	File.WriteAllBytes(Application.dataPath + "/../Assets/Resources/img" + (count-1) + ".png", bytes);
			img = null;

		}
		else
		{
			attention.text = "text/image is missing";
		}
		
	}

	void SaveLevelsToPrefs()
	{
		PlayerPrefs.SetInt("levelsCount", ItemController.I.models.Count);
		for(int i = 0; i < ItemController.I.models.Count; i++)
		{
			PlayerPrefs.SetString("level" + i, JsonUtility.ToJson(ItemController.I.models[i]));
		}
	}
}
