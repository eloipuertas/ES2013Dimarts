﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	
	
	public int damage;
	public GameObject modelWeapon;
	
	public int balesTotals;
	public int balesActualCarregador;
	public int tamanyCarregador;
	public float tiempoAnimacionCaminarMAX = 1f;
	private float tiempoAnimacionCaminar = 0f;
	
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
	
	//Gestio de items de municio
	/*public void addBalesTotalsTag(string tagWeapon, int bales) {
		if(tagWeapon.CompareTo(modelWeapon.tag) == 0)
			addBalesTotals(bales);
	}*/
	
	public void addBalesTotals(int bales) {
		balesTotals += bales;
	}
	
	public void showWeapon() {
    	//modelWeapon.transform.localScale += new Vector3(scalex,scaley,scalez);	
		modelWeapon.SetActive(true);
	}
	
	public void moveWeapon(){
		modelWeapon.animation.Play("Caminar");		
	}
	
	public int recarregar() {
		Debug.Log("bales totals "+balesTotals);
		Debug.Log("bales actuals carregador "+balesActualCarregador);
		if((balesTotals > 0) && (balesActualCarregador < tamanyCarregador)) {
			Debug.Log("entra if");
				modelWeapon.animation.Play("Recargar");		
				//Comprovar quantes bales hi ha el carregador, i afegir les que falten x omplir
				int num_bales = tamanyCarregador - balesActualCarregador;
				//Debug.Log("num bales = "+num_bales);
				//Debug.Log("tamany carregador = "+tamanyCarregador);
				//Debug.Log("bales actual carregador = "+balesActualCarregador);
				if(num_bales <= balesTotals){
					//Debug.Log("entra if");
					balesActualCarregador += num_bales;
					balesTotals -= num_bales;
					Debug.Log("bales totals = "+balesTotals);
					return num_bales;
				}

			else {
				//Debug.Log("entra else");
				balesActualCarregador += balesTotals;
				//Es 0 ja que si entro aqui vol dir que tinc menys bales que posicions al carregador
				balesTotals = 0;
				//Debug.Log("bales actual carregador = "+balesActualCarregador);
				Debug.Log("bales totals = "+balesTotals);
				return balesActualCarregador;
			}						
		}
		return balesActualCarregador;	
	}
	
	public int disparar() {
		Debug.Log("tag = "+modelWeapon.tag);
		//Debug.Log("Disparar");
		if(balesActualCarregador > 0) 			
			balesActualCarregador -= 1;
			modelWeapon.animation.Play("Disparar");
		Debug.Log("Disparo queden "+balesActualCarregador);
		return balesActualCarregador;
	}
		
	
	// Use this for initialization
	void Start () {	
		
	}
	
	// Update is called once per frame
	void Update () {

		if(tiempoAnimacionCaminar <= 0){			
			if( Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {			
				this.moveWeapon();
				tiempoAnimacionCaminar = tiempoAnimacionCaminarMAX;
			}			
		}
		else{
			tiempoAnimacionCaminar = tiempoAnimacionCaminar - Time.deltaTime;
		}
	}
	
}
