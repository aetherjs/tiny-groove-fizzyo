using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour {

	public Text highScoreText;
	public Text scoreText;
	public Text totalText;
	public int highScore;
	public int currentScore;
	public int total;

	void LoadPlayerPrefs() {
		highScore = PlayerPrefs.GetInt("HighScore1");
		currentScore = PlayerPrefs.GetInt("LastRun");
		total = PlayerPrefs.GetInt("TotalCoins");
	}

	// Use this for initialization
	void Start () {
		LoadPlayerPrefs();
		if (currentScore > highScore) 
		{
			highScore = currentScore;
			PlayerPrefs.SetInt ("HighScore1", highScore);
		}
		scoreText.text =  currentScore.ToString();
		highScoreText.text = highScore.ToString ();
	}

	public void Restart ()	{
		PlayerPrefs.SetInt("TotalCoins", total + currentScore);
		PlayerPrefs.Save();
		Initiate.Fade("Game Scene", Color.black, 1);
	}

	public void backToMain() {
		PlayerPrefs.SetInt("TotalCoins", total + currentScore);
		PlayerPrefs.Save();
		Initiate.Fade("Main Menu", Color.black, 1);
	}
}