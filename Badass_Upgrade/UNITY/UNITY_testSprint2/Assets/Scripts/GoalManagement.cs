using UnityEngine;
using System.Collections;

public class GoalManagement : MonoBehaviour {
	
	private bool success = false;
	public Texture textureWin;
	
	//Showing the final message of WIN
	private void gameSuccess(){
		success = true;
		Time.timeScale = 0;
	}
	
	void Update(){
		if (success){
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.LoadLevel(0);
		}
	}
	
	void OnGUI(){
		if(success){ 
			GUI.DrawTexture(new Rect(Screen.width/2.5F,Screen.height/2.5F,300,300),textureWin);
		}
	}
	
	// Trigger event
	void OnTriggerEnter(Collider other) {
        if(other.CompareTag("goal_ball")){ 
			gameSuccess();
		}
    }
}
