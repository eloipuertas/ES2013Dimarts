using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI_escena2 : MonoBehaviour {
    public Transform target;

    //Noms de les animacions:
	//ajupit
	//actiu
	//caminar
	//disparar
	//melee


    //variables modificables segons la ia--------
	public float vida=100;
    public int moveSpeed=3;
	public int rotationSpeed=2;
    float Distance;
    int dist_dmg=15;
	int melee_dmg=25;
	
	//-------------------------
	int patrullar =1;
	string[] points={"Waypoint1","Waypoint2","Waypoint3"};
	int i=0;
	int activacio=1;
	


	//----------------------------------
    private int fireRate=2;
    private int distancia_alerta=20;
    private int distancia_perseguir=6;
    private int distancia_melee=2;
	private int distancia_disparar = 15;
	private float escut =100, max_escut=100;
	private int temps_recarga_escut=2;
	private float regen_escut=20;
	private int armadura=3;
	
	

    //-------------------------------------------
    
    public Vector3 spawnPoint;
    public string state;
    public float timerAtac,timerEscut;
	RaycastHit hit;
    GameObject enemyCount;
    private Transform myTransform;
	GameObject player;

    void Awake(){
        myTransform = transform;
        spawnPoint=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        state="sleeping";
    }
	
	
	
    // Use this for initialization
    void Start () {
    	player = GameObject.FindGameObjectWithTag("Player");
		enemyCount = GameObject.FindGameObjectWithTag("enemiesCount");
		
        target = player.transform;
        timerAtac=Time.time;
		
		
     }
        
     // Update is called once per frame
    void Update()
    {
			segueix_waypoints();
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
	
	
	
	
	
	
    void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	    //float mov= myTransform.position.y -(gravetat * Time.deltaTime);
	    //myTransform.position.y =mov;
	    //myTransform.position=new Vector3(myTransform.position.x,mov,myTransform.position.z);
    }

    private void attack(int dmg,bool ranged){
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
     }
	
	public void rebreDany(int dmg){
		vida-=dmg;
		Debug.Log("Enemigo atacado quedan "+vida+" puntos de vida");
		if(vida<=0){
			Debug.Log("Enemigo muerto");
			enemyCount.SendMessage("enemyDeath");
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
	
	private void regenerar_escut(){
		if(Time.time>timerEscut && escut<max_escut){
			escut+=max_escut *(regen_escut/100);
			if(escut>max_escut){
					escut=max_escut;
			}
			
			timerEscut=Time.time+temps_recarga_escut;
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