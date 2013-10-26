using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	const int game_over = 3;
	
	/*Tanto el GUIText como el MainCharacter necesitan ser asignados a los correspondientes objetos de la escena
	 * vidaText: un objeto de tipo GUIText
	 * escudoText: un objeto de tipo GUIText
	 * enemiesCounterGUIText: un objeto de tipo GUIText
	 * balasCargadorText: un objeto de tipo GUIText
	 * balasTotalesText: un objeto de tipo GUIText
	 * armaEquipada: un objeto que contiene el script con la informacion del arma
	 * robotProtagonista: un objeto que contenga el script MainCharacter 
	 */
	
	public GUIText vidaText;
	public GUIText escudoText;
	public GUIText enemiesCounterGUIText;
	public GUIText balasCargadorText;
	public GUIText balasTotalesText;
	
	public Weapon armaEquipada;
	public MainCharacter robotProtagonista;
//	public EnemiesAmount enemiesCounter;
	
	//The first method to be called
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
		
		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";
//		enemiesCounterGUIText.text = "Remaining enemies: " + enemiesCounter.numOfEnem.ToString();
		
		balasCargadorText.text = armaEquipada.getBalesActualCarregador().ToString();
		balasTotalesText.text = armaEquipada.getBalesTotals().ToString();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Carga el menu game over si la vida baja a 0.
		if(robotProtagonista.vida <= 0){
			Application.LoadLevel(game_over);
		}
		
		//Por cada frame, actualiza los valores
		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";
//		enemiesCounterGUIText.text = "Remaining enemies: " + enemiesCounter.numOfEnem.ToString();
		
		
		balasCargadorText.text = armaEquipada.getBalesActualCarregador().ToString();
		balasTotalesText.text = armaEquipada.getBalesTotals().ToString();
		
	}
}
