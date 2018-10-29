using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fizzyo;

public class Magnet : MonoBehaviour {

    public Transform player;
	
	// Update is called once per frame
	void Update () {

        float pressure = FizzyoFramework.Instance.Device.Pressure();
        float pullingForce = pressure * 30f / Vector3.Distance(transform.position, player.position);
        // Debug.Log(pressure);
        if (pullingForce <= 2.5) 
            pullingForce = 0;
        transform.position = Vector3.MoveTowards(transform.position, player.position, pullingForce * Time.deltaTime);
    }
}
