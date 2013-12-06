using UnityEngine;
using System.Collections;

public class boss_area_fire : MonoBehaviour {
	private int dmg = 8;
	private float timerAtac = 0.0f;
	private int fireRate=1;
	// Use this for initialization
	void Start () {
		timerAtac=Time.time;
		//Destroy(this.gameObject,40.0f);
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay(Collider other) {
		if (Time.time > timerAtac) {
			if(other.gameObject.tag == "Boss" || other.gameObject.tag == "Player"){
	        	other.gameObject.SendMessage ("rebreAtac", dmg);
				timerAtac=Time.time+fireRate;
			}
		}
    }
}
