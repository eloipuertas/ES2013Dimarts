using UnityEngine;
using System.Collections;


public class codiBotons : MonoBehaviour {
	
	public GameObject Plataforma;
	public GameObject Porta;
	public GameObject Buto;
	public GameObject Palanca;
	public GameObject Pulsador;
	public GameObject Player;
	public GameObject DeteccioProximitat;
//	public AudioClip buttonSound;
	
	
	Color colorRed = Color.red;
    Color colorGreen = Color.green;
	Color colorYellow = Color.yellow;
	float duration = 1.0F;
	
	bool saClicat=false;
	bool activat=false;
	
	void Start () {
		if(Buto){
			Pulsador.renderer.material.color = colorRed; 
			
		}
	}
	

	void Update () {
			
	}
	/*
	void OnTriggerStay(Collider other){
		Debug.Log("Estic al trigger");
		if(other.gameObject == Player){
				Debug.Log("Estic davant el boto");
				float lerp = Mathf.PingPong(Time.time, duration) / duration;
        		Pulsador.renderer.material.color = Color.Lerp(colorRed, colorGreen, lerp);
                        
            }
	}*/
	
	void proximAlBoto(){
		if (!saClicat){
			float lerp = Mathf.PingPong(Time.time, duration) / duration;
		    Pulsador.renderer.material.color = Color.Lerp(colorRed, colorGreen, lerp);
			//Pulsador.renderer.material.color = colorYellow;
		}
	}
	
	void sortirDeLaDeteccio(){
		Debug.Log("sortitttttt");
		saClicat=false;
		if (activat){
			Pulsador.renderer.material.color = colorGreen;
		}else{
			Pulsador.renderer.material.color = colorRed;	
		} 
	}
	
	
	
	
	
	// Aixo inverteix la animacio de activar per tal de fer que la palanca pugui baixar i quedi desactivat, el
	// que fa es de la animacio principar invertirla i la guardo en una nova que es diu "Desactivar"
	
	void invertirAnimacio(){
		Debug.Log("INvertitttttt");
		Palanca.animation["Desactivar"].speed = -1;
    	Palanca.animation["Desactivar"].time = Palanca.animation["Desactivar"].length;
		

	}
	
	
	IEnumerator activarBoto(){
		Debug.Log("btooooooo");
		
//		AudioSource.PlayClipAtPoint(buttonSound,transform.position,0.9F);
		if (Palanca){
			if (!activat){
				Palanca.animation.CrossFade("Activar");
				activat=true;
			}else{
				invertirAnimacio();
				Palanca.animation.CrossFade("Desactivar");
				invertirAnimacio();
				activat=false;
			
			}
		}
		if (Buto){
			Buto.animation.CrossFade("Activar");
			saClicat=true;
			 
			if (!activat){
				activat=true;
				Pulsador.renderer.material.color = colorGreen;
			}else{
				activat=false;
				Pulsador.renderer.material.color = colorRed;
			
			}
		}
		
		yield return new WaitForSeconds(2);
		if(Plataforma){
			Plataforma.SendMessage("setBotoActivat");
		}
		if(Porta){
			Porta.SendMessage("setNivel_Completado",true);
		}
	}
	

		
}
