using UnityEngine;
using System.Collections;

public class AI2Chaser : MonoBehaviour {
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
	public int rotationSpeed=2;
    float Distance;
    int dist_dmg=15;
	int melee_dmg=2;

	//----------------------------------
    private float fireRate=0.15f;
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
    GameObject hud;
    private Transform myTransform;
	
	//vida
	public GUITexture enemy_Healthbar;
	float maxvida = 0.0f;
	bool inSight,prev_inSight;

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
		
		/*ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
		particlesystem.enableEmission = false;*/
		
		
		maxvida = vida;
		
					
		float percent = 0.0f;
		percent = vida/maxvida;
		percent = percent*100;
		float Size_width = 0.001f;
		float Size_height = 0.010f;
		
		Size_width = percent*Size_width;
		enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
		
		inSight=false;
		prev_inSight=false;
                
     }
        
     // Update is called once per frame
     void Update () {
		if(Vector3.Dot(target.forward, myTransform.position - target.position)>=0) {
			inSight = true;
			//Debug.Log (target.forward.ToString()+" "+myTransform.position.ToString()+" "+target.position.ToString());
		}else{
			inSight = false;	
		}
		if (inSight && !prev_inSight){
			float percent = 0.0f;
			percent = vida/maxvida;
			percent = percent*100;
			float Size_width = 0.0005f;
			float Size_height = 0.0050f;
			
			Size_width = percent*Size_width;
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
			prev_inSight = true;
		}else if(!inSight){
			//Debug.Log ("NOT PAINTING");
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
			prev_inSight = false;
		}
		//regenerar_escut();
       	//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		Distance=Vector3.Distance(target.position,transform.position);
		
		
		Vector3 enemyChest = myTransform.position+Vector3.up*0.8f;
        Debug.DrawRay(enemyChest, transform.forward);
		//Debug.DrawLine(target.position, myTransform.position, Color.yellow);
                
        if((Distance>distancia_melee)){
			moveTo();
            state = "walking";
			animation.CrossFade("caminar");
        }else{
			Vector3 temp = target.position;
			temp.y = 0.0f;
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(temp - enemyChest), rotationSpeed * Time.deltaTime);
	        state="attack";
	        //renderer.material.color=Color.red;
			Debug.Log("Atacant a melee");
	        attack(melee_dmg,false);
        }
	}
        
        
    void moveTo(){
		Vector3 enemyChest = myTransform.position+Vector3.up*0.8f;
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - enemyChest), rotationSpeed * Time.deltaTime);
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
		
		/*if (vida < maxvida*0.5f){
			ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
			particlesystem.enableEmission = true;
		}*/
		
		float percent = 0.0f;
		percent = vida/maxvida;
		percent = percent*100;
		//enemy_Healthbar.guiTexture.pixelInset.Set(enemy_Healthbar.guiTexture.pixelInset.x,enemy_Healthbar.guiTexture.pixelInset.y,percent,enemy_Healthbar.guiTexture.pixelInset.height);
		//Rect temp1 = new Rect(0, 0, percent, 10);
		//enemy_Healthbar.guiTexture.pixelInset=temp1;
		float Size_width = 0.0005f;
		float Size_height = 0.0050f;
		
		Size_width = percent*Size_width;
		enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
		
		
		Debug.Log ("QUEDA UN "+percent+" % DE VIDA");
		Debug.Log("Enemigo atacado quedan "+vida+" puntos de vida");
		if(vida<=0){
			Debug.Log("Enemigo muerto");
			hud.SendMessage("enemyDeath");
			Destroy(gameObject);
		}
	}
	
	private void disparar(int dis,int dmg){
		Vector3 enemyChest = myTransform.position+Vector3.up*0.8f;
		if(Physics.Raycast(transform.position, (target.position- enemyChest), out hit, dis)) {
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