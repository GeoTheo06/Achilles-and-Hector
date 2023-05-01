using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endgame : MonoBehaviour
{
	TextMeshProUGUI text;

	// Start is called before the first frame update
	void Start()
	{
		text = GameObject.Find("win").GetComponent<TextMeshProUGUI>();

		text.text = "Nίκησε ο " + PlayerPrefs.GetString("winner");
	}

	public void exit()
	{
		Application.Quit();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
