using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : MonoBehaviour {
	
	//Atributs del player
	public int vida;
	public int escudo;
	
	public int maxVida = 100;
	public int maxEscudo = 100;
	public int random; 
	public bool vivo;
	
	// So en rebre un impacte
	public AudioClip impactSound;
	
	// So del cop de puny
	public AudioClip meleSound;
	
	// So de les armes 
	public AudioClip[] weaponSound;
	public AudioClip weaponSwap;
	
	//Atributs de les armes
	public int posWeapon;
	public int actualWeaponDamage;
	public int balesCarregador;
	public int balesTotalsArmaActual;
	List<Weapon> weapons;
	
	//Atributs de l'accio disparar & melee
	RaycastHit hit;
	public Transform cam;
	float meleeDistance = 1.8f;
	public float shotDistance = 20f;
	int damageMelee = 10;	
	float buttonDistance = 1.7f;
	
	//down
	public GameObject player;
	float minScaleY = 0.5f;
	float maxScaleY = 1.0f;
	
	public GameObject cameraPlayer;
	public float minPosCamera;
	public float maxPosCamera;
	
	//Per saber si esta ajupit
	bool down;	
	
	//Per la llum en disparar 
	//private muzzleFlash shotLight;
	
	//public GameObject bullet;
	public float speed = 500f; 
	public Rigidbody projectile;
	public GameObject weapon2;
	MouseLook mouseLook;
	public GameObject weapon1;
	
	//S'haura d'ajustar en funcio del model de l'arma
	float xPosShot = -0.5f;
	float yPosShot;
	float constY = 2.5f;
	public GameObject posBullet;
	
	float tempsActual;
	float tempsAnterior;
	public float fireRate = 0.5f;
	
	float tempsActualStandBy;
	float tempsAnteriorStandBy;
	public float thresholdStandBy = 5f;
	
	public float tiempoAnimacionCaminarMAXWeapon1 = 3f;
	private float tiempoAnimacionCaminarWeapon1 = 0f;
	
	public float tiempoAnimacionCaminarMAXWeapon2 = 2f;
	private float tiempoAnimacionCaminarWeapon2 = 0f;
	
	void Awake () {	
		
	}

	// Use this for initialization
	void Start () {
		
		//ypos = ml.getYPosition();
		
		//armes
		weapons = new List<Weapon>();
		posWeapon = 0;
		
		init(maxVida,maxEscudo);
		initWeapons();
		hiddenAllWeapons();
		//mostro arma 1
		weapons[posWeapon].showWeapon();
		balesTotalsArmaActual = weapons[posWeapon].balesTotals;
		
		//melee & down
		down = false;
		
		cam = Camera.main.transform;
		
		
		cameraPlayer = GameObject.FindGameObjectWithTag("MainCamera");
		maxPosCamera = cameraPlayer.transform.localPosition.y;
		minPosCamera = 0.05f;
		
		
		mouseLook = cam.GetComponent <MouseLook>();
		
		
		//Inicialitzo el temps
		tempsAnterior = Time.time;
		tempsAnteriorStandBy = tempsAnterior;
	}
	
	// Update is called once per frame
	void Update () {
		if((Input.GetButtonDown("Disparar")) && (balesCarregador > 0) && (posWeapon == 0) ) {
			tempsAnteriorStandBy = Time.time;
			balesCarregador = weapons[posWeapon].disparar();
			if(Physics.Raycast(weapon1.transform.position,cam.forward,out hit, shotDistance)) {
		        if(hit.collider.gameObject.tag == "Enemy") {
	                hit.transform.gameObject.SendMessage("rebreDany",actualWeaponDamage);
		        }
		        else if(hit.collider.gameObject.tag == "Barril") {
	                hit.transform.gameObject.SendMessage("rebreTir");
		        }
			}
			player.SendMessage("Shoot");
			AudioSource.PlayClipAtPoint(weaponSound[1],transform.position,0.9F);			
		}
		else if((Input.GetButton("Disparar")) && (posWeapon == 1) && (balesCarregador > 0)) {
			tempsAnteriorStandBy = Time.time;
			tempsActual = Time.time;
			if(tempsActual - tempsAnterior > fireRate) {
				//Actualitzo el temps anterior
				tempsAnterior = tempsActual;
				yPosShot = mouseLook.rotationY + constY;	
				Rigidbody instantedProjectile = Instantiate(projectile,posBullet.transform.position,cameraPlayer.transform.rotation) as Rigidbody;
				//instantedProjectile.velocity = transform.TransformDirection(new Vector3(xPosShot,yPosShot,speed));
				instantedProjectile.velocity = transform.TransformDirection(new Vector3(0f,yPosShot,speed));
				//instantedProjectile.velocity = transform.TransformDirection(new Vector3(0f,0f,speed));
				instantedProjectile.SendMessage("addDamage",weapons[posWeapon].damage);
				balesCarregador = weapons[posWeapon].disparar();
				
				player.SendMessage("Shoot");
				AudioSource.PlayClipAtPoint(weaponSound[1],transform.position,0.9F);
			}			
		}
		else if(Input.GetButtonDown("Arma 1")) {
			tempsAnteriorStandBy = Time.time;
			weapons[posWeapon].enfundar();
			weapons[posWeapon].hideWeapon();
			//Poso a 0 ja que l'arma 1 es a la posicio 0
			posWeapon = 0;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			balesCarregador = weapons[posWeapon].getBalesActualCarregador();
			balesTotalsArmaActual = weapons[posWeapon].balesTotals;
			weapons[posWeapon].showWeapon();
			
			AudioSource.PlayClipAtPoint(weaponSwap, transform.position, 0.9f);
			
		}
		else if(Input.GetButtonDown("Arma 2")) {
			tempsAnteriorStandBy = Time.time;
			weapons[posWeapon].enfundar();
			weapons[posWeapon].hideWeapon();
			posWeapon = 1;
			actualWeaponDamage = weapons[posWeapon].getDamage();
			balesCarregador = weapons[posWeapon].getBalesActualCarregador();
			balesTotalsArmaActual = weapons[posWeapon].balesTotals;
			weapons[posWeapon].showWeapon();
			
			AudioSource.PlayClipAtPoint(weaponSwap, transform.position, 0.9f);
			
		}
		else if(Input.GetButtonDown("Recargar")) {
			tempsAnteriorStandBy = Time.time;
			balesCarregador = weapons[posWeapon].recarregar();
			balesTotalsArmaActual = weapons[posWeapon].balesTotals;
			if(balesCarregador <= 0)
				Debug.Log("No hi ha mes municio");
			else
				AudioSource.PlayClipAtPoint(weaponSound[0],transform.position,0.15F);
		}
		else if(Input.GetButtonDown("Agacharse")) {
			tempsAnteriorStandBy = Time.time;
			weapons[posWeapon].walkWeapon();
			if(cameraPlayer.transform.localPosition.y > minPosCamera) {
				float tmp = cameraPlayer.transform.localPosition.y - minPosCamera; 
				cameraPlayer.transform.localPosition -= new Vector3(0f,tmp,0f);	
			}
			down = true;
				
		}
		else if(Input.GetButtonUp("Agacharse")) {
			tempsAnteriorStandBy = Time.time;
			if(cameraPlayer.transform.localPosition.y < maxPosCamera) {
				float tmp2 = maxPosCamera - cameraPlayer.transform.localPosition.y;
				cameraPlayer.transform.localPosition += new Vector3(0f,tmp2,0f);	
			}
			down = false;		
		}
		else if((Input.GetButtonDown("Melee")) && (down == false)) {
			tempsAnteriorStandBy = Time.time;
			weapons[posWeapon].meeleWeapon();
			if(Physics.Raycast(cam.position, cam.forward,out hit, meleeDistance)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					hit.transform.gameObject.SendMessage("rebreDany",damageMelee);
				}
			}
			AudioSource.PlayClipAtPoint(meleSound,transform.position,0.9F);
		}
		else if((Input.GetButtonDown("Usar"))) {
			tempsAnteriorStandBy = Time.time;
			weapons[posWeapon].useButton();			
			if(Physics.Raycast(cam.position, cam.forward,out hit, buttonDistance)) { 
				if(hit.collider.gameObject.tag == "Button") {  
					hit.transform.gameObject.SendMessage("activarBoto");
				} 
			} 
		}
		//Actualitzar el temps dels altres botons
		else if((Input.GetButtonDown("Horizontal")) || (Input.GetButtonDown("Vertical")) || (Input.GetButtonDown("Saltar")) || (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") > 0) || (Input.GetButtonDown("Linterna")) || (Input.GetButton("Caminar"))) {
			tempsAnteriorStandBy = Time.time;
		}
		
		
		if(Time.time - tempsAnteriorStandBy > thresholdStandBy) {
			//Debug.Log("Animacio StandBy");
			tempsAnteriorStandBy = Time.time;
			weapons[posWeapon].standBy();
		}
		
		if(posWeapon == 0) {
			if(tiempoAnimacionCaminarWeapon1 <= 0){
				//Debug.Log("Animacio caminar posweapon "+weapons[posWeapon].tag);
				if((Input.GetButton("Horizontal") && !Input.GetButton("Caminar")) || (Input.GetButton("Vertical")  && !Input.GetButton("Caminar") )) {		
					//Debug.Log("Entra a correr" );
					weapons[posWeapon].moveWeapon();
					tiempoAnimacionCaminarWeapon1 = tiempoAnimacionCaminarMAXWeapon1;
					tempsAnteriorStandBy = Time.time;
				}			
				else if((Input.GetButton("Horizontal") && Input.GetButton("Caminar")) || (Input.GetButton("Vertical") && Input.GetButton("Caminar")))	{
					//Debug.Log("Entra a caminar" );
					weapons[posWeapon].walkWeapon();
					tiempoAnimacionCaminarWeapon1 = tiempoAnimacionCaminarMAXWeapon1;
					tempsAnteriorStandBy = Time.time;
				}
			}
			tiempoAnimacionCaminarWeapon1 = tiempoAnimacionCaminarWeapon1 - Time.deltaTime;
		}
		else if(posWeapon == 1) {
			if(tiempoAnimacionCaminarWeapon2 <= 0){	
				//Debug.Log("Animacio caminar posweapon "+weapons[posWeapon].tag);
				if((Input.GetButton("Horizontal") && !Input.GetButton("Caminar")) || (Input.GetButton("Vertical")  && !Input.GetButton("Caminar") )) {		
					//Debug.Log("Entra a correr" );
					weapons[posWeapon].moveWeapon();
					tiempoAnimacionCaminarWeapon2 = tiempoAnimacionCaminarMAXWeapon2;
					tempsAnteriorStandBy = Time.time;
				}			
				else if((Input.GetButton("Horizontal") && Input.GetButton("Caminar")) || (Input.GetButton("Vertical") && Input.GetButton("Caminar")))	{
					//Debug.Log("Entra a caminar" );
					weapons[posWeapon].walkWeapon();
					tiempoAnimacionCaminarWeapon2 = tiempoAnimacionCaminarMAXWeapon2;
					tempsAnteriorStandBy = Time.time;
				}
			}
			tiempoAnimacionCaminarWeapon2 = tiempoAnimacionCaminarWeapon2 - Time.deltaTime;	
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
	
	void addItemMunicio(int[] municioWeapon) {
		int municio = municioWeapon[0];
		int pos = municioWeapon[1];
		weapons[municioWeapon[1]].addBalesTotals(municio);
		if(posWeapon == pos)
			balesTotalsArmaActual += municio;
		
	}
	//Rebre dany de l'enemic
	void rebreAtac(int dany) {
		tempsAnteriorStandBy = Time.time;
		Debug.Log("dany "+dany);
		if(escudo > 0) {
			escudo -= dany;
			if(escudo < 0) {
				//Sumu ja que sera negatiu
				vida += escudo;
				escudo = 0;
			}
		}
		else {
			vida -= dany;
			if(vida <= 0) {
				vida = 0;
				setVivo(false);
			}
		}
	weapons[posWeapon].rebreDany();
	Debug.Log("escudo = "+escudo);
	Debug.Log("vida = "+vida);
    AudioSource.PlayClipAtPoint(impactSound,transform.position,0.15F);
		
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
		
		//dany, tag model, bales totals, bales carregador, tamany carregador
		w1.init(10,"weapon1",100,10,10);
		w2.init(25,"weapon2",75,25,25);
		
		weapons.Add(w1);
		weapons.Add(w2);
		
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




































