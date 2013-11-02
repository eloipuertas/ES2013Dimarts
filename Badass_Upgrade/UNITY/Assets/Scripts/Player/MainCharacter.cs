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
        
        //Atributs de les armes
        public int posWeapon;
        public int actualWeaponDamage;
        public int balesCarregador;
        public int balesTotalsArmaActual;
        List<Weapon> weapons;
        
        //Atributs de l'accio disparar & melee
        RaycastHit hit;
        Transform cam;
        float meleeDistance = 1.8f;
        float shotDistance = 20f;
        int damageMelee = 10;
        
        float buttonDistance = 1.5f;
        
        //down
        public GameObject player;
        public GameObject leftHand;
        float minScaleY = 0.2f;
        float maxScaleY = 1.0f;
        
        public GameObject cameraPlayer;
        public float minPosCamera;
        public float maxPosCamera;
        
        //Per saber si esta ajupit
        bool down;        
        //Fall damage
        float altura;
        
        public CharacterMotor a;
        
        //Fall
        
        bool falling = false;
        float fallingDamageThreshold = 5f;
        float fallStartLevel;
        
        bool grounded = false;
        
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
                balesTotalsArmaActual = weapons[posWeapon].balesTotals;
                
                
                //melee & down
                leftHand.SetActive(false);
                down = false;
                
                cam = Camera.main.transform;
                
                cameraPlayer = GameObject.FindGameObjectWithTag("MainCamera");
                maxPosCamera = cameraPlayer.transform.localPosition.y;
                minPosCamera = 0.05f;
                
        }
        
         void FallingDamageAlert (float fallDistance) {
        Debug.Log("Ouch! Fell " + fallDistance + " units!");   
    }
        
        // Update is called once per frame
        void Update () {
                
                if (grounded) {
                        if (falling) {
                    falling = false;
                    if (player.transform.localPosition.y < fallStartLevel - fallingDamageThreshold)
                        FallingDamageAlert(fallStartLevel - player.transform.localPosition.y);
                       }
                }
                 else {
                         if (!falling) {
                        falling = true;
                        fallStartLevel = player.transform.localPosition.y;
                    }
                }
                
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
                        
                }
                else if(Input.GetButtonDown("Arma 1")) {
                        //1-Escalo a 0 l'actual posWeapon
                        weapons[posWeapon].hideWeapon();
                        //2-Poso a 0 ja que l'arma 1 es a la posicio 0
                        posWeapon = 0;
                        actualWeaponDamage = weapons[posWeapon].getDamage();
                        balesCarregador = weapons[posWeapon].getBalesActualCarregador();
                        balesTotalsArmaActual = weapons[posWeapon].balesTotals;
                        //3-Escalo a pos el nou
                        weapons[posWeapon].showWeapon();
                        //Debug.Log("Arma 1 "+weapons[posWeapon].damage);
                }
                else if(Input.GetButtonDown("Arma 2")) {
                        weapons[posWeapon].hideWeapon();
                        posWeapon = 1;
                        actualWeaponDamage = weapons[posWeapon].getDamage();
                        balesCarregador = weapons[posWeapon].getBalesActualCarregador();
                        balesTotalsArmaActual = weapons[posWeapon].balesTotals;
                        weapons[posWeapon].showWeapon();
                        
                }
                else if(Input.GetButtonDown("Arma 3")) {
                        weapons[posWeapon].hideWeapon();
                        posWeapon = 2;
                        actualWeaponDamage = weapons[posWeapon].getDamage();
                        balesCarregador = weapons[posWeapon].getBalesActualCarregador();
                        balesTotalsArmaActual = weapons[posWeapon].balesTotals;
                        weapons[posWeapon].showWeapon();
                }
                else if(Input.GetButtonDown("Recargar")) {
                        balesCarregador = weapons[posWeapon].recarregar();
                        balesTotalsArmaActual = weapons[posWeapon].balesTotals;
                        if(balesCarregador <= 0)
                                Debug.Log("No hi ha mes municio");
                }
                else if(Input.GetButtonDown("Agacharse")) {
                        if(cameraPlayer.transform.localPosition.y > minPosCamera) {
                                Debug.Log("Min = "+minPosCamera);
                                
                                float tmp = cameraPlayer.transform.localPosition.y - minPosCamera; 
                                
                                Debug.Log("tmp = "+tmp);
                                cameraPlayer.transform.localPosition -= new Vector3(0f,tmp,0f);        
                        }
                        
                        down = true;
                        
                        /*
                        if(player.transform.localScale.y > minScaleY) {
                                player.transform.localScale -= new Vector3(0, minScaleY, 0);
                                //leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
                                
                                //Per eliminar braç quant s'ajup
                                //leftHand.SetActive(false);
                                //leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
                        }*/
                                
                }
                else if(Input.GetButtonUp("Agacharse")) {
                        Debug.Log("Max = "+maxPosCamera);
                        Debug.Log("camera y up= "+cameraPlayer.transform.localPosition.y);
                        if(cameraPlayer.transform.localPosition.y < maxPosCamera) {
                                
                                float tmp2 = maxPosCamera - cameraPlayer.transform.localPosition.y;
                                Debug.Log("tmp2 = "+tmp2);
                                cameraPlayer.transform.localPosition += new Vector3(0f,tmp2,0f);        
                        }
                        down = false;
                        
                        /*
                        if(player.transform.localScale.y < maxScaleY) {
                                player.transform.position += new Vector3(0, minScaleY, 0);
                                player.transform.localScale += new Vector3(0, minScaleY, 0);
                                //leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
                                //Tornar a posar braç
                                //leftHand.SetActive(true);
                                //leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
                        }*/
                                
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
                else if((Input.GetButtonDown("Usar"))) {
                        
                        //Proves caminar
                        
                        
                        //Animacio
                        //leftHand.SetActive(true);
                        //leftHand.animation.Play("ArmatureAction");
                        if(Physics.Raycast(cam.position, cam.forward,out hit, buttonDistance)) {
                                if(hit.collider.gameObject.tag == "Button") {
                                        //Enviar que el boto l'he apretat amb el metode que diguin els de escenari (enviar un true)
                                        //hit.transform.gameObject.SendMessage("rebreDany",damageMelee);
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
                //AudioSource.PlayClipAtPoint(impactSound,transform.position,0.15F);
                Debug.Log("dany "+dany);
                Debug.Log("escudo "+escudo);
                Debug.Log("vida "+vida);
                if(escudo > 0) {
                        escudo -= dany;
                        Debug.Log("escudo - dany = "+escudo);
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