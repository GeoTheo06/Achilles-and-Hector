using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ektoras : MonoBehaviour
{
	public int hp = 100;

	GameObject game;
	game gameScript;

	GameObject achileasGO;
	achileas achileasScript;

	GameObject ektorasDamageGO;
	TextMeshProUGUI ektorasDamageText;

	GameObject ektorasHpGO;
	TextMeshProUGUI ektorasHpText;

	public Animator ektorAnimator;
	public Animation attackAnim;

	void Start()
	{
		game = GameObject.Find("game");
		gameScript = game.GetComponent<game>();

		ektorasDamageGO = GameObject.Find("ektoras damage");
		ektorasDamageText = ektorasDamageGO.GetComponent<TextMeshProUGUI>();

		ektorasHpGO = GameObject.Find("ektoras hp");
		ektorasHpText = ektorasHpGO.GetComponent<TextMeshProUGUI>();

		achileasGO = GameObject.Find("achilleas");
		achileasScript = achileasGO.GetComponent<achileas>();

		ektorAnimator = GetComponent<Animator>();
		attackAnim = GetComponent<Animation>();
	}

	void Update()
	{

	}

	int damage;
	public void attack()
	{
		if (ektorAnimator.GetBool("defence"))
		{
			ektorAnimator.SetBool("defence", false);
			StartCoroutine(attackTimer());
		}
		else
		{
			ektorAnimator.SetBool("attack", true);
			gameScript.makeNonInteractable();
			achileasScript.takeDamage(Random.Range(10, 26));
		}
	}

	IEnumerator attackTimer()
	{
		yield return new WaitForSeconds(1);

		ektorAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		achileasScript.takeDamage(Random.Range(10, 26));
	}

	int defenceHealPoints;
	public void defence()
	{
		ektorAnimator.SetBool("defence", true);
		gameScript.ePlayedDefenceBefore = true;

		defenceHealPoints = Random.Range(5, 11);
		if (defenceHealPoints + hp > 150)
			defenceHealPoints = 150 - hp;

		ektorasDamageText.text = "+" + defenceHealPoints;

		StartCoroutine(defenceTimer());
	}

	IEnumerator defenceTimer()
	{
		yield return new WaitForSeconds(1);
		hp += defenceHealPoints;
		ektorasDamageText.text = "";
		ektorasHpText.text = "" + hp;
		gameScript.changePlayerTurn();
	}

	public void heal()
	{

		gameScript.changePlayerTurn();
	}

	public void takeDamage(int damage)
	{
		if (ektorAnimator.GetBool("defence"))
			StartCoroutine(takeDamageTimer());
		else
		{
			hp -= damage;
			ektorasDamageText.text = "-" + damage;
			StartCoroutine(removeText1());
		}
	}

	IEnumerator removeText1()
	{
		yield return new WaitForSeconds(1);
		if (!achileasScript.attackAnim.isPlaying && achileasScript.achilAnimator.GetBool("attack"))
			achileasScript.achilAnimator.SetBool("attack", false);
		StartCoroutine(removeText2());
	}
	IEnumerator removeText2()
	{
		yield return new WaitForSeconds(1);
		ektorasDamageText.text = "";
		ektorasHpText.text = "" + hp;
		achileasScript.achilAnimator.SetBool("attack", false);
		gameScript.changePlayerTurn();
	}

	IEnumerator takeDamageTimer()
	{
		yield return new WaitForSeconds(1);
		achileasScript.achilAnimator.SetBool("attack", false);
		StartCoroutine(takeDamageTimer2());
	}
	IEnumerator takeDamageTimer2()
	{
		yield return new WaitForSeconds(1);
		ektorAnimator.SetBool("defence", false);
		gameScript.changePlayerTurn();
	}
}
