using UnityEngine;
using System.Collections;

public class AI2Spawner : MonoBehaviour {
    public Transform target;

    //Noms de les animacions:
	//ajupit
	//actiu
	//caminar
	//disparar
	//melee


    //variables modificables segons la ia--------
	public float vida=40;
    public int moveSpeed=10;
	public int rotationSpeed=1;
    float Distance;

	//----------------------------------
    private int fireRate=5;
	private int directionChangeRate=5;
    private int distancia_alerta=50;
	private int distancia_disparar = 30;
	private float escut =100, max_escut=100;
	private int temps_recarga_escut=2;
	private float regen_escut=20;
	private int armadura=3;
	
    //-------------------------------------------
    
    public Vector3 spawnPoint;
    public string state;
    public float timerAtac,timerEscut,timerDirection;
	RaycastHit hit;
    GameObject hud;
    private Transform myTransform;
	
	
	

    void Awake(){
        myTransform = transform;
        spawnPoint=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        state="sleeping";
    }
	
	
	
    // Use this for initialization
    void Start () {
    	GameObject player = GameObject.FindGameObjectWithTag("Player");
		hud = GameObject.FindGameObjectWithTag("MainCamera");
		
        target = player.transform;
        timerAtac=Time.time;
     }
        
     // Update is called once per frame
     void Update () {
		//regenerar_escut();
       	//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		Distance=Vector3.Distance(target.position,transform.position);
        Debug.DrawRay(transform.position, transform.forward);
		//Debug.DrawLine(target.position, myTransform.position, Color.yellow);
                
        if(Distance>distancia_alerta && Vector3.Distance(spawnPoint, transform.position)>3){
        	state="away";
			//Debug.Log("Enemic inactiu");
			animation.CrossFade("ajupit");
		}else if(Distance<distancia_alerta && Distance>distancia_disparar){
			if(state != "alerta"){
				animation.CrossFade("activar");
			}
			state="alerta";
			//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			/*animation.CrossFade("activar");*/
        }else{
			if(state != "away"){
				animation.CrossFade("desactivar");
			}
			state="away";
			//Debug.Log("Enemic inactiu");
		}
		if(Distance<distancia_disparar){
			//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			state="shooting";
			if(Time.time>timerDirection){
				moveTo();
				timerDirection=Time.time+directionChangeRate;
        	}
            state = "walking";
            spawn_enemy();
        }
	}
        
        
    void moveTo(){
		//Vector3 randomDirection = new Vector3(0,Random.Range(-359, 359),Random.Range(-359, 359));
		//myTransform.Rotate(randomDirection);
	    //myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(randomDirection - myTransform.position), rotationSpeed * Time.deltaTime);
	    //myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }

    private void spawn_enemy(){
        if(Time.time>timerAtac){
				animation.CrossFade("disparar");
				Debug.Log("Spawn!");
				Vector3 temp = myTransform.position;
				temp.x = temp.x+Random.Range(2, 10);
				temp.z = temp.z+Random.Range(2, 10);
				timerAtac=Time.time+fireRate;
				GameObject Spawned_Enemy = (GameObject)Instantiate(Resources.Load("enemy"),temp,myTransform.rotation);
        }
     }
	
	public void rebreDany(int dmg){
		vida-=dmg;
		Debug.Log("Enemigo atacado quedan "+vida+" puntos de vida");
		if(vida<=0){
			Debug.Log("Enemigo muerto");
			hud.SendMessage("enemyDeath");
			Destroy(gameObject);
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

}