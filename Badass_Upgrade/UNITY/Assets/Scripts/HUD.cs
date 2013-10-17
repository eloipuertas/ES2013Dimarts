using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	/*Tanto el GUIText como el MainCharacter necesitan ser asignados a los correspondientes objetos de la escena
	 * vidaText: un objeto de tipo GUIText
	 * escudoText: un objeto de tipo GUIText
	 * robotProtagonista: un objeto que contenga el script MainCharacter 
	 */
	
	public GUIText vidaText;
	public GUIText escudoText;
	public GUIText enemiesCounterGUIText;
	
	//El nombre del script que contiene la informacion del personaje podria ser distinto
	public MainCharacter robotProtagonista;
	
	//The first method to be called
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
		
		vidaText.text = "Health: " + robotProtagonista.vida.ToString() + "%";
		escudoText.text = "Shield: " + robotProtagonista.escudo.ToString() + "%";
		enemiesCounterGUIText.text = "Remaining enemies: "; //+ robotProtagonista.enemies.ToString();
		//Los atributos vida y escudo del robotProtagonista son public float.
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Por cada frame, actualiza los valores
		vidaText.text = "Health: " + robotProtagonista.vida.ToString() + "%";
		escudoText.text = "Shield: " + robotProtagonista.escudo.ToString() + "%";
		enemiesCounterGUIText.text = "Remaining enemies: "; //+ robotProtagonista.enemies.ToString();
	}
}
