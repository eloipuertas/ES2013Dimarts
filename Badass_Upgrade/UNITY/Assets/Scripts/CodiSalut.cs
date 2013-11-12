using UnityEngine;
using System.Collections;

public class CodiSalut : MonoBehaviour {
	
	public GameObject CreuSalut;
	public GameObject Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Player.SendMessage("addItemVida",35);
        Destroy(gameObject);

	}
}
