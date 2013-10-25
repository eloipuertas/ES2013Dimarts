using UnityEngine;
using System.Collections;

public class downAndAtack : MonoBehaviour {
	
	//down
	public GameObject player;
	public GameObject leftHand;
	float minScaleY = 0.2f;
	float maxScaleY = 1.0f;
	
	//Per eliminar el braç
	float scaleHandx = 0;
	float scaleHandy = 0;
	float scaleHandz = 0;
	
	//Per saber si esta ajupit
	bool down;
	
	//melee
	RaycastHit hit;
	float meleeDistance = 1.8f;
	Transform cam;
	int damageMelee = 10;
	
	// Use this for initialization
	void Start () {
		/*scaleHandx = leftHand.transform.localScale.x;
		scaleHandy = leftHand.transform.localScale.y;
		scaleHandz = leftHand.transform.localScale.z;*/
		
		leftHand.SetActive(false);
		
		cam = Camera.main.transform;
		
		down = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Input.GetButtonDown("Agacharse")) {
			down = true;
			if(player.transform.localScale.y > minScaleY) {
				player.transform.localScale -= new Vector3(0, minScaleY, 0);
				//leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
				
				//Per eliminar braç quant s'ajup
				//leftHand.SetActive(false);
				//leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
			}
				
		}
		else if(Input.GetButtonUp("Agacharse")) {
			down = false;
			if(player.transform.localScale.y < maxScaleY) {
				player.transform.position += new Vector3(0, minScaleY, 0);
				player.transform.localScale += new Vector3(0, minScaleY, 0);
				//leftHand.transform.localScale -= new Vector3(scaleHandx,scaleHandy,scaleHandz);
				
				//Tornar a posar braç
				//leftHand.SetActive(true);
				//leftHand.transform.localScale += new Vector3(scaleHandx,scaleHandy,scaleHandz);
			}
				
		}
		
		if((Input.GetButtonDown("Melee")) && (down == false)) {
			leftHand.SetActive(true);
			leftHand.animation.Play("ArmatureAction");
			if(Physics.Raycast(cam.position, cam.forward,out hit, meleeDistance)) {
				if(hit.collider.gameObject.tag == "Enemy") {
					//Enviar el dany directament
					hit.transform.gameObject.SendMessage("rebreDany",damageMelee);
				}
			}
		}
	}
}
