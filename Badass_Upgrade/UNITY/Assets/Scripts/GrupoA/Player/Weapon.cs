using UnityEngine;
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
    	//modelWeapon.transform.localScale += new Vector3(scalex,scaley,scalez);	
		modelWeapon.SetActive(true);
	}
	
	public void moveWeapon(){
		modelWeapon.animation.Play("Correr");		
	}
	
	public void walkWeapon(){
		modelWeapon.animation.Play ("Caminar");
	}
	
	public void useButton(){
		modelWeapon.animation.Play ("ActivarBoton");
	}
	
	public void meeleWeapon(){
		if(Random.value<0.5f)
			    modelWeapon.animation.Play ("AtaqueMelee2");//    .animation.Play("AtaqueMelee1"); //Aqui utilizare un random para cambiar entre las dos posibles
			else
				modelWeapon.animation.Play ("AtaqueMelee1");
	}
	
	public int recarregar() {
		Debug.Log("bales totals "+balesTotals);
		Debug.Log("bales actuals carregador "+balesActualCarregador);
		if((balesTotals > 0) && (balesActualCarregador < tamanyCarregador)) {
			Debug.Log("entra if");
				modelWeapon.animation.Play("Recargar");		
				//Comprovar quantes bales hi ha el carregador, i afegir les que falten x omplir
				int num_bales = tamanyCarregador - balesActualCarregador;
				if(num_bales <= balesTotals){
					balesActualCarregador += num_bales;
					balesTotals -= num_bales;
					//Debug.Log("bales totals = "+balesTotals);
					return balesActualCarregador;
				}

			else {
				//Debug.Log("entra else");
				balesActualCarregador += balesTotals;
				//Es 0 ja que si entro aqui vol dir que tinc menys bales que posicions al carregador
				balesTotals = 0;
				//Debug.Log("bales actual carregador = "+balesActualCarregador);
				//Debug.Log("bales totals = "+balesTotals);
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
		//Debug.Log("Disparo queden "+balesActualCarregador);
		return balesActualCarregador;
	}
		
	
	// Use this for initialization
	void Start () {	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(tiempoAnimacionCaminar <= 0){	
			
			if( (Input.GetButton("Horizontal") && !Input.GetButton("Caminar")) || (Input.GetButton("Vertical")  && !Input.GetButton("Caminar") )) {		
				Debug.Log("Entra a correr" );
				this.moveWeapon();
				tiempoAnimacionCaminar = tiempoAnimacionCaminarMAX;
			}			
			//Fer un OR amb agachasre
			else if((Input.GetButton("Horizontal") && Input.GetButton("Caminar")) || (Input.GetButton("Vertical") && Input.GetButton("Caminar")))	{
				Debug.Log("Entra a caminar" );
				this.walkWeapon();
				tiempoAnimacionCaminar = tiempoAnimacionCaminarMAX;
			}
		}
		tiempoAnimacionCaminar = tiempoAnimacionCaminar - Time.deltaTime;
		
	}
	
}
