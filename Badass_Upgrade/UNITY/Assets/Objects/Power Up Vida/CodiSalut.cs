using UnityEngine;
using System.Collections;

public class CodiSalut : MonoBehaviour {
	
	public GameObject CreuSalut;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		////////   funcio incrementar vida!!!!!! //////
        Destroy(gameObject);

	}
}
