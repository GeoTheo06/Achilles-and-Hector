using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achileas : MonoBehaviour
{
	public int hp = 100;
	GameObject ektoras;
	ektoras ektorasScript;

	public Animator achilAnimator;
	public Animation attackAnim;

	GameObject game;
	game gameScript;

	void Start()
	{
		game = GameObject.Find("game");
		gameScript = game.GetComponent<game>();

		ektoras = GameObject.Find("ektoras");
		ektorasScript = ektoras.GetComponent<ektoras>();

		achilAnimator = GetComponent<Animator>();
		attackAnim = GetComponent<Animation>();
	}

	public void attack()
	{
		achilAnimator.SetBool("attack", true);
		gameScript.makeNonInteractable();
		ektorasScript.takeDamage(Random.Range(5, 26));
	}

	public void defence()
	{
		achilAnimator.SetBool("defence", true);
		gameScript.changePlayerTurn();
	}

	public void heal()
	{

		gameScript.changePlayerTurn();
	}
}
