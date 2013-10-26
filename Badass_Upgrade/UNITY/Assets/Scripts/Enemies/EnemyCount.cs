using UnityEngine;
using System.Collections;

public class EnemyCount : MonoBehaviour {
	
	//Aquest script s'assigna al guiText enemycount del hud!!
	
	int enemies;
	
	void Awake(){
		GameObject[] objs;
		objs=GameObject.FindGameObjectsWithTag("Enemy");
		enemies=objs.Length;
		print("there are "+enemies);
		guiText.text="Enemies :"+enemies;
		
		
	}
	
	
	public void enemyDeath(){
		enemies--;
		guiText.text="Enemies :"+enemies;
		if(enemies<=0){
			GameObject p=GameObject.FindGameObjectWithTag("porta1");
			p.SendMessage("obrirPorta",0,SendMessageOptions.DontRequireReceiver);
			print ("porta oberta");	
		}
			
			
	}
	
	
	public void setEnemies(int num){
		
		enemies=num;
	}
	
}
