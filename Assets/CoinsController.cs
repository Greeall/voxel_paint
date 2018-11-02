using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour {

	public GameObject adverisingOffer;
	private int _coins = 4;

	public int coins
	{
		get
		{
			return _coins;
		}
		set
		{
			_coins = value;
			SavingCoins();
		}
	}
	public Text coinsText;
	public static CoinsController I;

	public int quantityCoinsForAdvertising = 2;

	// Use this for initialization
	void Start () {
		I = this;
		//PlayerPrefs.SetInt("coins", 0);
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
		CloseAdvertisingOffer();
		
	}

	void SavingCoins()
	{
		PlayerPrefs.SetInt("coins", coins);
	}

	public void OpenAdvertisingOffer()
	{
		adverisingOffer.SetActive(true);
	}

	public void CloseAdvertisingOffer()
	{
		adverisingOffer.SetActive(false);
	}

}
