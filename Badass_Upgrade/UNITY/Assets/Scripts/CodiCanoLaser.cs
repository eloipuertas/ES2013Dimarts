using UnityEngine;
using System.Collections;

public class CodiCanoLaser : MonoBehaviour {

	int vida = 3;
	public GameObject Cano;
	public GameObject Canodestruit;
	public GameObject Laser;
	
	// Use this for initialization
	void Start () {
		Canodestruit.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void rebreTir(){
		Debug.Log("Caaanooooo");
		
		vida -= 1;
		if(vida <= 0) {
			Cano.SetActive(false);
			Laser.SetActive(false);
			Canodestruit.SetActive(true);
			
		}
		
	}
	
	
}