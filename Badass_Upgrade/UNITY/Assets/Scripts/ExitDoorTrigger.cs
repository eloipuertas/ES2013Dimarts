using UnityEngine;
using System.Collections;

public class ExitDoorTrigger : MonoBehaviour {
	
	public EnemiesAmount enemiesCounter;
	private int numberOfEnemies;

	// Use this for initialization
	void Start () {
		
		numberOfEnemies = enemiesCounter.numOfEnem;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		numberOfEnemies = enemiesCounter.numOfEnem;
		if(numberOfEnemies<=0){
			collider.isTrigger = true;
		}
		
	}
}
