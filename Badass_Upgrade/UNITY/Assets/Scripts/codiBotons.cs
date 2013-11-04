using UnityEngine;
using System.Collections;

public class codiBotons : MonoBehaviour {
	
	public GameObject Plataforma;

	
	void Start () {
	
	}
	

	void Update () {
	
	}
	
	
	void activarBoto(){
		Debug.Log("btooooooo");
		Plataforma.SendMessage("setBotoActivat");
	}
		
}
