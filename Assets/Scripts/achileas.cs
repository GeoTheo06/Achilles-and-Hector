using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achileas : MonoBehaviour
{
	public int hp = 100;
	GameObject ektoras;
	ektoras ektorasScript;

	public Animator achilAnimator;
	public Animation epitheshAnim;

	void Start()
	{
		ektoras = GameObject.Find("ektoras");
		ektorasScript = ektoras.GetComponent<ektoras>();
		achilAnimator = GetComponent<Animator>();
		epitheshAnim = GetComponent<Animation>();
	}

	public void epithesh()
	{
		achilAnimator.SetBool("epithesh", true);
		ektorasScript.takeDamage(Random.Range(5, 26));
	}

	public void amyna()
	{
		achilAnimator.SetBool("amyna", true);
	}

	public void heal()
	{

	}
}
