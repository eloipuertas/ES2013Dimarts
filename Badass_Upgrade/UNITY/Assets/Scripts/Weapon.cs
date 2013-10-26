using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	
	public int damage;
	public GameObject modelWeapon;
	
	public int balesTotals;
	public int balesActualCarregador;
	public int tamanyCarregador;
	
	public void init(int damage,string tagName, int initbales, int initbalesActuals, int inittamanyCarregador) {
		this.damage = damage;
		this.modelWeapon = GameObject.FindGameObjectWithTag(tagName);
		
		this.balesTotals = initbales;
		this.balesActualCarregador = initbalesActuals;
		this.tamanyCarregador = inittamanyCarregador;
	}
	
	public int getDamage() {
		return damage;
	}
	
	public int getBalesTotals() {
		return balesTotals;
	}
	
	public int getBalesActualCarregador() {
		return balesActualCarregador;
	}
	
	public int getTamanyCarregador() {
		return tamanyCarregador;
	}
	
	public void hideWeapon() {	
		modelWeapon.SetActive(false);
	}
	
	public void addBalesTotals(int bales) {
		balesTotals += bales;
	}
	
	public void showWeapon() {

    	//modelWeapon.transform.localScale += new Vector3(scalex,scaley,scalez);	
		modelWeapon.SetActive(true);
	}
	
	public int recarregar() {
		Debug.Log("bales totals = "+balesTotals);
		if(balesTotals > 0) {
			//Comprovar quantes bales hi ha el carregador, i afegir les que falten x omplir
			int num_bales = tamanyCarregador - balesActualCarregador;
			//Debug.Log("num bales = "+num_bales);
			//Debug.Log("tamany carregador = "+tamanyCarregador);
			//Debug.Log("bales actual carregador = "+balesActualCarregador);
			if(num_bales <= balesTotals){
				//Debug.Log("entra if");
				balesActualCarregador += num_bales;
				balesTotals -= num_bales;
				return num_bales;
			}
			else {
				//Debug.Log("entra else");
				balesActualCarregador = balesTotals;
				balesTotals -= balesActualCarregador;
				//Debug.Log("bales actual carregador = "+balesActualCarregador);
				return balesActualCarregador;
			}
		}
		return 0;	
	}
	
	public int disparar() {
		//Debug.Log("Disparar");
		if(balesActualCarregador > 0) 
			balesActualCarregador -= 1;
		Debug.Log("Disparo queden "+balesActualCarregador);
		return balesActualCarregador;
	}
		
	
	// Use this for initialization
	void Start () {	
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
