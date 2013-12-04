using UnityEngine;
using System.Collections;



public class Homing_missile : MonoBehaviour {
	public Transform target;
	private Vector3 attack_location;
	float Distance;
    public int moveSpeed=8;
	public int rotationSpeed=2;
	private int damage = 10;
	RaycastHit hit;
	private Transform myTransform;
	private bool destroyed = false;
	public float timerDestroyed = 0.0f;
	
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
		
		if(destroyed){
			if(timerDestroyed+1.0f < Time.time){
				Destroy(gameObject);
			}
		}else{
			moveTo();
			if(Distance<1){
				disparar(4.0f,damage);
			}
		}
		
	}
	
	void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(attack_location - myTransform.position), rotationSpeed * Time.deltaTime);
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
		Component halo = GetComponent("Halo"); 		
		RaycastHit[] hits;
		if(!destroyed){
			GameObject obj = gameObject.transform.Find("bombaNpc").gameObject;
			Destroy (obj);
			GameObject Explosion = (GameObject)Instantiate(Resources.Load("Homing_explosion"),myTransform.position,myTransform.rotation);
			hits = Physics.RaycastAll (transform.position, (target.position - transform.position), distancia);; 
		    int i = 0;
	        while (i < hits.Length) {
				Debug.Log("Tocat a: "+hits[i].collider.gameObject.tag);
				if(hits[i].collider.gameObject.tag == "Player") {
					Debug.Log("Missil ha fet "+dmg+" punts de dany");
					hits[i].transform.gameObject.SendMessage("rebreAtac",dmg);
					halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
					destroyed = true;
					timerDestroyed = Time.time;
					//Destroy(gameObject);
					break;
				}
				i++;
			}
			destroyed = true;
			timerDestroyed = Time.time;
			halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
			//Destroy(gameObject);
		}
	}
	
	private void OnCollisionEnter(Collision collision)
	{
		disparar(4.0f,damage);
	}
	
	
}
