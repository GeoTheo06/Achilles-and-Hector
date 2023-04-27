using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class achileas : MonoBehaviour
{
	public int hp = 100;
	GameObject ektoras;
	ektoras ektorasScript;

	public Animator achilAnimator;
	public Animation attackAnim;

	GameObject achilleasDamage;
	TextMeshProUGUI achilleasDamageText;

	GameObject achilleasHp;
	TextMeshProUGUI achilleasHpText;

	GameObject game;
	game gameScript;

	void Start()
	{
		game = GameObject.Find("game");
		gameScript = game.GetComponent<game>();

		achilleasDamage = GameObject.Find("axilleas damage");
		achilleasDamageText = achilleasDamage.GetComponent<TextMeshProUGUI>();

		achilleasHp = GameObject.Find("axilleas hp");
		achilleasHpText = achilleasHp.GetComponent<TextMeshProUGUI>();

		ektoras = GameObject.Find("ektoras");
		ektorasScript = ektoras.GetComponent<ektoras>();

		achilAnimator = GetComponent<Animator>();
		attackAnim = GetComponent<Animation>();
	}

	public void attack()
	{
		if (achilAnimator.GetBool("defence"))
			achilAnimator.SetBool("defence", false);

		achilAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		ektorasScript.takeDamage(Random.Range(10, 26));
	}

	int healPoints;
	public void defence()
	{
		achilAnimator.SetBool("defence", true);
		gameScript.aPlayedDefenceBefore = true;

		healPoints = Random.Range(5, 16);
		if (healPoints + hp > 150)
			healPoints = 150 - hp;

		achilleasDamageText.text = "+" + healPoints;

		StartCoroutine(defenceTimer());
	}

	IEnumerator defenceTimer()
	{
		yield return new WaitForSeconds(1);
		hp += healPoints;
		achilleasDamageText.text = "";
		achilleasHpText.text = "" + hp;
		gameScript.changePlayerTurn();
	}

	public void heal()
	{

		gameScript.changePlayerTurn();
	}


	public void takeDamage(int damage)
	{
		if (achilAnimator.GetBool("defence"))
			StartCoroutine(takeDamageTimer());
		else
		{
			hp -= damage;
			achilleasDamageText.text = "-" + damage;
			StartCoroutine(removeText());
		}
	}

	IEnumerator takeDamageTimer()
	{
		yield return new WaitForSeconds(2);
		ektorasScript.ektorAnimator.SetBool("attack", false);
		StartCoroutine(takeDamageTimer2());
	}
	IEnumerator takeDamageTimer2()
	{
		yield return new WaitForSeconds(2);
		achilAnimator.SetBool("defence", false);
		gameScript.changePlayerTurn();
	}

	IEnumerator removeText()
	{
		yield return new WaitForSeconds(2);
		if (!ektorasScript.attackAnim.isPlaying && ektorasScript.ektorAnimator.GetBool("attack"))
		{
			achilleasDamageText.text = "";
			achilleasHpText.text = "" + hp;
			ektorasScript.ektorAnimator.SetBool("attack", false);
		}
		gameScript.changePlayerTurn();
	}
}
