using UnityEngine;
using System.Collections;

public class CodiMunicio : MonoBehaviour {
	
	public GameObject Municio;
	public GameObject Cosa;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	void OnTriggerEnter(Collider other) {
		////////   funcio incrementar municio!!!!!! //////
        Destroy(gameObject);

	}
    

}
