using UnityEngine;
using System.Collections;

public class muzzleFlash : MonoBehaviour {
	
	
	public GameObject muzzleFlashObj;
	// Use this for initialization
	void Start () {
		muzzleFlashObj = GameObject.FindGameObjectWithTag("muzzleFlash");
		muzzleFlashObj.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
		muzzleFlashObj.SetActive(true);
		muzzleFlashObj.SetActive(false);
	}
}
