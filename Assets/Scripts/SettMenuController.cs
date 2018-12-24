using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettMenuController : MonoBehaviour {

	public GameObject SetMenu;
	public GameObject Learning;

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
