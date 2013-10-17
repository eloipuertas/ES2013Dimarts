using UnityEngine;
using System.Collections;

public class Playerdown : MonoBehaviour {
	
	public GameObject player;
	public GameObject leftHand;
	float minScaleY = 0.2f;
	float maxScaleY = 1.0f;
	
	//float scaleHandx = 30f;
	//float scaleHandy = -10f;
	//float scaleHandz = -5f;
	
	//Per eliminar el braç
	float scaleHandx = 0;
	float scaleHandy = 0;
	float scaleHandz = 0;
	
	
	// Use this for initialization
	void Start () {
		scaleHandx = leftHand.transform.localScale.x;
		scaleHandy = leftHand.transform.localScale.y;
		scaleHandz = leftHand.transform.localScale.z;	
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Input.GetButtonDown("Agacharse")) {
			if(player.transform.localScale.y > minScaleY) {
				player.transform.localScale -= new Vector3(0, minScaleY, 0);
				//leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
				
				//Per eliminar braç quant s'ajup
				leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
			}
			
			
		}
		else if(Input.GetButtonUp("Agacharse")) {
			if(player.transform.localScale.y < maxScaleY) {
				player.transform.position += new Vector3(0, minScaleY, 0);
				player.transform.localScale += new Vector3(0, minScaleY, 0);
				//leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
				
				//Tornar a posar braç
				leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
			}
				
		}
	}
}
