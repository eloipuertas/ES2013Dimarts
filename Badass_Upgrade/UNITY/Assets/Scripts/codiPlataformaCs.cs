using UnityEngine;
using System.Collections;

public class codiPlataformaCs : MonoBehaviour {

	bool direccio=false; //true -> add position  / false -> remove position
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
	
	string debugBoton = ""; //Only for debug outputs
	string debugDireccion = ""; //Only for debug outputs
	
	void Start(){
		PosicioInicialX = (this.transform.localPosition.x);  
		PosicioInicialY = (this.transform.localPosition.y); 
		PosicioInicialZ = (this.transform.localPosition.z); 
		/*
		Debug.Log(PosicioInicialX);
		Debug.Log(PosicioInicialY);
		Debug.Log(PosicioInicialZ);
		*/
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
					
				} 
				else {
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
				
				if(eix_X){
					if (x<PosicioFinalX){
						direccio = true; //para añadir siempre 
						if(!paraPlataforma){
							paraPlataforma=true;
							StartCoroutine(paraDelay());
						}
					}
					if (x>PosicioInicialX){
						direccio = false; //para reducir siempre 
						if(!paraPlataforma){
							paraPlataforma=true;
							StartCoroutine(paraDelay());
						}
						
					}
				}
				if(eix_Y){	
					if (y < PosicioFinalY){
						//Debug.Log("ENTRA Y < P");
						direccio = true; //para añadir siempre y subir
						if(!paraPlataforma){
							paraPlataforma=true;
							StartCoroutine(paraDelay());
							//Debug.Log("SALE Y < P");
						}
					}
					if (y > PosicioInicialY){
						//Debug.Log("ENTRA Y > P");
						direccio = false; //para reducir siempre y bajar
						if(!paraPlataforma){
							paraPlataforma=true;
							StartCoroutine(paraDelay());
							//Debug.Log("SALE Y > P");
						}
					}
				}
				if(eix_Z){
					if (z<PosicioFinalZ){
						direccio = true; //para añadir siempre 
						if(!paraPlataforma){
							paraPlataforma=true;
							StartCoroutine(paraDelay());
						}
					}
					if (z>PosicioInicialZ){
						direccio = false; //para reducir siempre y bajar
						if(!paraPlataforma){
							paraPlataforma=true;
							StartCoroutine(paraDelay());
						}
					}
				}
				
				/* DEBUG MODE
				if(botoActivat)
					debugBoton = "BOTON ON";
				else
					debugBoton = "BOTON OFF";
			
				if(direccio)
					debugDireccion = "DIR ON";
				else
					debugDireccion = "DIR OFF";
			
				if(paraPlataforma)
					Debug.Log("POS INICIAL Y"+PosicioInicialY+" X -> "+x+"Y -> "+y+"Z -> "+z+" PLAT: OFF |" + debugBoton + "DIRECC "+debugDireccion);
				else
					Debug.Log("POS INICIAL Y"+PosicioInicialY+" X -> "+x+"Y -> "+y+"Z -> "+z+" PLAT: ON |" + debugBoton + "DIRECC "+debugDireccion );
				*/
				
			}
			else{
				this.transform.Translate(0, 0, 0);
			}
			
			
			
		}
	}
		
	IEnumerator paraDelay(){
		if(paraPlataforma){
			yield return new WaitForSeconds(2);
			if(paraPlataforma)
				paraPlataforma=false;
			//Debug.Log("ACTIVAR DE NUEVO LA PLATAFORMA");
		}
		else{
			yield return new WaitForSeconds(0); 
			//Debug.Log("ACTIVAR DE NUEVO LA PLATAFORMA HACK");
		}
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
	
	/* useless code now
	void OnTriggerStay(Collider other){
		
            if(other.gameObject == player){
				Debug.Log("Estic sota la Plataforma");
				//player.SendMessage();
                        
            }
        }
	*/
	
	void setBotoActivat(){
		//Debug.Log("he activat boto");
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
   		yield return new WaitForSeconds(2);
		this.transform.position += Vector3.up * -0.5f; //TODO: Its only works for UP/DOWN platforms
		this.botoActivat = true;
	}
	
	 void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){ 
        	other.gameObject.SendMessage("playerOnPlataforma", true);
        }
    }

}
