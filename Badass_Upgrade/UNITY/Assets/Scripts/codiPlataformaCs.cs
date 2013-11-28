using UnityEngine;
using System.Collections;

public class codiPlataformaCs : MonoBehaviour {
	
	//public GameObject Plataforma;
	//public GameObject Player;
	public GameObject player;
	bool direccio=false;
	bool plataformaQuieta = false;
	public bool botoActivat=false;
	
	public bool eix_X;
	public bool eix_Y;
	public bool eix_Z;
	public float PosicioFinalX;
	public float PosicioFinalY;
	public float PosicioFinalZ;
	
	float PosicioInicialX; 
	float PosicioInicialY;
	float PosicioInicialZ;
	bool paraPlataforma=false;

	//public int distancia = 250;
	//int cont = 0;
	
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
	
	void Start(){
		PosicioInicialX = (this.transform.localPosition.x);  
		PosicioInicialY = (this.transform.localPosition.y); 
		PosicioInicialZ = (this.transform.localPosition.z); 
		/*
		PosicioInicialX = this.transform.position.x; 
		PosicioInicialY = this.transform.position.y;
		PosicioInicialZ = this.transform.position.z;
		*/
		Debug.Log(PosicioInicialX);
		Debug.Log(PosicioInicialY);
		Debug.Log(PosicioInicialZ);
		
	}
	
	void Update(){
		moviment();
	}
	
	void moviment() {
		float translation = Time.deltaTime * 2;
        
		if (botoActivat){
			if(!paraPlataforma){
				if (direccio){
					if(eix_X){
						this.transform.Translate(translation, 0, 0);
						
					}
					if(eix_Y){
						this.transform.Translate(0, translation, 0);
					
					}
					if(eix_Z){
						this.transform.Translate(0, 0, translation);
					}
					
				}else{
					if(eix_X){
						this.transform.Translate(-translation, 0, 0);
						
					}
					if(eix_Y){
						this.transform.Translate(0, -translation, 0);
					
					}
					if(eix_Z){
						this.transform.Translate(0, 0, -translation);
						
					}
				}
				float x = this.transform.localPosition.x; 
				float y = this.transform.localPosition.y; 
				float z = (this.transform.localPosition.z); 
				/*
				float x = this.transform.position.x; 
				float y = this.transform.position.y; 
				float z = this.transform.position.z;
				*/ 
				Debug.Log(x);
				Debug.Log(y);
				Debug.Log(z);
				
				if(eix_X){
					if (x<PosicioFinalX){
						paraPlataforma=true;
						StartCoroutine(paraDelay());
						direccio = !this.direccio;
					}
					if (x>PosicioInicialX){
						paraPlataforma=true;
						StartCoroutine(paraDelay());
						direccio = !this.direccio;
					}
				}
				if(eix_Y){	
					if (y<PosicioFinalY){
						paraPlataforma=true;
						StartCoroutine(paraDelay());
						direccio = !this.direccio;
					}
					if (y>PosicioInicialY){
						paraPlataforma=true;
						StartCoroutine(paraDelay());
						direccio = !this.direccio;
					}
				}
				if(eix_Z){
					if (z<PosicioFinalZ){
						paraPlataforma=true;
						StartCoroutine(paraDelay());
						direccio = !this.direccio;
					}
					if (z>PosicioInicialZ){
						paraPlataforma=true;
						StartCoroutine(paraDelay());
						direccio = !this.direccio;
					}
				}
					
			}else{
				this.transform.Translate(0, 0, 0);
			}
				
			
			
		}
		
		
		
	}
		
	IEnumerator paraDelay(){
		yield return new WaitForSeconds(2);
		paraPlataforma=false;
		Debug.Log("despres");
		
	}
		
		
	/*
	IEnumerator canviarDireccio(){
		Debug.Log("dintre");
		yield return new WaitForSeconds(2);
		//this.direccio = !this.direccio;
		Debug.Log("despres");
		direccio = !this.direccio;
		
	}*/
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
