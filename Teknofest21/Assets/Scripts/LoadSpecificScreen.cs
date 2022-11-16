using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSpecificScreen : MonoBehaviour
{
	public Button loadButton;
	public GameObject menuObject;
	
    public void LoadSceneGezegen()
	{
		SceneManager.LoadScene("Gezegenler", LoadSceneMode.Additive);
		loadButton.enabled = false;
		menuObject.active = false;
	}

	public void LoadSceneKıta()
	{
		SceneManager.LoadScene("DragNDrop", LoadSceneMode.Additive);
		menuObject.active = false;
	}

}
