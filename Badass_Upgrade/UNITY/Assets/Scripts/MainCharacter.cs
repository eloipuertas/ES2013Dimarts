using UnityEngine;
using System.Collections;

public class MainCharacter : MonoBehaviour {
	
	public int vida;
	public int escudo;
	public int enemies;
	
	void Awake () {
		enemies = 10;
		vida = 100;
		escudo = 100;
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
