using UnityEngine;
using System.Collections;

public class SimpleCamera : MonoBehaviour {

    [SerializeField] private float      movementSpeed = 0.5f;
	
	// Update is called once per frame
	void Update () {
        float inputX = Input.GetAxis("Horizontal");

        if(inputX > Mathf.Epsilon)
            transform.position += new Vector3(1.0f, 0.0f, 0.0f) * movementSpeed * Time.deltaTime;

        else if (inputX < -Mathf.Epsilon)
            transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * movementSpeed * Time.deltaTime;
    }
}
