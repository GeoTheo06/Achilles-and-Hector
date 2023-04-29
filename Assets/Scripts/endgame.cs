using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endgame : MonoBehaviour
{
	GameObject textOB;
	TextMeshProUGUI text;

	// Start is called before the first frame update
	void Start()
	{
		textOB = GameObject.Find("win");
		text = textOB.GetComponent<TextMeshProUGUI>();

		text.text = "Νίκησε ο " + PlayerPrefs.GetString("VariableName");
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
