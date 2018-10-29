using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class ScoreController : MonoBehaviour {

	public Texture2D coinIconTexture;
	public Font PixelFont;
	public GameObject prefab;

	public int coins = 0;
	private int lostCoins = 10;
	private int highScore;
	private int totalCoins;
	private GUIStyle style;

	void LoadPlayerPrefs() {
		highScore = PlayerPrefs.GetInt("HighScore1", 0);
	}


	void DisplayCoinsCount() {
    	Rect coinIconRect = new Rect(10, 10, 32, 32);
    	GUI.DrawTexture(coinIconRect, coinIconTexture);                         
    	GUIStyle style = new GUIStyle();
    	style.fontSize = 30;
    	style.fontStyle = FontStyle.Bold;
		style.font = PixelFont;
    	style.normal.textColor = Color.yellow;
    	Rect labelRect = new Rect(coinIconRect.xMax, coinIconRect.y, 60, 32);
    	GUI.Label(labelRect, coins.ToString(), style);
	}	

	void HitTheBush(Collider2D collider) {
		if (coins > lostCoins) {
			coins = coins - lostCoins;
			DropSomeCoins(10);
		}
		else {
			DropSomeCoins(coins);
			coins = 0;
		}
		collider.enabled = false;
		lostCoins = lostCoins + 5;
	}

	void DropSomeCoins(int count) {
		for (int i = 0; i < count; ++i) {
			GameObject clone;
			float rdm = Random.Range(-1.0f, 1.0f);
			Vector3 direction = new Vector3 (Mathf.Sin(i*(2*Mathf.PI/count)) + rdm, Mathf.Cos(i*(2*Mathf.PI/count)) + rdm, 0);
	    	clone = Instantiate(prefab, this.transform.position + direction * 0.4f, Quaternion.identity);
			clone.GetComponent<Rigidbody2D>().velocity = direction * Random.Range(2, 4);
			Destroy(clone, 3);
        }
	}

	void CollectCoin(Collider2D coinCollider)
	{
    	coins++;
		Destroy(coinCollider.gameObject);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.CompareTag("Coin"))
			CollectCoin(collider);
		else
			HitTheBush(collider);
	}

	void OnGUI() {
		DisplayCoinsCount();
	}

	void OnDestroy() {
		PlayerPrefs.SetInt("LastRun", coins);
		PlayerPrefs.Save();
	}
}
