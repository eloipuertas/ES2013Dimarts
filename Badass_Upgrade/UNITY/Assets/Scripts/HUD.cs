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
	
<<<<<<< HEAD
	//El nombre del script que contiene la informacion del personaje podria ser distinto
	//public MainCharacter robotProtagonista;
=======
	public Weapon armaEquipada;
	public MainCharacter robotProtagonista;
//	public EnemiesAmount enemiesCounter;
>>>>>>> origin/Grupo-E
	
	//The first method to be called
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
		
<<<<<<< HEAD
		//vidaText.text = "Health: " + robotProtagonista.vida.ToString() + "%";
		//escudoText.text = "Shield: " + robotProtagonista.escudo.ToString() + "%";
		//enemiesCounterGUIText.text = "Remaining enemies: "; //+ robotProtagonista.enemies.ToString();
		//Los atributos vida y escudo del robotProtagonista son public float.
=======
		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";
//		enemiesCounterGUIText.text = "Remaining enemies: " + enemiesCounter.numOfEnem.ToString();
		
		balasCargadorText.text = armaEquipada.getBalesActualCarregador().ToString();
		balasTotalesText.text = armaEquipada.getBalesTotals().ToString();
>>>>>>> origin/Grupo-E
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//Carga el menu game over si la vida baja a 0.
		if(robotProtagonista.vida <= 0){
			Application.LoadLevel(game_over);
		}
		
		//Por cada frame, actualiza los valores
<<<<<<< HEAD
		//vidaText.text = "Health: " + robotProtagonista.vida.ToString() + "%";
		//escudoText.text = "Shield: " + robotProtagonista.escudo.ToString() + "%";
		//enemiesCounterGUIText.text = "Remaining enemies: "; //+ robotProtagonista.enemies.ToString();
=======
		vidaText.text = robotProtagonista.vida.ToString() + "%";
		escudoText.text = robotProtagonista.escudo.ToString() + "%";
//		enemiesCounterGUIText.text = "Remaining enemies: " + enemiesCounter.numOfEnem.ToString();
		
		
		balasCargadorText.text = armaEquipada.getBalesActualCarregador().ToString();
		balasTotalesText.text = armaEquipada.getBalesTotals().ToString();
		
>>>>>>> origin/Grupo-E
	}
}
