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
	public float vida=100;
    public int moveSpeed=10;
	public int rotationSpeed=1;
    float Distance;

	//----------------------------------
    private int fireRate=5;
	private int directionChangeRate=5;
    private int distancia_alerta=30;
	private int distancia_disparar = 20;
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
	
	
	GameObject shield;
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
		
		
		Vector3 enemyChest = myTransform.position+Vector3.up*1.6f;
        Debug.DrawRay(enemyChest, transform.forward);
		//Debug.DrawLine(target.position, myTransform.position, Color.yellow);
                
		if(Distance<distancia_alerta && Distance>distancia_disparar){
			if(state != "alerta"){
				animation.Play("activar");
				Destroy (shield);
			}
			state="alerta";
			//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			/*animation.CrossFade("activar");*/
        }else if(Distance<=distancia_disparar){
			//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			state="shooting";
			if(Time.time>timerDirection){
				timerDirection=Time.time+directionChangeRate;
        	}
            spawn_enemy();
        }else{
			if(state != "away"){
				animation.Play("desactivar");
				shield = (GameObject)Instantiate(Resources.Load("Enemy_Shield"),myTransform.position,myTransform.rotation);
			}
			state="away";
			//Debug.Log("Enemic inactiu");
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
				GameObject Spawned_Enemy = (GameObject)Instantiate(Resources.Load("enemy_chaser"),temp,myTransform.rotation);
        }
     }
	
	public void rebreDany(int dmg){
		if (state != "away"){
			vida-=dmg;
			
			if (vida < maxvida*0.5f){
				ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
				particlesystem.enableEmission = true;
			}
			
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