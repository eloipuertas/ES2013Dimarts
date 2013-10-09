using UnityEngine;
using System.Collections;

public class atackMelee : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		//animation.Stop();
		animation.Play("ArmatureAction");
	}
	
	// Update is called once per frame
	void Update () {
				
		if(Input.GetAxis("atackMelee") > 0) {
			animation.Play("ArmatureAction0");	
		}
		
	
	}
}

/*if (Input.GetKeyDown ("q")) {
                 animation .Play();
		}*/ 