using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class game : MonoBehaviour
{

	GameObject seiraPaikthGameObject;
	TextMeshProUGUI seiraPaikthText;
	int number = 0;
	public int seiraPaikth;
	GameObject bla;
	Button aAttackButton, aDefenceButton, aHealButton, eAttackButton, eDefenceButton, eHealButton;

	void Start()
	{
		aAttackButton = GameObject.Find("AAttack").GetComponent<Button>();
		aDefenceButton = GameObject.Find("ADefence").GetComponent<Button>();
		aHealButton = GameObject.Find("AHeal").GetComponent<Button>();
		eAttackButton = GameObject.Find("EAttack").GetComponent<Button>();
		eDefenceButton = GameObject.Find("EDefence").GetComponent<Button>();
		eHealButton = GameObject.Find("ΕHeal1").GetComponent<Button>();


		seiraPaikthGameObject = GameObject.Find("seiraPaikth");
		seiraPaikthText = seiraPaikthGameObject.GetComponent<TextMeshProUGUI>();
		number = Random.Range(0, 2);
		seiraPaikth = number;
		if (number == 0)
			seiraPaikthText.text = "Παίζει ο Αχιλλέας";
		else if (number == 1)
			seiraPaikthText.text = "Παίζει ο Έκτορας";

		if (seiraPaikth == 1)
		{
			aAttackButton.interactable = false;
			aDefenceButton.interactable = false;
			aHealButton.interactable = false;
		}
		else if (seiraPaikth == 0)
		{
			eAttackButton.interactable = false;
			eDefenceButton.interactable = false;
			eHealButton.interactable = false;
		}

	}

	void Update()
	{

	}
}
