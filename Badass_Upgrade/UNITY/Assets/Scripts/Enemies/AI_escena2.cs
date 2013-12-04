using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AI_escena2 : MonoBehaviour {
    private Transform target;

    //variables modificables segons la ia--------
	private float vida=100;
    private int moveSpeed=6;
	private int rotationSpeed=10;
    float Distance;
	
	//-------------------------
	int patrullar =1;
	string[] points={"Waypoint1","Waypoint2","Waypoint3","Waypoint4","Waypoint5","Waypoint6"};
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
    
    private Vector3 spawnPoint;
    private string state;
    private float timerAtacMissils,timerAtacFoc,timerAtacTrail,timerAtacMelee;
	RaycastHit hit;
    GameObject enemyCount;
    private Transform myTransform;
	GameObject player;
	//private Vector3 attack_location;
	
	public GUITexture enemy_Healthbar;
	private float maxvida = 0.0f;
	private	float timerShot;
	private bool inSight,prev_inSight,recently_shot,isShowingLaser;
	
	GameObject hud;

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
		
		maxvida = vida;
		float percent = 0.0f;
		percent = vida/maxvida;
		percent = percent*100;
		float Size_width = 0.0005f;
		float Size_height = 0.0050f;
		Size_width = percent*Size_width;
		enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,3);
		inSight=false;
		prev_inSight=false;
		timerShot = Time.time;
		recently_shot = false;
		
		hud = GameObject.FindGameObjectWithTag("HUD Camera");
		hud.SendMessage("addEnemy");
		
     }
        
     // Update is called once per frame
    void Update()
    {
		if(Vector3.Dot(target.forward, myTransform.position - target.position)>=0) {
			inSight = true;
			//Debug.Log (target.forward.ToString()+" "+myTransform.position.ToString()+" "+target.position.ToString());
		}else{
			inSight = false;	
		}
		
		if(timerShot+3.0f < Time.time){
			recently_shot = false;
		}
		
		if (inSight && !prev_inSight && recently_shot){
			float percent = 0.0f;
			percent = vida/maxvida;
			percent = percent*100;
			float Size_width = 0.0005f;
			float Size_height = 0.0050f;
			
			Size_width = percent*Size_width;
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,3);
			prev_inSight = true;
		}else if(!inSight || !recently_shot){
			//Debug.Log ("NOT PAINTING");
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
			prev_inSight = false;
		}
		
		
		float player_distance=Vector3.Distance(target.position,transform.position);
		if(player_distance>distancia_melee){
			patrullar = 1;	
			trail_attack();
		}else{
			melee_attack();
		}
		segueix_waypoints();
		
		if(player_distance<50.0f){
			missile_attack();
			fire_area_attack();
		}
		
		
		
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
			GameObject foc = (GameObject)Instantiate(Resources.Load("boss_area_fire"),attack_location,myTransform.rotation);
			timerAtacFoc=Time.time+fireRateFoc;
		}
	
	}
	
	
	private void trail_attack(){
		if(Time.time>timerAtacTrail){
			Debug.Log("Trail!");
			Vector3 attack_location = myTransform.position-(myTransform.forward*8.0F);
			attack_location.y = attack_location.y+0.5f;
			GameObject acid = (GameObject)Instantiate(Resources.Load("boss_trail"),attack_location,myTransform.rotation);
			timerAtacTrail=Time.time+fireRateTrail;
		}
	
	}
	
	private void melee_attack(){
		patrullar = 0;
		if(Time.time>timerAtacMelee){
				GameObject temp_player = GameObject.FindGameObjectWithTag("Player");
				Debug.Log("Melee!");
				temp_player.SendMessage("rebreAtac", melee_damage);
				timerAtacMelee=Time.time+fireRateMelee;
		}
	}
	
	private void set_patrullar_on(){
		patrullar = 1;	
	}
	
    void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }

	public void rebreDany(int dmg){
		vida-=dmg;
		
		recently_shot = true;
		timerShot = Time.time;
		
		float percent = 0.0f;
		percent = vida/maxvida;
		percent = percent*100;
		float Size_width = 0.0005f;
		float Size_height = 0.0050f;
		Size_width = percent*Size_width;
		enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,3);

		
		
		Debug.Log("Boss atacado quedan "+vida+" puntos de vida.");
		Debug.Log ("Boss:"+percent+"%");
		if(percent <= 50.0f){
			Vector3 temp = myTransform.position;
			temp.x = 0.0f;
			temp.y = 5.5f;
			temp.z = 3.5f;
			GameObject fire = (GameObject)Instantiate(Resources.Load("Boss_damaged_fire"),myTransform.position,myTransform.rotation);
			fire.transform.parent = myTransform;
			//fire.transform.position = myTransform.position;
			fire.transform.localPosition = temp;
		}else if(vida<=0){
			Debug.Log("Enemigo muerto");
			GameObject Explosion = (GameObject)Instantiate(Resources.Load("Homing_explosion"),myTransform.position,myTransform.rotation);
			hud.SendMessage("enemyDeath");
			Destroy(gameObject);
		}
	}
	
	private void disparar(int dis,int dmg){
		if(Physics.Raycast(transform.position, (target.position- transform.position), out hit, dis)) {
			Debug.DrawLine(target.position, transform.position, Color.green);
			Debug.DrawRay(transform.position, transform.forward,Color.blue);
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