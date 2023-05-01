using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
	AudioSource music;

	private void Start()
	{
		music = GameObject.Find("oneMusic").GetComponent<AudioSource>();

		music.Play();
		music.volume = 65;
	}
	public void ChangeScene()
	{
		PlayerPrefs.SetFloat("musicTime", music.time);
		SceneManager.LoadScene("main");
	}

	public void quit()
	{
		Application.Quit();
	}
}
