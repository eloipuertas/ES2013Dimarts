using UnityEngine;
using System.Collections;



public class Homing_missile : MonoBehaviour {
	public Transform target;
	float Distance;
    public int moveSpeed=8;
	public int rotationSpeed=2;
	private int damage = 25;
	RaycastHit hit;
	private Transform myTransform;
	// Use this for initialization
	void Start () {
		myTransform = transform;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Distance=Vector3.Distance(target.position,transform.position);
		Debug.DrawRay(transform.position, transform.forward);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
		moveTo();
		if(Distance<1){
			disparar(2,damage);
		}
	}
	
	void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }
	
	/*private void disparar(int distancia,int dmg){
		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, (target.position- transform.position), distancia);; 
	    int i = 0;
        while (i < hits.Length) {
            RaycastHit hit = hits[i];
			Debug.Log (hits[i]);
	        if (hits[i].collider.tag == "Player"){
				Debug.Log("ataco al player i li faig "+dmg+" punts de dany");
				hit.transform.gameObject.SendMessage("rebreAtac",dmg);
				break;
			}else if(hits[i].collider.tag == null){
				break;
			}
			i++;
	    }
	}*/
	
	
	private void disparar(float distancia,int dmg){
		RaycastHit[] hits;

		hits = Physics.RaycastAll (transform.position, (target.position - transform.position), distancia);; 
	    int i = 0;
        while (i < hits.Length) {
			Debug.Log("Tocat a: "+hits[i].collider.gameObject.tag);
			if(hits[i].collider.gameObject.tag == "Player") {
				Debug.Log("Missil ha fet "+dmg+" punts de dany");
				hits[i].transform.gameObject.SendMessage("rebreAtac",dmg);
				Destroy(gameObject);
				break;
			}
			i++;
		}
		//Destroy(gameObject);
	}
	/*private void disparar(float distancia,int dmg){
		if(Physics.Raycast(transform.position, (target.position- transform.position), out hit, distancia)) {
			if(hit.collider.gameObject.tag == "Player") {
				Debug.Log("Missil ha fet "+dmg+" punts de dany");
				hit.transform.gameObject.SendMessage("rebreAtac",dmg);
			}
			if(hit.collider.gameObject.tag != "Enemy") {
				Destroy(gameObject);
			}
		}
		
	}*/
	
	private void OnCollisionEnter(Collision collision)
	{
		disparar(4.0f,damage);
	}
	
	
}
