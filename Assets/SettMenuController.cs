using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettMenuController : MonoBehaviour {

	public GameObject SetMenu;
	public GameObject Learning;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenSetMenu()
	{
		SetMenu.SetActive(true);
	}

	public void CloseSetMenu()
	{
		SetMenu.SetActive(false);
	}

	public void OpenLearning()
	{
		Learning.SetActive(true);
	}

	public void CloseLearning()
	{
		Learning.SetActive(false);
	}
}
