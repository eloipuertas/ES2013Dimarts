using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : MonoBehaviour {
	
	//Atributs del player
	public int vida;
	public int escudo;
	public bool vivo;
	
	//Atributs de les armes
	int posWeapon;
	int actualWeaponDamage;
	List<Weapon> weapons;
	//Weapon w1 = new Weapon();
	//Weapon w2 = new Weapon();
	//Weapon w3 = new Weapon();
	
	//Atributs de l'accio disparar
	RaycastHit hit;
	float shotDistance = 20f;
	Transform cam; 
	
	
	
	void Awake () {	
		
	}

	// Use this for initialization
	void Start () {
		
		weapons = new List<Weapon>();
		posWeapon = 0;
		init(100,100);
		initWeapons();
		hiddenAllWeapons();
		
		cam = Camera.main.transform;
		
		//mostro arma 1
		//Debug.Log("dany inciial "+weapons[posWeapon].damage);
		//weapons[posWeapon].showWeapon();
			
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Input.GetButtonDown("Disparar")) {
			if(Physics.Raycast(cam.position, cam.forward,out hit, shotDistance)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					Debug.Log("Disparo i toco l'enemic i li faig "+actualWeaponDamage+" punts de dany");
					//Agafar de l'arma que estic el mal que fa i enviarl-la al enemic
				}
			}
			
		}
		else if(Input.GetButtonDown("Arma 1")) {
			//1-Escalo a 0 l'actual posWeapon
			//weapons[posWeapon].hideWeapon();
			//2-Poso a 0 ja que l'arma 1 es a la posicio 0
			posWeapon = 0;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			//3-Escalo a pos el nou
			//weapons[posWeapon].showWeapon();
			//Debug.Log("Arma 1 "+weapons[posWeapon].damage);
		}
		else if(Input.GetButtonDown("Arma 2")) {
			//1-Escalo a 0 l'actual posWeapon
			//weapons[posWeapon].hideWeapon();
			//2-Poso a 1 ja que l'arma 1 es a la posicio 0
			posWeapon = 1;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			//3-Escalo a pos el nou
			//weapons[posWeapon].showWeapon();
			//Debug.Log("Arma 2 "+weapons[posWeapon].damage);
			
		}
		else if(Input.GetButtonDown("Arma 3")) {
			//1-Escalo a 0 l'actual posWeapon
			//weapons[posWeapon].hideWeapon();
			//2-Poso a 2 ja que l'arma 1 es a la posicio 0
			posWeapon = 2;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			//3-Escalo a pos el nou
			//weapons[posWeapon].showWeapon();
			//Debug.Log("Arma 3 "+weapons[posWeapon].damage);
		}
	}
	
	void init(int vida, int escudo) {
		this.vida = vida;
		this.escudo = escudo;
		this.vivo = true;
	}
	
	//Getters i setters dels atributs del player
	
	int getVida() {
		return vida;
	}
	
	void setVida(int novaVida) {
		this.vida = novaVida;
	}
	
	int getEscudo() {
		return escudo;
	}
	
	void setEscudo(int nouEscudo) {
		this.escudo = nouEscudo;
	}
	
	bool getVivo() {
		return vivo;
	}
	
	void setVivo(bool nouVivo) {
		this.vivo = nouVivo;
	}
	
	//Metodes per recollir items de vida i escut 
	
	void addItemVida(int valor) {
		this.vida += valor;
	}
	
	void addItemEscudo(int valor) {
		this.escudo += valor;
	}
	
	
	void rebreAtac(int dany) {
		if(escudo > 0) {
			escudo -= dany;
			if(escudo < 0)
				vida -= escudo;
		}
		else
			vida -= dany;			
	}
	
	bool PlayerIsLived() {
		if(vida > 0)
			return true;
		return false;
	}
	
	void atack() {
		
		//return actualWeaponDamage;
	}
	
	
	//Iniciar armes
	void initWeapons() {
		
		Weapon w1 = new Weapon();
		Weapon w2 = new Weapon();
		Weapon w3 = new Weapon();
		
		w1.init(50,"weapon1");
		w2.init(100,"weapon2");
		w3.init(120,"weapon3");
		
		weapons.Add(w1);
		weapons.Add(w2);
		weapons.Add(w3);
		
		//Arma actual la primera (weapon 1)
		actualWeaponDamage = weapons[posWeapon].damage;
		
	}
	
	//Per amagar el model de totes les armes
	void hiddenAllWeapons() {
		for(int i = 0; i < weapons.Count; i++) {
			weapons[i].hideWeapon();
		}
	}
	
	/*void OnCollisionEnter(Collision collision) {
    	if(collision.gameObject.tag == "Enemy"){
  			Debug.Log("L'enemic ma tocat");
 		}
		else
			Debug.Log("he tocat algo");
    }*/
	
	//Dectacta nomes tot am el que colisiono
	/*void OnControllerColliderHit(ControllerColliderHit hit)
	{
		Debug.Log("hola");
		if(hit.gameObject.name == "enemy_01")
			Debug.Log("adeu");
     //Respond to collision
	}*/
	
	
}




































