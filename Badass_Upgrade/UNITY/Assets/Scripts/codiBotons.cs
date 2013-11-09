using UnityEngine;
using System.Collections;

public class codiBotons : MonoBehaviour {
	
	public GameObject Plataforma;
	public GameObject Porta;
	
	void Start () {
	
	}
	

	void Update () {
	
	}
	
	
	void activarBoto(){
		Debug.Log("btooooooo");
		if(Plataforma){
			Plataforma.SendMessage("setBotoActivat");
		}
		if(Porta){
			Porta.SendMessage("setNivel_Completado",true);
		}
	}
		
}
