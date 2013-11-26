using UnityEngine;
using System.Collections;

public class DeteccioProximitat : MonoBehaviour {
	
	public GameObject player;
	public GameObject CubOnVaCodi;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		
            if(other.gameObject == player){
				Debug.Log("Estic davan el boto");
				CubOnVaCodi.SendMessage("proximAlBoto");
                        
            }
   }
	
	
	void OnTriggerExit(Collider other) {
        Debug.Log("sortir de la deteccio");
		CubOnVaCodi.SendMessage("sortirDeLaDeteccio");
    }
}
