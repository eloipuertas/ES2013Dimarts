using UnityEngine;
using System.Collections;

public class ButtonActivation : MonoBehaviour {
	
	public bool isButton1 = false;			//Is the button the quit button?
	public bool isButton2 = false;
	public bool isButton3 = false;
	public bool isButton4 = false;
	
	private static int buttonCounter = 1;
	
	
	// Use this for initialization
	void Start () {
		renderer.material.color = Color.green;
	}
	
	
	void processButton(){
		if(isButton1 && buttonCounter == 1){
			renderer.material.color = Color.red;
			buttonCounter++;
			Debug.Log("Button 1 Activated - OK!");
		}
		else if (isButton2 && buttonCounter == 2){ 
			renderer.material.color = Color.red;
			buttonCounter++;
			Debug.Log("Button 2 Activated - OK!");
		}
		else if (isButton3 && buttonCounter == 3){
			renderer.material.color = Color.red;
			buttonCounter++;
			Debug.Log("Button 3 Activated - OK!");
		}
		else if (isButton4 && buttonCounter == 4){
			renderer.material.color = Color.red;
			Debug.Log("Button 4 Activated - OK!");
			succes();
		}
		else {
			//restart the combination
			Debug.Log("I'm in the else statement! && buttonCounter = " +buttonCounter);
			buttonCounter = 1;
			GameObject [] buttons = GameObject.FindGameObjectsWithTag("button");
			foreach(GameObject b in buttons){
				b.renderer.material.color = Color.green;
			}
		}
			
	}
	
	void succes(){
		//Destroy the fire wall & the blocking wall
		Destroy(GameObject.FindGameObjectWithTag("wall_fire"));		
		Destroy(GameObject.FindGameObjectWithTag("wall_block"));
		Debug.Log ("Middle wall cleared! You can go ahead!");
	}
	
	
	// Trigger event
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){ 
			processButton();
		}
    }
}
