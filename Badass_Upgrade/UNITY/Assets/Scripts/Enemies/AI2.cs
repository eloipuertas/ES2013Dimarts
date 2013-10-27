using UnityEngine;
using System.Collections;

public class AI2 : MonoBehaviour {
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
    int dist_dmg=20;
	int melee_dmg=25;

	//----------------------------------
    private int fireRate=2;
    private int distancia_alerta=20;
    private int distancia_perseguir=6;
    private int distancia_melee=3;
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

    void Awake(){
        myTransform = transform;
        spawnPoint=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        state="sleeping";
    }
	
	
	
    // Use this for initialization
    void Start () {
    	GameObject player = GameObject.FindGameObjectWithTag("Player");
		enemyCount = GameObject.FindGameObjectWithTag("enemyCount");
		
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
			Debug.Log("Enemic inactiu");
			animation.CrossFade("ajupit");
            //renderer.material.color=Color.blue;
            //retorn al spawnpoint?
		}else if(Distance<distancia_alerta && Distance>distancia_disparar){
			if(state != "alerta"){
				animation.CrossFade("activar");
			}
			state="alerta";
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			/*animation.CrossFade("activar");*/
				
        }else if(Distance<distancia_disparar && Distance>distancia_perseguir){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			state="shooting";
            
            attack(dist_dmg);
        }else if((Distance <=distancia_perseguir) && (Distance>distancia_melee)){
			moveTo();
            state = "walking";
			animation.CrossFade("caminar");
        }else if(Distance<distancia_melee ){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	        state="attack";
	        //renderer.material.color=Color.red;
			Debug.Log("Atacant a melee");
	        attack(melee_dmg);
	        animation.CrossFade("melee");
        }else{
			if(state != "away"){
				animation.CrossFade("desactivar");
			}
			state="away";
			Debug.Log("Enemic inactiu");
		}
	}
        
        
    void moveTo(){
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	    //float mov= myTransform.position.y -(gravetat * Time.deltaTime);
	    //myTransform.position.y =mov;
	    //myTransform.position=new Vector3(myTransform.position.x,mov,myTransform.position.z);
    }


        

    private void attack(int dmg){
        if(Time.time>timerAtac){
            print ("Shooting");
			animation.CrossFade("disparar");
			disparar(distancia_disparar,dist_dmg);
            timerAtac=Time.time+fireRate;
        }
     }
	
	
	public void rebreDany(int dmg){
		vida-=dmg;
		if(vida<=0){
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

}