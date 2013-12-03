using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI_escena2 : MonoBehaviour {
    private Transform target;

    //variables modificables segons la ia--------
	public float vida=100;
    private int moveSpeed=8;
	private int rotationSpeed=10;
    float Distance;
	
	//-------------------------
	int patrullar =1;
	string[] points={"Waypoint1","Waypoint2","Waypoint3","Waypoint4"};
	int i=0;
	int activacio=1;
	


	//----------------------------------
	private int numMissils=3;
    private int fireRateMissils=15;
	private int fireRateFoc=10;
	private float fireRateTrail=0.5f;
	private float fireRateMelee=3.0f;
    private float distancia_melee=8.0f;
	private float melee_damage=50.0f;
	private float escut =100, max_escut=100;
	private int temps_recarga_escut=2;
	private float regen_escut=20;
	private int armadura=3;
	
	

    //-------------------------------------------
    
    public Vector3 spawnPoint;
    public string state;
    public float timerAtacMissils,timerAtacFoc,timerAtacTrail,timerAtacMelee;
	RaycastHit hit;
    GameObject enemyCount;
    private Transform myTransform;
	GameObject player;
	//private Vector3 attack_location;

    void Awake(){
        myTransform = transform;
        spawnPoint=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        state="sleeping";
    }
	
	
	
    // Use this for initialization
    void Start () {
    	GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
		//attack_location = target.position;
		
        target = player.transform;
        timerAtacMissils=Time.time+fireRateMissils;
		timerAtacFoc=Time.time+fireRateFoc;
		timerAtacTrail=Time.time+fireRateTrail;
		timerAtacMelee=Time.time+fireRateMelee;
		
		
     }
        
     // Update is called once per frame
    void Update()
    {
		segueix_waypoints();
		missile_attack();
		fire_area_attack();
		trail_attack();
		melee_attack();
    }
	
	private void segueix_waypoints(){
		//animation.CrossFade("caminar");
        GameObject punt = GameObject.Find(points[i]);
        Distance = Vector3.Distance(transform.position, punt.transform.position);
        if (patrullar == 1 || patrullar ==2)
        {
            if (Distance > 1)
            {
              
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(punt.transform.position - transform.position), rotationSpeed * Time.deltaTime);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

            }
            else
            {
                i++;

                if (i >= points.Length)
                {
                    i = 0;
                    if(patrullar==2){
                        Array.Reverse(points);

                	}
            	}

        	}

        }
		
	}
	
	
	private void missile_attack(){
		if(Time.time>timerAtacMissils){
			int i=0;
			for (i = 0;i<numMissils;i++){
				//animation.CrossFade("disparar");
				Debug.Log("Missile!");
				Vector3 temp = myTransform.position;
				int randomNumber = UnityEngine.Random.Range(-10, 10);
				temp.x = temp.x+randomNumber;
				temp.y = temp.y+12.0f;
				randomNumber = UnityEngine.Random.Range(-10, 10);
				temp.z = temp.x+randomNumber;
				GameObject missile = (GameObject)Instantiate(Resources.Load("Homing_missile_1"),temp,myTransform.rotation);
			}
			timerAtacMissils=Time.time+fireRateMissils;
		}
	}
	
	private void fire_area_attack(){
		if(Time.time>timerAtacFoc){
			Debug.Log("Foc!");
			Vector3 attack_location = target.position+(target.forward*5.0F);
			/*int randomNumber = UnityEngine.Random.Range(-2, 2);
			attack_location.x = attack_location.x+randomNumber;
			attack_location.y = attack_location.y+3.0f;
			randomNumber = UnityEngine.Random.Range(-2, 2);
			attack_location.z = attack_location.z+randomNumber;*/
			GameObject foc = (GameObject)Instantiate(Resources.Load("boss_area_fire"),attack_location,myTransform.rotation);
			timerAtacFoc=Time.time+fireRateFoc;
		}
	
	}
	
	
	private void trail_attack(){
		if(Time.time>timerAtacTrail){
			Debug.Log("Trail!");
			Vector3 attack_location = myTransform.position-(myTransform.forward*8.0F);
			attack_location.y = attack_location.y+2.0f;
			/*int randomNumber = UnityEngine.Random.Range(-2, 2);
			attack_location.x = attack_location.x+randomNumber;
			attack_location.y = attack_location.y+3.0f;
			randomNumber = UnityEngine.Random.Range(-2, 2);
			attack_location.z = attack_location.z+randomNumber;*/
			GameObject acid = (GameObject)Instantiate(Resources.Load("boss_trail"),attack_location,myTransform.rotation);
			timerAtacTrail=Time.time+fireRateTrail;
		}
	
	}
	
	private void melee_attack(){
		if(Time.time>timerAtacMelee){
			float player_distance=Vector3.Distance(target.position,transform.position);
			if(player_distance<distancia_melee){
				patrullar = 0;
				Invoke ("set_patrullar_on", 2);
				GameObject temp_player = GameObject.FindGameObjectWithTag("Player");
				Debug.Log("Melee!");
				temp_player.SendMessage("rebreAtac", melee_damage);
				timerAtacMelee=Time.time+fireRateMelee;
			}
		}
	
	}
	
	private void set_patrullar_on(){
		patrullar = 1;	
	}
	
    void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	    //float mov= myTransform.position.y -(gravetat * Time.deltaTime);
	    //myTransform.position.y =mov;
	    //myTransform.position=new Vector3(myTransform.position.x,mov,myTransform.position.z);
    }

    /*private void attack(int dmg,bool ranged){
        if(Time.time>timerAtac){
			if(ranged){
				Debug.Log("Shooting");
				animation.CrossFade("disparar");
				disparar(distancia_disparar,dist_dmg);
			}else{
				Debug.Log("Melee");
				animation.CrossFade("melee");
				disparar(distancia_disparar,melee_dmg);
			}
            timerAtac=Time.time+fireRate;
        }
     }*/
	
	public void rebreDany(int dmg){
		vida-=dmg;
		Debug.Log("Enemigo atacado quedan "+vida+" puntos de vida");
		if(vida<=0){
			Debug.Log("Enemigo muerto");
			//enemyCount.SendMessage("enemyDeath");
			Destroy(gameObject);
		}
	}
	
	private void disparar(int dis,int dmg){
		if(Physics.Raycast(transform.position, (target.position- transform.position), out hit, dis)) {
			Debug.DrawLine(target.position, transform.position, Color.green);
			Debug.DrawRay(transform.position, transform.forward,Color.blue);
			//print (hit.collider.gameObject.tag);
			if(hit.collider.gameObject.tag == "Player") {
				Debug.Log("ataco al player i li faig "+dmg+" punts de dany");
				hit.transform.gameObject.SendMessage("rebreAtac",dmg);
			}
		}
	}
	
	private float reduir_mal(int dmg){
		float v2;
		if(escut==0){
				v2=vida-dmg;
		}else if(dmg<escut){
			escut-=dmg;
			v2=vida;
		}else{
			v2=vida+(escut-dmg);
			escut=0;
			
		}

		return v2;

	}
	
	public void activar(){
		activacio=1;
		
	}
	


}