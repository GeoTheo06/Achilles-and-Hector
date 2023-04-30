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

	GameObject achileasGO;
	achileas achileasScript;

	GameObject ektorasDamageGO;
	TextMeshProUGUI ektorasDamageText;

	GameObject ektorasHpGO;
	TextMeshProUGUI ektorasHpText;

	GameObject healsRemainingGO;
	TextMeshProUGUI healsRemainingText;

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

		healsRemainingGO = GameObject.Find("eHealsRemaining");
		healsRemainingText = healsRemainingGO.GetComponent<TextMeshProUGUI>();

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
		if (ektorAnimator.GetBool("defence"))
			StartCoroutine(takeDamageTimer());
		else
		{
			if (hp - damage <= 0)
			{
				damage = hp;
				gameScript.gameOver = true;
			}

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
		if (gameScript.gameOver)

			StartCoroutine(gameOver());
		else
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
