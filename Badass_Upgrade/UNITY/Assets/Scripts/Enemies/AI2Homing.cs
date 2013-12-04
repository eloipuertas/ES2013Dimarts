using UnityEngine;
using System.Collections;

public class AI2Homing : MonoBehaviour {
    public Transform target;

    //Noms de les animacions:
	//ajupit
	//actiu
	//caminar
	//disparar
	//melee


    //variables modificables segons la ia--------
	public float vida=40;
    public int moveSpeed=3;
	public int rotationSpeed=2;
    float Distance;
    int dist_dmg=15;
	int melee_dmg=25;

	//----------------------------------
    private int fireRate=3;
    private int distancia_alerta=35;
    private int distancia_perseguir=6;
    private int distancia_melee=2;
	private int distancia_disparar = 25;
	private float escut =100, max_escut=100;
	private int temps_recarga_escut=2;
	private float regen_escut=20;
	private int armadura=3;
	private bool unhit =true;
	
    //-------------------------------------------
    
    public Vector3 spawnPoint;
    public string state;
    public float timerAtac,timerEscut;
	RaycastHit hit;
    GameObject hud;
    private Transform myTransform;
	
	
	
	public GameObject Homing_missile;
	GameObject shield;
	//vida
	public GUITexture enemy_Healthbar;
	float maxvida = 0.0f;
	float timerShot;
	bool inSight,prev_inSight,recently_shot;
	
	
	
    void Awake(){
        myTransform = transform;
        spawnPoint=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        state="sleeping";
    }
	
	
	
    // Use this for initialization
    void Start () {
    	GameObject player = GameObject.FindGameObjectWithTag("Player");
		hud = GameObject.FindGameObjectWithTag("HUD Camera");
		
        target = player.transform;
        timerAtac=Time.time;
		
		ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
		particlesystem.enableEmission = false;
		
		
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
		
		timerShot = Time.time;
		recently_shot = false;
		
		hud.SendMessage("addEnemy");
     }
        
     // Update is called once per frame
     void Update () {
		
		if(Vector3.Dot(target.forward, myTransform.position - target.position)>=0) {
			inSight = true;
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
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
			prev_inSight = true;
		}else if(!inSight || !recently_shot){
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
			prev_inSight = false;
		}
		Distance=Vector3.Distance(target.position,transform.position);
		
		
		Vector3 enemyChest = myTransform.position+Vector3.up*1.6f;
        Debug.DrawRay(enemyChest, transform.forward);
		if(Distance<distancia_alerta && Distance>distancia_disparar){
			if(state != "alerta"){
				animation.Play("activar");
				Destroy (shield);
			}
			state="alerta";
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - enemyChest), rotationSpeed * Time.deltaTime);
				
        }else if(Distance<distancia_disparar){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - enemyChest), rotationSpeed * Time.deltaTime);
			state="shooting";
            attack(dist_dmg,true);
        }else{
			if(state != "away"){
				animation.Play("desactivar");
				shield = (GameObject)Instantiate(Resources.Load("Enemy_Shield"),myTransform.position,myTransform.rotation);
			}
			state="away";
		}
	}
        
        
    void moveTo(){
		Vector3 enemyChest = myTransform.position+Vector3.up*1.6f;
	    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - enemyChest), rotationSpeed * Time.deltaTime);
	    myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }

    private void attack(int dmg,bool ranged){
        if(Time.time>timerAtac){
			if(ranged){
				animation.CrossFade("disparar");
				Debug.Log("Missile!");
				Vector3 temp = myTransform.position;
				temp.y = temp.y+4.0f;
				timerAtac=Time.time+fireRate;
				GameObject missile = (GameObject)Instantiate(Resources.Load("Homing_missile_1"),temp,myTransform.rotation);
			}else{
				Debug.Log("Melee");
				animation.CrossFade("melee");
				disparar(distancia_disparar,melee_dmg);
			}
            timerAtac=Time.time+fireRate;
        }
     }
	
	public void rebreDany(int dmg){
		if (state != "away" || !unhit){
			vida-=dmg;
			unhit=false;
			distancia_disparar=100;
			recently_shot = true;
			timerShot = Time.time;
			
			if (vida < maxvida*0.5f){
				ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
				particlesystem.enableEmission = true;
			}
			
			float percent = 0.0f;
			percent = vida/maxvida;
			percent = percent*100;
			float Size_width = 0.0005f;
			float Size_height = 0.0050f;
			
			Size_width = percent*Size_width;
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
			
			
			Debug.Log ("QUEDA UN "+percent+" % DE VIDA");
			Debug.Log("Enemigo atacado quedan "+vida+" puntos de vida");
			if(vida<=0){
				Debug.Log("Enemigo muerto");
				hud.SendMessage("enemyDeath");
				drop();
				Destroy(gameObject);
			}
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
	
	private void regenerar_escut(){
		if(Time.time>timerEscut && escut<max_escut){
			escut+=max_escut *(regen_escut/100);
			if(escut>max_escut){
					escut=max_escut;
			}
			
			timerEscut=Time.time+temps_recarga_escut;
		}

	}
	
	
	private void drop(){
		myTransform.rotation.Set(0,0,0);
		Vector3 temp = myTransform.position;
		int ra = Random.Range(0, 2);
		if(ra==0){
			ra = Random.Range(0, 3);
			if(ra==0){
				GameObject missile = (GameObject)Instantiate(Resources.Load("cura"),temp,myTransform.rotation);
			}else if(ra==1){
				GameObject missile = (GameObject)Instantiate(Resources.Load("municio_pistola"),temp,myTransform.rotation);
			}else{
				GameObject missile = (GameObject)Instantiate(Resources.Load("municio_rifle"),temp,myTransform.rotation);
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

}