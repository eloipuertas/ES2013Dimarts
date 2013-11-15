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
	
	private void disparar(int dis,int dmg){
		if(Physics.Raycast(transform.position, (target.position- transform.position), out hit, dis)) {
			//Debug.DrawLine(target.position, transform.position, Color.green);
			//Debug.DrawRay(transform.position, transform.forward,Color.blue);
			//print (hit.collider.gameObject.tag);
			if(hit.collider.gameObject.tag == "Player") {
				Debug.Log("Missil ha fet "+dmg+" punts de dany");
				hit.transform.gameObject.SendMessage("rebreAtac",dmg);
			}
			if(hit.collider.gameObject.tag != "Enemy") {
				Destroy(gameObject);
			}
		}
		
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		disparar(2,damage);
	}
	
	
}
