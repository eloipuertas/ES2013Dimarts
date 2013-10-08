using UnityEngine;
using System.Collections;

public class atackMelee : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//animation.Stop();
		//animation.PlayQueued("AmatureAction");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("q")) {
                 animation .Play();
		} 
	
	}
}
