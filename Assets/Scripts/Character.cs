using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

	public void goto_scene(string scene)
	{
		SceneManager.LoadScene (scene);
	}

}
