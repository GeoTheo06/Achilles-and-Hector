using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ektoras : MonoBehaviour
{
	public int hp = 100;

	GameObject game;
	game gameScript;

	GameObject achileas;
	achileas achileasScript;

	GameObject ektorasDamage;
	TextMeshProUGUI ektorasDamageText;

	GameObject ektorasHp;
	TextMeshProUGUI ektorasHpText;

	public Animator ektorAnimator;
	public Animation attackAnim;

	void Start()
	{
		game = GameObject.Find("game");
		gameScript = game.GetComponent<game>();

		ektorasDamage = GameObject.Find("ektoras damage");
		ektorasDamageText = ektorasDamage.GetComponent<TextMeshProUGUI>();

		ektorasHp = GameObject.Find("ektoras hp");
		ektorasHpText = ektorasHp.GetComponent<TextMeshProUGUI>();

		achileas = GameObject.Find("achilleas");
		achileasScript = achileas.GetComponent<achileas>();

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
			ektorAnimator.SetBool("defence", false);

		ektorAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		achileasScript.takeDamage(Random.Range(5, 26));
	}

	public void defence()
	{
		ektorAnimator.SetBool("defence", true);
		gameScript.changePlayerTurn();
	}

	public void heal()
	{
		gameScript.changePlayerTurn();
	}

	public void takeDamage(int damage)
	{
		if (ektorAnimator.GetBool("defence"))
			StartCoroutine(defenceTimer());
		else
		{
			hp -= damage;
			ektorasDamageText.text = "-" + damage;
			StartCoroutine(removeText());
		}
	}

	IEnumerator defenceTimer()
	{
		yield return new WaitForSeconds(2);
		achileasScript.achilAnimator.SetBool("attack", false);
		StartCoroutine(defenceTimer2());
	}
	IEnumerator defenceTimer2()
	{
		yield return new WaitForSeconds(2);
		ektorAnimator.SetBool("defence", false);
		gameScript.changePlayerTurn();
	}

	IEnumerator removeText()
	{
		yield return new WaitForSeconds(2);
		if (!achileasScript.attackAnim.isPlaying && achileasScript.achilAnimator.GetBool("attack"))
		{
			ektorasDamageText.text = "";
			ektorasHpText.text = "" + hp;
			achileasScript.achilAnimator.SetBool("attack", false);
		}
		gameScript.changePlayerTurn();
	}
}
