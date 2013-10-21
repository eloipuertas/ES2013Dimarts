using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {
	
	public int vida;
	public int escudo;
	public bool vivo;

	void Awake () {
		
		init(100,100);
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void init(int vida, int escudo) {
		this.vida = vida;
		this.escudo = escudo;
		this.vivo = true;
	}
	
	void addItemVida(int valor) {
		this.vida += valor;
	}
	
	void addItemEscudo(int valor) {
		this.escudo += valor;
	}
	
	//Per dany resto a escut i el que sobri a vida i anar fent axi
	bool rebreAtac(int dany) {
		return false;		
	}
}
