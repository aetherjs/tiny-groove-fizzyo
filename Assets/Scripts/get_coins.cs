using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class get_coins : MonoBehaviour {

	void Start(){
		GetComponent<UnityEngine.UI.Text>().text = "Total coins: " + Convert.ToString(PlayerPrefs.GetInt("TotalCoins", 0));
	}
}
