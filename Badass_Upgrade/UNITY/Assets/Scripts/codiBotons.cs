using UnityEngine;
using System.Collections;

public class codiBotons : MonoBehaviour {
	
	public GameObject Plataforma;
	public GameObject Porta;
	public GameObject Buto_Palanca;
	bool activat=false;
	
	void Start () {
		
	}
	

	void Update () {
	
	}
	
	
	// Aixo inverteix la animacio de activar per tal de fer que la palanca pugui baixar i quedi desactivat, el
	// que fa es de la animacio principar invertirla i la guardo en una nova que es diu "Desactivar"
	
	void invertirAnimacio(){
		Debug.Log("INvertitttttt");
		Buto_Palanca.animation["Desactivar"].speed = -1;
    	Buto_Palanca.animation["Desactivar"].time = Buto_Palanca.animation["Desactivar"].length;

	}
	
	
	void activarBoto(){
		Debug.Log("btooooooo");
		
		if (!activat){
			Buto_Palanca.animation.CrossFade("Activar");
			activat=true;
		}else{
			invertirAnimacio();
			Buto_Palanca.animation.CrossFade("Desactivar");
			invertirAnimacio();
			activat=false;
		
		}
		
		if(Plataforma){
			Plataforma.SendMessage("setBotoActivat");
		}
		if(Porta){
			Porta.SendMessage("setNivel_Completado",true);
		}
	}
		
}
