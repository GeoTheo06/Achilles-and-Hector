using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class menu : MonoBehaviour
{
	AudioSource music;
	TextMeshProUGUI info;

	private void Start()
	{
		info = GameObject.Find("information").GetComponent<TextMeshProUGUI>();
		info.enabled = false;

		music = GameObject.Find("oneMusic").GetComponent<AudioSource>();

		music.Play();
		music.volume = 65;
	}
	public void ChangeScene()
	{
		PlayerPrefs.SetFloat("musicTime", music.time);
		SceneManager.LoadScene("main");
	}

	bool toggle = true;
	public void setInfo()
	{
		if (toggle)
			info.enabled = true;
		else
			info.enabled = false;
		toggle = !toggle;
	}

	public void quit()
	{
		Application.Quit();
	}
}
