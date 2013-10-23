using UnityEngine;
using System.Collections;

public class barril : MonoBehaviour {
	int vida=4;
	public GameObject Barril;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void rebreTir (){
		vida=vida-1;
		if(vida==0){
			Barril.setActive(false);
			DestroyBarril.setActive(true);
		}
	}
}
