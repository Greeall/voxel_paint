using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour {

	public int coins = 4;
	public Text coinsText;
	public static CoinsController I;

	public int quantityCoinsForAdvertising = 2;

	// Use this for initialization
	void Start () {
		I = this;
		coins = PlayerPrefs.GetInt("coins", 2);
	}
	
	// Update is called once per frame
	void Update () {
		coinsText.text = coins.ToString();
	}

	public void AdverisingOffer()
	{
		
	}
	public void AddingCoins()
	{
		//Here is watching advertising
		Debug.Log("watching adverising...");
		coins += quantityCoinsForAdvertising;
		SavingCoins();
	}

	void SavingCoins()
	{
		PlayerPrefs.SetInt("coins", coins);
	}


}
