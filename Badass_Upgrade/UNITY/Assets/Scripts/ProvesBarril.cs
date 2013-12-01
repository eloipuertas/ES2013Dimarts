using UnityEngine;
using System.Collections;

public class ProvesBarril : MonoBehaviour {
	
	const int MULTIPLICADOR = 20;
	const float DISTANCIA_MAXIMA = 15;
	
	int vida;
	
	float distance;
	int dany;
	public GameObject Barril;
	public GameObject Destroy;
	public GameObject Fire;
	public GameObject Player;
	
	// Use this for initialization
	void Start () {
		Destroy.SetActive(false);
		Fire.SetActive(false);
		//Barril.SetActive(false);
		
		vida = 3;
		distance = 0;
		dany = 0;
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
			distance = Vector3.Distance (Barril.transform.position, Player.transform.position);
			if(distance <= DISTANCIA_MAXIMA){
				dany = (int)(DISTANCIA_MAXIMA/distance)*MULTIPLICADOR;
				Player.SendMessage("rebreAtac",dany);
			}
			Destroy.SetActive(true);
			
		}
		
	}
	
	
}
