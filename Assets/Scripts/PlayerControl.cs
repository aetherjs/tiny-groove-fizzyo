using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class PlayerControl : MonoBehaviour {

	public Rigidbody2D RB;
	public float Thrust;
	public float forwardMovementSpeed = 3.0f;
	public GameObject bird;

	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody2D>();
		bird = GameObject.Find ("Character");
		// Load skin settings from player prefs and set up character object accordingly
		if (PlayerPrefs.GetString ("skin") != null) {
			if (PlayerPrefs.GetString ("skin") == "") {
				bird.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load ("birdd", typeof(Sprite));	
			} else {
				bird.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.Load (PlayerPrefs.GetString ("skin"), typeof(Sprite));
				bird.GetComponent<SpriteRenderer> ().flipX = true;
				if (PlayerPrefs.GetString ("skin") == "bird1") {
					bird.transform.localScale = new Vector3 (0.1f, 0.1f, 0);
					bird.GetComponent<CapsuleCollider2D> ().size = new Vector2 (2, 2);
				} else if (PlayerPrefs.GetString ("skin") == "bird2") {
					bird.transform.localScale = new Vector3 (0.5f, 0.5f, 0);
					bird.GetComponent<CapsuleCollider2D> ().size = new Vector2 (2, 2);
				} else {
					bird.transform.localScale = new Vector3 (0.3f, 0.3f, 0);
					bird.GetComponent<CapsuleCollider2D> ().size = new Vector2 (2, 2);
				}
			}
		} 


	}

	// Update is called once per frame
	void Update () {
		
		// If button is pressed on a Fizzyo device - give Character a little push upwards
		bool birdActive = FizzyoFramework.Instance.Device.ButtonDown();

		// Added force is randomised to make dodging abstacles a little bit more challenging
		float mistake = Random.Range(-4f, 8f);
		if (birdActive) {
                RB.AddForce(new Vector2(0, Thrust + mistake));
            }

		// Movement forward control
		Vector2 newVelocity = RB.velocity;
		newVelocity.x = forwardMovementSpeed;
		RB.velocity = newVelocity;
	}
}
