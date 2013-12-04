using UnityEngine;
using System.Collections;



public class Homing_missile : MonoBehaviour {
	public Transform target;
	private Vector3 attack_location;
	float Distance;
    private int moveSpeed=13;
	private int rotationSpeed=20;
	private int damage = 15;
	RaycastHit hit;
	private Transform myTransform;
	private bool destroyed = false;
	public GameObject bomba;
	public GameObject smoke_trail;
	
	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,15.0f);
		myTransform = transform;
		GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
		attack_location = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		Distance=Vector3.Distance(attack_location,transform.position);
		Debug.DrawRay(transform.position, transform.forward);
		//GameObject player = GameObject.FindGameObjectWithTag("Player");
        //target = player.transform;
		
		if(!destroyed){
			moveTo();
			if(Distance<1){
			Debug.Log("DISPARAR");
				disparar(4.0f,damage);
			}
		}
		
	}
	
	void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(attack_location - myTransform.position), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }
	
	private void disparar(float distancia,int dmg){
		//Component halo = GetComponent("Halo");
		RaycastHit[] hits;
		//GameObject obj = gameObject.transform.Find("bombaNpc").gameObject;
		//obj.activeSelf = false;
		//halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
		//Destroy (bomba);
		//Destroy (smoke_trail);
		GameObject Explosion = (GameObject)Instantiate(Resources.Load("Homing_explosion"),myTransform.position,myTransform.rotation);
		hits = Physics.RaycastAll (transform.position, (target.position - transform.position), distancia);; 
	    int i = 0;
        while (i < hits.Length){
			Debug.Log("Missil ha tocat a: "+hits[i].collider.gameObject.tag);
			if(hits[i].collider.gameObject.tag == "Player") {
				Debug.Log("Missil ha fet "+dmg+" punts de dany");
				hits[i].transform.gameObject.SendMessage("rebreAtac",dmg);
				break;
			}
			i++;
		}
		
		destroyed = true;
		//Destroy(this.gameObject,1.0f);
		Destroy(gameObject);
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		disparar(4.0f,damage);
	}
	
	
}
