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
	public bool playerTurn, gameOver = false;
	Button aAttackButton, aDefenceButton, aHealButton, eAttackButton, eDefenceButton, eHealButton;
	achileas achileasScript;
	ektoras ektorasScript;
	AudioSource music;

	GameObject aHealPSob, eHealPSob;
	ParticleSystem aHealPS, eHealPS;

	void Start()
	{
		music = GameObject.Find("oneMusic").GetComponent<AudioSource>();
		music.time = PlayerPrefs.GetFloat("musicTime");
		music.Play();
		music.volume = 65;

		achileasScript = GameObject.Find("achilleas").GetComponent<achileas>();
		ektorasScript = GameObject.Find("ektoras").GetComponent<ektoras>();

		aHealPSob = GameObject.Find("aHealPS");
		aHealPS = aHealPSob.GetComponent<ParticleSystem>();

		eHealPSob = GameObject.Find("eHealPS");
		eHealPS = GameObject.Find("eHealPS").GetComponent<ParticleSystem>();

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

		aHealPS.Stop();
		eHealPS.Stop();
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

	public bool aPlayedDefenceBefore = false, ePlayedDefenceBefore = false;
	public void changePlayerTurn()
	{
		if (playerTurn)
			playerTurnText.text = "Παίζει ο Αχιλλέας";
		else if (!playerTurn)
			playerTurnText.text = "Παίζει ο Έκτορας";

		if (playerTurn)
		{
			eAttackButton.interactable = false;
			eDefenceButton.interactable = false;
			eHealButton.interactable = false;

			aAttackButton.interactable = true;
			aHealButton.interactable = true;

			//i dont want the players to be able to just use shields all the time: after using it once, it is disabled for one round
			if (aPlayedDefenceBefore)
			{
				aDefenceButton.interactable = false;
				aPlayedDefenceBefore = false;
			}
			else
				aDefenceButton.interactable = true;
		}
		else if (!playerTurn)
		{
			aAttackButton.interactable = false;
			aDefenceButton.interactable = false;
			aHealButton.interactable = false;

			eAttackButton.interactable = true;
			eHealButton.interactable = true;

			if (ePlayedDefenceBefore)
			{
				eDefenceButton.interactable = false;
				ePlayedDefenceBefore = false;
			}
			else
				eDefenceButton.interactable = true;
		}

		if (achileasScript.healsUsed >= 2 || achileasScript.hp == 100)
			aHealButton.interactable = false;
		if (ektorasScript.healsUsed >= 2 || ektorasScript.hp == 100)
			eHealButton.interactable = false;

		playerTurn = !playerTurn;
	}

	float rotationSpeed = 200f;
	void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();

		aHealPSob.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
		eHealPSob.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

		if (Input.GetKey(KeyCode.Space))
		{
			achileasScript.hp = 1;
		}
	}


	public void fadeMusic()
	{
		StartCoroutine(DecreaseVolumeOverTime());
	}
	IEnumerator DecreaseVolumeOverTime()
	{
		float time = 2f;
		float initialVolume = music.volume; //store initial volume
		float elapsedTime = 0f;

		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			music.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / time); //gradually decrease volume
			yield return null;
		}
	}

	public void aHeal(bool start)
	{
		if (start)
			aHealPS.Play();
		else
			aHealPS.Stop();
	}

	public void eHeal(bool start)
	{
		if (start)
			eHealPS.Play();
		else
			eHealPS.Stop();
	}
}
