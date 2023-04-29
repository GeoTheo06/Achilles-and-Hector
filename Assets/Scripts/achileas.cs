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

	GameObject achilleasDamageGO;
	TextMeshProUGUI achilleasDamageText;

	GameObject achilleasHpGO;
	TextMeshProUGUI achilleasHpText;

	GameObject healsRemainingGO;
	TextMeshProUGUI healsRemainingText;

	GameObject gameGO;
	game gameScript;

	void Start()
	{
		gameGO = GameObject.Find("game");
		gameScript = gameGO.GetComponent<game>();

		achilleasDamageGO = GameObject.Find("axilleas damage");
		achilleasDamageText = achilleasDamageGO.GetComponent<TextMeshProUGUI>();

		achilleasHpGO = GameObject.Find("axilleas hp");
		achilleasHpText = achilleasHpGO.GetComponent<TextMeshProUGUI>();

		healsRemainingGO = GameObject.Find("axilleas hp");
		healsRemainingText = healsRemainingGO.GetComponent<TextMeshProUGUI>();

		ektoras = GameObject.Find("ektoras");
		ektorasScript = ektoras.GetComponent<ektoras>();

		achilAnimator = GetComponent<Animator>();
		attackAnim = GetComponent<Animation>();
	}

	public void attack()
	{
		if (achilAnimator.GetBool("defence"))
		{
			achilAnimator.SetBool("defence", false);
			StartCoroutine(attackTimer());
		}
		else
		{
			achilAnimator.SetBool("attack", true);
			gameScript.makeNonInteractable();
			ektorasScript.takeDamage(Random.Range(10, 26));
		}
	}

	IEnumerator attackTimer()
	{
		yield return new WaitForSeconds(1);

		achilAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		ektorasScript.takeDamage(Random.Range(10, 26));
	}

	int defenceHealPoints;
	public void defence()
	{
		achilAnimator.SetBool("defence", true);
		gameScript.aPlayedDefenceBefore = true;

		defenceHealPoints = Random.Range(5, 11);
		if (defenceHealPoints + hp > 150)
			defenceHealPoints = 150 - hp;

		achilleasDamageText.text = "+" + defenceHealPoints;

		StartCoroutine(defenceTimer());
	}

	IEnumerator defenceTimer()
	{
		yield return new WaitForSeconds(1);
		hp += defenceHealPoints;
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
		{
			StartCoroutine(takeDamageTimer());
		}
		else
		{
			hp -= damage;
			achilleasDamageText.text = "-" + damage;
			StartCoroutine(removeText1());
		}
	}
	IEnumerator removeText1()
	{
		yield return new WaitForSeconds(1);
		if (!ektorasScript.attackAnim.isPlaying && ektorasScript.ektorAnimator.GetBool("attack"))
			ektorasScript.ektorAnimator.SetBool("attack", false);
		StartCoroutine(removeText2());
	}
	IEnumerator removeText2()
	{
		yield return new WaitForSeconds(1);
		achilleasDamageText.text = "";
		achilleasHpText.text = "" + hp;
		ektorasScript.ektorAnimator.SetBool("attack", false);
		gameScript.changePlayerTurn();
	}

	IEnumerator takeDamageTimer()
	{
		yield return new WaitForSeconds(1);
		ektorasScript.ektorAnimator.SetBool("attack", false);
		StartCoroutine(takeDamageTimer2());
	}
	IEnumerator takeDamageTimer2()
	{
		yield return new WaitForSeconds(1);
		achilAnimator.SetBool("defence", false);
		gameScript.changePlayerTurn();
	}
}
