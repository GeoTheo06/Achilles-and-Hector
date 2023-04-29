using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
	public void ChangeScene()
	{
		SceneManager.LoadScene("main");
	}

	public void quit()
	{
		Application.Quit();
	}
}
