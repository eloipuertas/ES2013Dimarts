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

	/*void Start (bool x,bool y,bool z,int dist) {
		eix_X = x;
		eix_Y = y;
		eix_Z = z;
		distancia = dist;

	}*/
	

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
   		yield return new WaitForSeconds(3);
		this.transform.position += Vector3.up * -0.5f; //TODO: Its only works for UP/DOWN platforms
		this.botoActivat = true;
	}

}
