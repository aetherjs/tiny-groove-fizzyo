using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buy_skin : MonoBehaviour {

	void Start(){
		if (PlayerPrefs.GetString (this.name) != "")
			Destroy (this.gameObject);
	}

	public void buyskin(){
		if (PlayerPrefs.GetInt ("TotalCoins") > Convert.ToInt32 (this.name))
		{
			PlayerPrefs.SetInt ("TotalCoins", PlayerPrefs.GetInt ("TotalCoins") - Convert.ToInt32 (this.name));
			PlayerPrefs.SetString (this.name, "1");
			SceneManager.LoadScene ("CharacterShop");
		}
	}
}
