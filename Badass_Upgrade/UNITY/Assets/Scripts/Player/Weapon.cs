using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	
	public int damage;
	public GameObject modelWeapon;
	
	//Mida inicial
	/*float scalex;
	float scaley;
	float scalez;*/
	
	public void init(int damage,string tagName) {
		
		this.damage = damage;
		this.modelWeapon = GameObject.FindGameObjectWithTag(tagName);
		
		/*scalex = modelWeapon.transform.localScale.x;
		scaley = modelWeapon.transform.localScale.y;
		scalez = modelWeapon.transform.localScale.z;*/
	}
	
	public int getDamage() {
		return damage;
	}
	
	public void hideWeapon() {

    	//modelWeapon.transform.localScale -= new Vector3(scalex,scaley,scalez);	
		modelWeapon.SetActive(false);
	}
	
	public void showWeapon() {

    	//modelWeapon.transform.localScale += new Vector3(scalex,scaley,scalez);	
		modelWeapon.SetActive(true);
	}
	
	// Use this for initialization
	void Start () {	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
