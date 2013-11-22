using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	const int game_over = 3;
	
	//Contendra todos de enemigos en la escena
	GameObject[] enem;
	
	//El bloque que impide el paso en la salida
	GameObject portal;
	
	//Numero de enemigos en la escena
	int numOfEnem;
	
	//Slot de arma equipada
	int weaponPos;
	
	//Puntuacion
	private int score;
	
	
	
	public Texture2D linternaApagada;
	public Texture2D linternaEncendida;
	
	
	/* Elementos de texto del HUD
	 * 
	 * vidaText: un objeto de tipo GUIText, muestra la vida
	 * escudoText: un objeto de tipo GUIText, muestra el escudo
	 * balasCargadorText: un objeto de tipo GUIText, muestra las balas del cargador
	 * balasTotalesText: un objeto de tipo GUIText, muestra las balas totales
	 * contadorEnemigos: un objeto de tipo GUIText, muestra el numero de enemigos
	 * slotArma#: un objeto de tipo GUIText, muestra el slot del arma equipada y de las demas
	 */
	public GUIText vidaText;
	public GUIText escudoText;
	public GUIText balasCargadorText;
	public GUIText balasTotalesText;
	public GUIText contadorEnemigos;
	//public GUIText slotArma1;
	//public GUIText slotArma2;
	public GUITexture healthLine;
	public GUITexture shieldLine;
	public GUITexture linternaTexture;
	
	public GUITexture slotArma1;
	public GUITexture slotArma2;

	/* Inicializacion de scripts externos
	 * 
	 * public MainCharacter robotProtagonista: El script que contiene los valores de interes para el HUD
	 */
	public MainCharacter robotProtagonista;
	public linterna linterna;
	
	
	Rect healthWidth;
	Rect shieldWidth;
	
	Rect arma1;
	Rect arma2;
	
	//The first method to be called
	void Awake(){
				
	}
	
	// Use this for initialization
	void Start () {
		
		if(linterna.activeLinterna){
			linternaTexture.texture = linternaEncendida;
		}else{
			linternaTexture.texture = linternaApagada;
		}
		
		enem = GameObject.FindGameObjectsWithTag("Enemy");
		numOfEnem = enem.Length;
		contadorEnemigos.text=numOfEnem.ToString();
		
		portal = GameObject.FindGameObjectWithTag("porta1");

		vidaText.text = robotProtagonista.vida.ToString();
		escudoText.text = robotProtagonista.escudo.ToString();
		
		balasCargadorText.text = robotProtagonista.balesCarregador.ToString();
		balasTotalesText.text = robotProtagonista.balesTotalsArmaActual.ToString();
		

		
		arma1 = slotArma1.pixelInset;
		arma2 = slotArma2.pixelInset;
		
		
		weaponPos = robotProtagonista.posWeapon;
		
		healthWidth = healthLine.pixelInset;
		shieldWidth = shieldLine.pixelInset;
		
		score = 0;
	
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(linterna.activeLinterna){
			linternaTexture.texture = linternaEncendida;
		}else{
			linternaTexture.texture = linternaApagada;
		}
		
		//Carga el menu game over si la vida baja a 0.
		if(robotProtagonista.vida <= 0){
			Application.LoadLevel(game_over);
		}
		
		//Por cada frame, actualiza los valores
		
		healthWidth.width = robotProtagonista.vida;
		healthLine.pixelInset = healthWidth;
		
		shieldWidth.width = robotProtagonista.escudo;
		shieldLine.pixelInset = shieldWidth;

		vidaText.text = robotProtagonista.vida.ToString();
		escudoText.text = robotProtagonista.escudo.ToString();		
		
		balasCargadorText.text = robotProtagonista.balesCarregador.ToString();;
		balasTotalesText.text = robotProtagonista.balesTotalsArmaActual.ToString();
		
		weaponPos = robotProtagonista.posWeapon;
		
		if(weaponPos == 0){
			
			arma1.width = 40;
			arma1.height = 28;
			slotArma1.pixelInset = arma1;
		
				
			arma2.width = 36;
			arma2.height = 24;
			slotArma2.pixelInset = arma2;
			
			
		}else if(weaponPos == 1){
			
			arma1.width = 36;
			arma1.height = 24;
			slotArma1.pixelInset = arma1;
			
			
			arma2.width = 40;
			arma2.height = 28;
			slotArma2.pixelInset = arma2;
			
		}

	}
	
	//Cuando muere un enemigo, se actualiza el contador de enemigos
	public void enemyDeath(){
		
		numOfEnem--;
		contadorEnemigos.text=numOfEnem.ToString();
		
		score = score +10;
		
	}
	public int getCurrentTotalScore(){
		
		score = score + robotProtagonista.vida;
		score = score + robotProtagonista.escudo;
		score = score + robotProtagonista.balesCarregador;
		score = score + robotProtagonista.balesTotalsArmaActual;
		
		return score;
	}
}
