using UnityEngine;
using System.Collections;

public class ProvesBarril : MonoBehaviour {
	
	int vida = 4;
	public GameObject Barril;
	public GameObject DestroyBarril;
	
	// Use this for initialization
	void Start () {
		DestroyBarril.SetActive(false);
		//Barril.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void rebreTir(){
		vida -= 1;
		if(vida <= 0) {
			Barril.SetActive(false);
			DestroyBarril.SetActive(true);
		}
		
	}
	
	
}
