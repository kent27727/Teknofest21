using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backMenuScript : MonoBehaviour
{
	public Button backMenuButton;
	public Button quitButton;
	public Canvas quitCanvas;

	public void BackMenu()
	{
		
		SceneManager.LoadScene("oyunMenuSu");
		//SceneManager.UnloadSceneAsync("Gezegenler", UnloadSceneOptions.None);
	}

	public void QuitApp()
	{
		Debug.Log("Quit");
		Application.Quit();
		quitCanvas.enabled = false;
		
		

		
	}
}
