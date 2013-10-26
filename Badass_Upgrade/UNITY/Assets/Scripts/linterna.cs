using UnityEngine;
using System.Collections;

public class linterna : MonoBehaviour {
	
	
	public bool activeLinterna;
	public GameObject Linterna;
	
	// Use this for initialization
	void Start () {
		activeLinterna = false;
		Linterna = GameObject.FindWithTag("linterna");
		Linterna.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Linterna")){
			if(activeLinterna){
				activeLinterna = false;
			}
			else{
				activeLinterna = true;
			}
			Linterna.SetActive(activeLinterna);
		}
	}
}
