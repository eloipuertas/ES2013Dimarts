using UnityEngine;
using System.Collections;

public class codiPlataformaCs : MonoBehaviour {
	
	//public GameObject Plataforma;
	//public GameObject Player;
	bool direccio=false;
	bool botoActivat=false;
	public bool eix_X;
	public bool eix_Y;
	public bool eix_Z;
	public int distancia = 250;
	int cont = 0;
	
	bool itemBounceUp = false;

//<<<<<<< HEAD
	//void Start (bool x,bool y,bool z,int dist) {
//=======
	/*void Start (bool x,bool y,bool z,int dist) {
>>>>>>> origin/dev
		eix_X = x;
		eix_Y = y;
		eix_Z = z;
		distancia = dist;

<<<<<<< HEAD
	}
=======
	}*/
//>>>>>>> origin/dev
	

	void Update () {
		

		if (botoActivat){
			if (direccio){
				if(eix_X){
					
					this.transform.position += Vector3.right * 0.05f;
				}
				if(eix_Y){
					this.transform.position += Vector3.up * 0.05f;
				
				}
				if(eix_Z){
					this.transform.position += Vector3.back * 0.05f;
				}
				
			}else{
				if(eix_X){
					
					this.transform.position += Vector3.right * -0.05f;
				}
				if(eix_Y){
					this.transform.position += Vector3.up * -0.05f;
				
				}
				if(eix_Z){
					this.transform.position += Vector3.back * -0.05f;
					
				}
			}
			
			cont++;
			if(distancia==cont){
				direccio = !this.direccio;
				cont=0;
			}
		}
		
	}
	
	void setBotoActivat(){
		Debug.Log("he activat boto");
		botoActivat = !this.botoActivat;
	}
	
	
}
