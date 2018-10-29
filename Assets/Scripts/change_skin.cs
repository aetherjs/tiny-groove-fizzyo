using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_skin : MonoBehaviour {

	public void change_to(string a)
	{
		PlayerPrefs.SetString ("skin", a);
	}
}
