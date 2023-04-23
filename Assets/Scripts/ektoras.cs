using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ektoras : MonoBehaviour
{
	public int hp = 100;

	achileas achileasScript;
	GameObject achileas;

	GameObject ektorasDamage;
	TextMeshProUGUI ektorasDamageText;

	GameObject ektorasHp;
	TextMeshProUGUI ektorasHpText;

	public Animator ektorAnimator;
	public Animation epitheshAnim;

	// Start is called before the first frame update
	void Start()
	{
		ektorasDamage = GameObject.Find("ektoras damage");
		ektorasDamageText = ektorasDamage.GetComponent<TextMeshProUGUI>();

		ektorasHp = GameObject.Find("ektoras hp");
		ektorasHpText = ektorasHp.GetComponent<TextMeshProUGUI>();

		achileas = GameObject.Find("achilleas");
		achileasScript = achileas.GetComponent<achileas>();

		ektorAnimator = GetComponent<Animator>();
		epitheshAnim = GetComponent<Animation>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	int damage;
	public void epithesh()
	{
		damage = Random.Range(5, 25);

		achileasScript.hp -= damage;
	}

	public void amyna()
	{
		ektorAnimator.SetBool("amyna", true);
	}

	public void heal()
	{

	}

	public void takeDamage(int damage)
	{
		if (ektorAnimator.GetBool("amyna"))
			StartCoroutine(amynaTimer());
		else
		{
			hp -= damage;
			ektorasDamageText.text = "-" + damage;
			StartCoroutine(removeText());
		}
	}

	IEnumerator amynaTimer()
	{
		yield return new WaitForSeconds(2);
		achileasScript.achilAnimator.SetBool("epithesh", false);
		StartCoroutine(amynaTimer2());
	}
	IEnumerator amynaTimer2()
	{
		yield return new WaitForSeconds(2);
		ektorAnimator.SetBool("amyna", false);
	}

	IEnumerator removeText()
	{
		yield return new WaitForSeconds(2);
		if (!achileasScript.epitheshAnim.isPlaying && achileasScript.achilAnimator.GetBool("epithesh"))
		{
			ektorasDamageText.text = "";
			ektorasHpText.text = "" + hp;
			achileasScript.achilAnimator.SetBool("epithesh", false);
		}
	}
}
