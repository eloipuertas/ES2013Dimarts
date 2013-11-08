using UnityEngine;
using System.Collections;

public class EnemyCount : MonoBehaviour {
	
	//Aquest script s'assigna al guiText enemycount del hud!!
	
	int enemies;
	
	void Awake(){
		GameObject[] objs;
		objs=GameObject.FindGameObjectsWithTag("Enemy");
		enemies=objs.Length;
		guiText.text="Enemies :"+enemies;
	}
	
	
	public void enemyDeath(){
		enemies--;
		guiText.text="Enemies :"+enemies;
		Debug.Log ("Quedan "+enemies+" Enemigos");
		if(enemies<=0){
			Debug.Log ("No Quedan Enemigos");
			GameObject p=GameObject.FindGameObjectWithTag("porta1");
			//p.SendMessage("setNivel_Completado",true,SendMessageOptions.DontRequireReceiver);
			Debug.Log("porta oberta");
		}
			
			
	}
	
	
	public void setEnemies(int num){
		
		enemies=num;
	}
	
}
