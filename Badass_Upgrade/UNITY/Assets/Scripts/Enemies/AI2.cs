using UnityEngine;
using System.Collections;

public class AI2 : MonoBehaviour {
    public Transform target;

	int gravetat=10;

	//variables modificables segons la ia--------
	public int moveSpeed=3;
    public int rotationSpeed=2;
	float Distance;
	float shootDistance;
	int damage=20;
	public int fireRate=1;
	public int distancia_alerta=20;
	public int distancia_perseguir=10;
	public int distancia_atac_fisic=3;
    public int distancia_atac_distancia = 15;
	//-------------------------------------------
	
	public Vector3 spawnPoint;
	public string state;
	public float lastAttack;
	
    private Transform myTransform;

    void Awake(){
        myTransform = transform;
		spawnPoint=new Vector3(transform.position.x,transform.position.y,transform.position.z);
		state="sleeping";
    }

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
		lastAttack=Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		
		Distance=Vector3.Distance(target.position,transform.position);
        Debug.DrawLine(target.position, myTransform.position, Color.yellow);
		
		if(Distance>distancia_alerta && Vector3.Distance(spawnPoint, transform.position)>3){
			state="away";
			//renderer.material.color=Color.blue;
			//retorn al spawnpoint?
		}else if(Distance<distancia_alerta && Distance<=distancia_perseguir && Distance>distancia_atac_fisic){
            moveTo();
            state = "walking";
            animation.Play("walk");
        }else if(Distance <=distancia_atac_fisic){
			state="attack";
			//renderer.material.color=Color.red;
			attack();
			animation.Play("meleeattack");
		}else if(Distance<distancia_alerta ){
			//print("Distance shoot or follow: "+Distance+" -- "+distancia_atac_distancia);
			state="shooting";
            animation.Play("crouch");
            distanceAttack();
			//renderer.material.color=Color.yellow;
		}
	}
	
	
	void moveTo(){
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		//float mov= myTransform.position.y -(gravetat * Time.deltaTime);
		//myTransform.position.y =mov;
		//myTransform.position=new Vector3(myTransform.position.x,mov,myTransform.position.z);
	}

	void attack(){
		if(Time.time>lastAttack){
			print("attack!");
			lastAttack=Time.time+fireRate;
		}
	}
	

    void distanceAttack(){
        if(Time.time>lastAttack){
            print ("Shooting");
            lastAttack=Time.time+fireRate;
        }
     }
	/*
	void distanceAttack(){
		RaycastHit hitt;
		Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),hitt);
			

		if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),hitt)){
			shootDistance=hitt.distance;
			hitt.transform.SendMessage("dealDamage",damage,SendMessageOptions.DontRequireReceiver);
			
		}
	}
	*/
}