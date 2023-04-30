using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

	GameObject musicOB;
	AudioSource music;

	private void Start()
	{
		musicOB = GameObject.Find("oneMusic");
		music = musicOB.GetComponent<AudioSource>();

		music.Play();
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
