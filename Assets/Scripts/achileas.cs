using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class achileas : MonoBehaviour
{
	public int hp = 100;

	public Animator achilAnimator;
	public Animation attackAnim;
	TextMeshProUGUI achilleasDamageText, achilleasHpText, healsRemainingText;
	game gameScript;
	ektoras ektorasScript;

	void Start()
	{
		gameScript = GameObject.Find("game").GetComponent<game>();
		achilleasDamageText = GameObject.Find("axilleas damage").GetComponent<TextMeshProUGUI>();
		achilleasHpText = GameObject.Find("axilleas hp").GetComponent<TextMeshProUGUI>();
		healsRemainingText = GameObject.Find("aHealsRemaining").GetComponent<TextMeshProUGUI>();
		ektorasScript = GameObject.Find("ektoras").GetComponent<ektoras>();
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
			ektorasScript.takeDamage(Random.Range(10, 31));
		}
	}

	IEnumerator attackTimer()
	{
		yield return new WaitForSeconds(0.3f);

		achilAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		ektorasScript.takeDamage(Random.Range(10, 31));
	}

	int defenceHealPoints;
	public void defence()
	{
		achilAnimator.SetBool("defence", true);
		gameScript.aPlayedDefenceBefore = true;

		//too poor to find different sound lol
		gameScript.playSwordHitSound();

		defenceHealPoints = Random.Range(5, 11);
		if (defenceHealPoints + hp > 100)
			defenceHealPoints = 100 - hp;

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

	public int healsUsed = 0;
	int healPoints, oldHp;
	public void heal()
	{
		healsUsed++;
		healsRemainingText.text = healsUsed + "/2";

		healPoints = Random.Range(10, 31);
		if (hp + healPoints > 100)
			healPoints = 100 - hp;
		achilleasDamageText.text = "+" + healPoints;

		gameScript.aHeal(true);
		gameScript.playHealSound();

		oldHp = hp;
		InvokeRepeating("incrementHp", 0.0f, 0.1f);
	}

	void incrementHp()
	{
		hp += 1;
		achilleasHpText.text = "" + hp;

		if (hp >= oldHp + healPoints)
		{
			CancelInvoke("incrementHp");
			achilleasDamageText.text = "";
			gameScript.aHeal(false);
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

		achilleasDamageText.text = "-" + damage;
		StartCoroutine(takeDamageTimer());
	}
	IEnumerator takeDamageTimer()
	{
		yield return new WaitForSeconds(1);
		ektorasScript.ektorAnimator.SetBool("attack", false);

		if (achilAnimator.GetBool("defence"))
			StartCoroutine(takeDamageWithShield());
		else
			StartCoroutine(takeDamageWithoutShield());
	}
	IEnumerator takeDamageWithoutShield()
	{
		yield return new WaitForSeconds(1);

		achilleasDamageText.text = "";
		achilleasHpText.text = "" + hp;
		gameScript.playSwordHitSound();

		if (gameScript.gameOver)

			StartCoroutine(gameOver());
		else
			gameScript.changePlayerTurn();
	}
	IEnumerator takeDamageWithShield()
	{
		yield return new WaitForSeconds(1);

		achilAnimator.SetBool("defence", false);
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
		string axilleasName = "Έκτορας";
		PlayerPrefs.SetString("winner", axilleasName);
		SceneManager.LoadScene("endgame");
	}
}
