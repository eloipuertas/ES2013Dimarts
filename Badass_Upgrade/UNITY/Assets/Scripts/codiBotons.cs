using UnityEngine;
using System.Collections;


public class codiBotons : MonoBehaviour {
	
	public GameObject Plataforma;
	public GameObject Porta;
	public GameObject Buto;
	public GameObject Palanca;
		
	bool activat=false;
	
	void Start () {
		
	}
	

	void Update () {
	
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
			/*if (!activat){
				activat=true;
			}else{
				activat=false;
			
			}*/
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
