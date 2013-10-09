using UnityEngine;
using System.Collections;

public class Playerdown : MonoBehaviour {
	
	public GameObject player;
	float minScaleY = 0.5f;
	float maxScaleY = 1.0f;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		/*if(Input.GetAxis("Down") > 0) {
			//player.transform.localScale.y += 0.5f;
			player.transform.localScale += new Vector3(0, 0.5f, 0);
			Debug.Log("down");
		}*/
		
		
		if(Input.GetKeyDown("q")) {
			if(player.transform.localScale.y > minScaleY) {
				player.transform.localScale -= new Vector3(0, minScaleY, 0);		
			}
			else {
				player.transform.localScale -= new Vector3(0, minScaleY, 0);	
			}
			
		}
		else if(Input.GetKeyUp("q")) {
			if(player.transform.localScale.y < maxScaleY) {
				player.transform.localScale += new Vector3(0, minScaleY, 0);	
			}
				
		}
	}
}
