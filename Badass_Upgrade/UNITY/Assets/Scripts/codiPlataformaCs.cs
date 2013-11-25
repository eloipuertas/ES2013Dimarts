using UnityEngine;
using System.Collections;

public class codiPlataformaCs : MonoBehaviour {
	
	//public GameObject Plataforma;
	//public GameObject Player;
	public GameObject player;
	bool direccio=false;
	public bool botoActivat=false;
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
		
		float translation = Time.deltaTime * 2;
        
		if (botoActivat){
			if (direccio){
				if(eix_X){
					this.transform.Translate(translation, 0, 0);
					//this.transform.position += Vector3.right * 0.05f;
				}
				if(eix_Y){
					this.transform.Translate(0, translation, 0);
					//this.transform.position += Vector3.up * 0.05f;
				
				}
				if(eix_Z){
					this.transform.Translate(0, 0, translation);
					//this.transform.position += Vector3.back * 0.05f;
				}
				
			}else{
				if(eix_X){
					this.transform.Translate(-translation, 0, 0);
					
					//this.transform.position += Vector3.right * -0.05f;
				}
				if(eix_Y){
					this.transform.Translate(0, -translation, 0);
					//this.transform.position += Vector3.up * -0.05f;
				
				}
				if(eix_Z){
					this.transform.Translate(0, 0, -translation);
					//this.transform.position += Vector3.back * -0.05f;
					
				}
			}
			
			cont++;
			if(distancia==cont){
				direccio = !this.direccio;
				cont=0;
			}
		}
		
		
	}
	/*
	void mourePlataforma(){
		float translation = Time.deltaTime * 10;
        this.transform.Translate(0, translation, 0);
		//transform.Translate(0, -translation, 0);
	}*/
	
	
	void OnTriggerStay(Collider other){
		
            if(other.gameObject == player){
				Debug.Log("Estic sota la Plataforma");
				//player.SendMessage();
                        
            }
        }
	
	void setBotoActivat(){
		Debug.Log("he activat boto");
		botoActivat = !this.botoActivat;
		//mourePlataforma();
	}
	
	void OnCollisionEnter(Collision Target){  
		/* TODO: Solo se tiene encuenta por ahora la primera plataforma, que tiene el RigidBody,
		 * el resto, no tienen RigidBody por lo que nunca se detectara la Collision, no pasa nada,
		 * ya que la unica que puedes aplastar es la primera.
		 * 
		 * */
		if(Target.gameObject.tag == "Player"){
			this.botoActivat = false;
			this.transform.position += Vector3.up * 0.5f; //TODO: Its only works for UP/DOWN platforms
			Target.gameObject.SendMessage("rebreAtac",50); //Quitarle vida al player
		}		
	}
	
	void OnCollisionExit(Collision Target){		
		if(Target.gameObject.tag == "Player"){
			StartCoroutine("DelayedPlatformMove");
		}
	}
	
	IEnumerator DelayedPlatformMove(){
   		yield return new WaitForSeconds(2);
		this.transform.position += Vector3.up * -0.5f; //TODO: Its only works for UP/DOWN platforms
		this.botoActivat = true;
	}

}
