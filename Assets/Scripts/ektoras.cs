using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ektoras : MonoBehaviour
{
	public int hp = 100;

	GameObject game;
	game gameScript;
	achileas achileasScript;
	TextMeshProUGUI ektorasHpText, ektorasDamageText, healsRemainingText;
	public Animator ektorAnimator;
	public Animation attackAnim;

	void Start()
	{
		achileasScript = GameObject.Find("achilleas").GetComponent<achileas>();
		game = GameObject.Find("game");
		gameScript = game.GetComponent<game>();
		ektorasDamageText = GameObject.Find("ektoras damage").GetComponent<TextMeshProUGUI>();
		ektorasHpText = GameObject.Find("ektoras hp").GetComponent<TextMeshProUGUI>();
		healsRemainingText = GameObject.Find("eHealsRemaining").GetComponent<TextMeshProUGUI>();
		ektorAnimator = GetComponent<Animator>();
		attackAnim = GetComponent<Animation>();
	}

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
			achileasScript.takeDamage(Random.Range(10, 31));
		}
	}

	IEnumerator attackTimer()
	{
		yield return new WaitForSeconds(0.3f);

		ektorAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		achileasScript.takeDamage(Random.Range(10, 31));
	}

	int defenceHealPoints;
	public void defence()
	{
		ektorAnimator.SetBool("defence", true);
		gameScript.ePlayedDefenceBefore = true;

		gameScript.playSwordHitSound();

		defenceHealPoints = Random.Range(5, 11);
		if (defenceHealPoints + hp > 100)
			defenceHealPoints = 100 - hp;

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

	public int healsUsed = 0;
	int healPoints, oldHp;
	public void heal()
	{
		healsUsed++;
		healsRemainingText.text = healsUsed + "/2";

		healPoints = Random.Range(10, 31);
		if (hp + healPoints > 100)
			healPoints = 100 - hp;

		ektorasDamageText.text = "+" + healPoints;

		gameScript.eHeal(true);
		gameScript.playHealSound();

		oldHp = hp;
		InvokeRepeating("incrementHp", 0.0f, 0.1f);
	}

	void incrementHp()
	{
		hp += 1;
		ektorasHpText.text = "" + hp;

		if (hp >= oldHp + healPoints)
		{
			CancelInvoke("incrementHp");
			ektorasDamageText.text = "";
			gameScript.eHeal(false);
			gameScript.changePlayerTurn();
		}
	}

	public void takeDamage(int damage)
	{
		if (hp - damage <= 0)
		{
			damage = hp;
			gameScript.gameOver = true;
		}

		hp -= damage;

		ektorasDamageText.text = "-" + damage;
		StartCoroutine(takeDamageTimer());
	}

	IEnumerator takeDamageTimer()
	{
		yield return new WaitForSeconds(1);
		achileasScript.achilAnimator.SetBool("attack", false);

		if (ektorAnimator.GetBool("defence"))
			StartCoroutine(takeDamageWithShield());
		else
			StartCoroutine(takeDamageWithoutShield());
	}
	IEnumerator takeDamageWithoutShield()
	{
		yield return new WaitForSeconds(1);

		ektorasDamageText.text = "";
		ektorasHpText.text = "" + hp;
		gameScript.playSwordHitSound();

		if (gameScript.gameOver)

			StartCoroutine(gameOver());
		else
			gameScript.changePlayerTurn();
	}

	IEnumerator takeDamageWithShield()
	{
		yield return new WaitForSeconds(1);

		ektorAnimator.SetBool("defence", false);
		gameScript.playSwordClashSound();

		if (gameScript.gameOver)

			StartCoroutine(gameOver());
		else
			gameScript.changePlayerTurn();
	}

	IEnumerator gameOver()
	{
		gameScript.fadeMusic();
		yield return new WaitForSeconds(2);
		string ektorasName = "Αχιλλέας";
		PlayerPrefs.SetString("winner", ektorasName);
		SceneManager.LoadScene("endgame");
	}
}
