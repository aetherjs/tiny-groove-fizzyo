using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject Bird; 

	public void PauseGame() {
		Time.timeScale = 0f;
	}

	public void ResumeGame() {
		Time.timeScale = 1f;
	}

	public void BackToMain() {
		Time.timeScale = 1f;
		ScoreController targetScript = Bird.GetComponent<ScoreController>();
		targetScript.coins = 0;
		Initiate.Fade("Main Menu", Color.black, 1);
	}
}
