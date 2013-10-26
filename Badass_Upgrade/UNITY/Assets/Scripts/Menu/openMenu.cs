using UnityEngine;
using System.Collections;

public class openMenu : MonoBehaviour {
	
	//Constants
	const int menu_pause = 3;
	
	//Variables
	public bool showMenu = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Escape")) {
			Application.LoadLevel(menu_pause);
		}
	}
}
