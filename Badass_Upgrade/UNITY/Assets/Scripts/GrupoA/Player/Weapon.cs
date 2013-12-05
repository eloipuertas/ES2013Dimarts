﻿using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour {
	
	
	public int damage;
	public GameObject modelWeapon;
	
	public int balesTotals;
	public int balesActualCarregador;
	public int tamanyCarregador;
	public float tiempoAnimacionCaminarMAX = 3f;
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
		modelWeapon.SetActive(true);
	}
	
	public void moveWeapon(){
		Debug.Log("Animacio Correr");
		modelWeapon.animation.Play("Correr");		
	}
	
	public void walkWeapon(){
		Debug.Log("Animacio Caminar");
		modelWeapon.animation.Play("Caminar");
	}
	
	public void useButton(){
		modelWeapon.animation.Play("ActivarBoton");
	}
	
	public void rebreDany(){
		modelWeapon.animation.Play("Dany");
	}
	
	public void standBy(){
		modelWeapon.animation.Play("StandBy");
	}
	
	public void enfundar(){
		modelWeapon.animation.Play("Enfundar");
	}
	
	public void meeleWeapon(){
		if(Random.value<0.5f){
			modelWeapon.animation.Play ("AtaqueMelee2");//Aqui utilizare un random para cambiar entre las dos posibles
		}
		else{
			modelWeapon.animation.Play ("AtaqueMelee1");
		}
	}
	
	public int recarregar() {
		if((balesTotals > 0) && (balesActualCarregador < tamanyCarregador)) {
			modelWeapon.animation.Play("Recargar");		
			//Comprovar quantes bales hi ha el carregador, i afegir les que falten x omplir
			int num_bales = tamanyCarregador - balesActualCarregador;
			if(num_bales <= balesTotals){
				balesActualCarregador += num_bales;
				balesTotals -= num_bales;
				return balesActualCarregador;
			}
			else {
				balesActualCarregador += balesTotals;
				//Es 0 ja que si entro aqui vol dir que tinc menys bales que posicions al carregador
				balesTotals = 0;
				return balesActualCarregador;
			}						
		}
		return balesActualCarregador;	
	}
	
	public int disparar() {
		//Debug.Log("tag = "+modelWeapon.tag);
		if(balesActualCarregador > 0) 			
			balesActualCarregador -= 1;
			modelWeapon.animation.Play("Disparar");
		return balesActualCarregador;
	}
		
	
	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
}
