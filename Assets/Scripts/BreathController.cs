using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fizzyo;

public class BreathController : MonoBehaviour {

	public GameObject bird;
	GeneratorScript targetScript;
	public int breathCount = 1;
	public int maxBreaths;
	public int setCount = 1;
	public int maxSets;
	bool breathActive = false;

	void LoadPlayerPrefs() {
		maxBreaths = PlayerPrefs.GetInt("MaxBreaths");
		maxSets = PlayerPrefs.GetInt("Sets");
	}

	// Use this for initialization
	void Start () {
		LoadPlayerPrefs();
		//FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        //FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
		targetScript = bird.GetComponent<GeneratorScript>();
	}
	
	// Stops the generator script from generating objects for provided time to allow for rest between the sets
	void giveBreak(int time) {
		float timer = 0;
		targetScript.SetGenerateEmpty(true);
		Invoke("stopBreak", time);
	}

	void stopBreak() {
		targetScript.SetGenerateEmpty(false);
	}

/* 	void OnBreathStarted(object sender) {

    }

	void OnBreathEnded(object sender, ExhalationCompleteEventArgs e) {
		Debug.Log("Breath ended");
		if (breathCount < maxBreaths) {
			breathCount++;
		} else {
			setCount++;
			breathCount = 0;
			giveBreak();
		}
    }   */

	void OnBreathEnd() {
		Debug.Log("Breath ended");
		if (breathCount < maxBreaths) {
			breathCount++;
		} else {
			setCount++;
			breathCount = 0;
			giveBreak(5);
		}
	}

	// Update is called once per frame
	void Update () {
		if (FizzyoFramework.Instance.Device.Pressure() > 0.1f) {
			Debug.Log("Pffffff out");
			breathActive = true;
		}

		if (FizzyoFramework.Instance.Device.Pressure() < 0.1f && breathActive) {
			Debug.Log("Breath ended");
			OnBreathEnd();
			breathActive = false;
		}

		if (setCount == maxSets && breathCount == maxBreaths) {
			Initiate.Fade("Game Over Scene", Color.black, 1);
		}
	}
}
