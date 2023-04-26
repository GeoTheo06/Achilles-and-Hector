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
	public Animation attackhAnim;

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
		attackhAnim = GetComponent<Animation>();
	}

	void Update()
	{

	}

	int damage;
	public void attack()
	{
		damage = Random.Range(5, 25);

		achileasScript.hp -= damage;
		gameScript.changePlayerTurn();
	}

	public void defence()
	{
		ektorAnimator.SetBool("amyna", true);
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
