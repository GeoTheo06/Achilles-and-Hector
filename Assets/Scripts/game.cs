using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class game : MonoBehaviour
{

	GameObject playerTurnGameObject;
	TextMeshProUGUI playerTurnText;
	int number = 0;
	public bool playerTurn;
	Button aAttackButton, aDefenceButton, aHealButton, eAttackButton, eDefenceButton, eHealButton;

	void Start()
	{
		aAttackButton = GameObject.Find("AAttack").GetComponent<Button>();
		aDefenceButton = GameObject.Find("ADefence").GetComponent<Button>();
		aHealButton = GameObject.Find("AHeal").GetComponent<Button>();
		eAttackButton = GameObject.Find("EAttack").GetComponent<Button>();
		eDefenceButton = GameObject.Find("EDefence").GetComponent<Button>();
		eHealButton = GameObject.Find("EHeal").GetComponent<Button>();

		playerTurnGameObject = GameObject.Find("playerTurn");
		playerTurnText = playerTurnGameObject.GetComponent<TextMeshProUGUI>();
		number = Random.Range(0, 2);
		playerTurn = number == 1;

		if (playerTurn)
		{
			playerTurnText.text = "Παίζει ο Αχιλλέας";
		}
		else if (!playerTurn)
		{
			playerTurnText.text = "Παίζει ο Έκτορας";
		}
		changePlayerTurn();
	}

	public void makeNonInteractable()
	{
		aAttackButton.interactable = false;
		aDefenceButton.interactable = false;
		aHealButton.interactable = false;
		eAttackButton.interactable = false;
		eDefenceButton.interactable = false;
		eHealButton.interactable = false;
	}

	public void changePlayerTurn()
	{
		if (playerTurn)
		{
			eAttackButton.interactable = false;
			eDefenceButton.interactable = false;
			eHealButton.interactable = false;
			aAttackButton.interactable = true;
			aDefenceButton.interactable = true;
			aHealButton.interactable = true;
		}
		else if (!playerTurn)
		{
			aAttackButton.interactable = false;
			aDefenceButton.interactable = false;
			aHealButton.interactable = false;
			eAttackButton.interactable = true;
			eDefenceButton.interactable = true;
			eHealButton.interactable = true;
		}
		playerTurn = !playerTurn;
	}

	void Update()
	{

	}
}
