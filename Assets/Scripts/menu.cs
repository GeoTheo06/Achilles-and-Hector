using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
	AudioSource music;
	GameObject info;

	private void Start()
	{
		info = GameObject.Find("information");
		info.SetActive(false);

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
			info.SetActive(true);
		else
			info.SetActive(false);
		toggle = !toggle;
	}

	public void quit()
	{
		Application.Quit();
	}
}
