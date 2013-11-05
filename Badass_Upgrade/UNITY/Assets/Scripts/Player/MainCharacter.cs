using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : MonoBehaviour {
	
	//Atributs del player
	public int vida;
	public int escudo;
	
	public int maxVida = 100;
	public int maxEscudo = 100;
	
	public bool vivo;
	
	// So en rebre un impacte
	public AudioClip impactSound;
	
	// So de les armes 
	public AudioClip[] weaponSound;
	
	//Atributs de les armes
	int posWeapon;
	int actualWeaponDamage;
	int balesCarregador;
	List<Weapon> weapons;
	
	//Atributs de l'accio disparar & melee
	RaycastHit hit;
	Transform cam;
	float meleeDistance = 1.8f;
	float shotDistance = 20f;
	float buttonDistance = 20f;
	int damageMelee = 10;
	
	//down
	public GameObject player;
	public GameObject leftHand;
	float minScaleY = 0.2f;
	float maxScaleY = 1.0f;
	//Per saber si esta ajupit
	bool down;
	
	void Awake () {	
		
	}

	// Use this for initialization
	void Start () {
		
		//armes
		weapons = new List<Weapon>();
		posWeapon = 0;
		
		init(maxVida,maxEscudo);
		initWeapons();
		hiddenAllWeapons();
		//mostro arma 1
		weapons[posWeapon].showWeapon();
		
		//melee & down
		leftHand.SetActive(false);
		down = false;
		
		cam = Camera.main.transform;
			
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetButtonDown("Disparar")) && (balesCarregador > 0)) {
			
                        balesCarregador = weapons[posWeapon].disparar();
                        if(Physics.Raycast(cam.position, cam.forward,out hit, shotDistance)) {
                                if(hit.collider.gameObject.tag == "Enemy") {
                                        Debug.Log("Disparo i toco l'enemic i li faig "+actualWeaponDamage+" punts de dany");
                                        hit.transform.gameObject.SendMessage("rebreDany",actualWeaponDamage);
                                }
                                else if(hit.collider.gameObject.tag == "Barril") {
                                        Debug.Log("Disparo contre el barril");
                                        hit.transform.gameObject.SendMessage("rebreTir");
                                }
                        }
                 AudioSource.PlayClipAtPoint(weaponSound[1],transform.position,0.15F);       
                }
		if((Input.GetButtonDown("Usar"))) { //Proves caminar //Animacio //leftHand.SetActive(true); //leftHand.animation.Play("ArmatureAction"); 
			Debug.Log("Usar boto");
			if(Physics.Raycast(cam.position, cam.forward,out hit, buttonDistance)) { 
				Debug.Log("111111111");
				if(hit.collider.gameObject.tag == "Button") { //Enviar que el boto l'he apretat amb el metode que diguin els de escenari (enviar un true) //hit.transform.gameObject.SendMessage("rebreDany",damageMelee); 
					Debug.Log("22222222222");
					hit.transform.gameObject.SendMessage("activarBoto");
					Debug.Log("3333333");
				} 
			} 
		}
		
					
					
		else if(Input.GetButtonDown("Arma 1")) {
			//1-Escalo a 0 l'actual posWeapon
			weapons[posWeapon].hideWeapon();
			//2-Poso a 0 ja que l'arma 1 es a la posicio 0
			posWeapon = 0;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			balesCarregador = weapons[posWeapon].getBalesActualCarregador();
			//3-Escalo a pos el nou
			weapons[posWeapon].showWeapon();
			//Debug.Log("Arma 1 "+weapons[posWeapon].damage);
		}
		else if(Input.GetButtonDown("Arma 2")) {
			weapons[posWeapon].hideWeapon();
			posWeapon = 1;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			balesCarregador = weapons[posWeapon].getBalesActualCarregador();
			weapons[posWeapon].showWeapon();
			
		}
		else if(Input.GetButtonDown("Arma 3")) {
			weapons[posWeapon].hideWeapon();
			posWeapon = 2;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			balesCarregador = weapons[posWeapon].getBalesActualCarregador();
			weapons[posWeapon].showWeapon();
		}
		else if(Input.GetButtonDown("Recargar")) {
			Debug.Log("Recarrego l'arma");
			balesCarregador = weapons[posWeapon].recarregar();
			if(balesCarregador <= 0)
				Debug.Log("No hi ha mes municio");
			else
				AudioSource.PlayClipAtPoint(weaponSound[0],transform.position,0.15F);
		}
		else if(Input.GetButtonDown("Agacharse")) {
			down = true;
			if(player.transform.localScale.y > minScaleY) {
				player.transform.localScale -= new Vector3(0, minScaleY, 0);
				//leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
				
				//Per eliminar braç quant s'ajup
				//leftHand.SetActive(false);
				//leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
			}
				
		}
		else if(Input.GetButtonUp("Agacharse")) {
			down = false;
			if(player.transform.localScale.y < maxScaleY) {
				player.transform.position += new Vector3(0, minScaleY, 0);
				player.transform.localScale += new Vector3(0, minScaleY, 0);
				//leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
				//Tornar a posar braç
				//leftHand.SetActive(true);
				//leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
			}
				
		}
		else if((Input.GetButtonDown("Melee")) && (down == false)) {
			leftHand.SetActive(true);
			leftHand.animation.Play("ArmatureAction");
			if(Physics.Raycast(cam.position, cam.forward,out hit, meleeDistance)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					//Enviar el dany directament
					hit.transform.gameObject.SendMessage("rebreDany",damageMelee);
				}
			}
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
	
	//Metodes per recollir items de vida, escut i municio
	
	void addItemVida(int valor) {
		int novaVida = vida + valor;
		if(novaVida <= maxVida)
			this.vida = novaVida;
		else
			this.vida = maxVida;
	}
	
	void addItemEscudo(int valor) {
		int nouEscut = escudo + valor;
		if(nouEscut <= maxEscudo)
			this.escudo = nouEscut;
		else
			this.escudo = maxEscudo;
	}
	
	void addItemMunicio(int municio) {
		weapons[posWeapon].addBalesTotals(municio);
	}
	
	//Rebre dany de l'enemic
	void rebreAtac(int dany) {
		AudioSource.PlayClipAtPoint(impactSound,transform.position,0.15F);
		if(escudo > 0) {
			escudo -= dany;
			if(escudo < 0)
				//Sumu ja que sera negatiu
				vida += escudo;
		}
		else {
			vida -= dany;
			if(vida <= 0) {
				vida = 0;
				setVivo(false);
			}
		}
	}
	
	bool PlayerIsLived() {
		if(vida > 0)
			return true;
		return false;
	}
	
	//Iniciar armes
	void initWeapons() {
		
		Weapon w1 = new Weapon();
		Weapon w2 = new Weapon();
		Weapon w3 = new Weapon();
		
		//dany, tag model, bales totals, bales carregador, tamany carregador
		w1.init(10,"weapon1",100,10,10);
		w2.init(25,"weapon2",40,4,4);
		w3.init(50,"weapon3",3,6,6);
		
		weapons.Add(w1);
		weapons.Add(w2);
		weapons.Add(w3);
		
		//Arma actual la primera (weapon 1) i les bales que te al carregador
		actualWeaponDamage = weapons[posWeapon].getDamage();
		balesCarregador = weapons[posWeapon].getBalesActualCarregador();
		
	}
	
	//Per amagar el model de totes les armes
	void hiddenAllWeapons() {
		for(int i = 0; i < weapons.Count; i++) {
			weapons[i].hideWeapon();
		}
	}	
}




































