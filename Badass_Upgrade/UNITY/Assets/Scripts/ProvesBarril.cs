using UnityEngine;
using System.Collections;

public class ProvesBarril : MonoBehaviour {
	
	int vida = 3;
	public GameObject Barril;
	public GameObject Destroy;
	public GameObject Fire;
	
	// Use this for initialization
	void Start () {
		Destroy.SetActive(false);
		Fire.SetActive(false);
		//Barril.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void rebreTir(){
		
		
		vida -= 1;
		if (vida<=2){
			Fire.SetActive(true);
		}
		if(vida <= 0) {
			Barril.SetActive(false);
			Destroy.SetActive(true);
			
		}
		
	}
	
	
}
