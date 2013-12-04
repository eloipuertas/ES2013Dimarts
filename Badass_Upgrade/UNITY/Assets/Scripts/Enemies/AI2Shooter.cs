using UnityEngine;
using System.Collections;

public class AI2Shooter : MonoBehaviour {
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
    private int fireRate=2;
    private int distancia_alerta=20;
    private int distancia_perseguir=6;
    private int distancia_melee=2;
	private int distancia_disparar = 15;
	private float escut =100, max_escut=100;
	private int temps_recarga_escut=2;
	private float regen_escut=20;
	private int armadura=3;
	private bool unhit=true;
    //-------------------------------------------
    
    public Vector3 spawnPoint;
    public string state;
    public float timerAtac,timerEscut;
	RaycastHit hit;
    GameObject hud;
    private Transform myTransform;
	
	
	GameObject shield;
	public GUITexture enemy_Healthbar;
	float maxvida = 0.0f;
	float timerShot;
	bool inSight,prev_inSight,recently_shot,isShowingLaser;
	LineRenderer linerenderer;
	
	

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
		float Size_width = 0.0005f;
		float Size_height = 0.0050f;
		
		Size_width = percent*Size_width;
		enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
		
		inSight=false;
		prev_inSight=false;
		
		timerShot = Time.time;
		recently_shot = false;
		
		hud.SendMessage("addEnemy");
		
		isShowingLaser=false;
		linerenderer = (LineRenderer)gameObject.GetComponent("LineRenderer");
		linerenderer.enabled = false;
		
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
		if(Distance<distancia_alerta && Distance>distancia_disparar && unhit){
			if(state != "alerta" && state != "shooting"){
				animation.Play("activar");
				Destroy (shield);
			}
			state="alerta";
			Vector3 temp = target.position;
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(temp - enemyChest), rotationSpeed * Time.deltaTime);

				
        }else if(Distance<distancia_disparar){
			Vector3 temp = target.position;
			//temp.y = 0.0f;
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(temp - enemyChest), rotationSpeed * Time.deltaTime);
			state="shooting";
            attack(dist_dmg,true,Distance);
        }else{
			if(state != "away" && unhit){
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

    private void attack(int dmg,bool ranged,float distancia){

		
		
        if(Time.time>timerAtac){
			if(ranged){
				Debug.Log("Shooting");
				animation.CrossFade("disparar");
				disparar(distancia_disparar,dist_dmg,distancia);
			}else{
				Debug.Log("Melee");
				animation.CrossFade("melee");
				disparar(distancia_disparar,melee_dmg,distancia);
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

	
	private void disparar(int dis,int dmg,float distancia){
		showLaser();
		if(Physics.Raycast(transform.position, (target.position- transform.position), out hit, dis)) {
			Debug.DrawLine(target.position, transform.position, Color.green);
			Debug.DrawRay(transform.position, transform.forward,Color.blue);
			print ("wtf is this"+hit.collider.gameObject.tag);
			if(hit.collider.gameObject.tag == "Player") {
				bool success=false;
				int rr=Random.Range(0,11);

				if(distancia<10){
					success=true;
				}else if (distancia>10 && distancia <20){
					if(rr>2) success=true;
					
				}else if(distancia>20 && distancia<40){
					if(rr>5) success=true;
				}else{
					if(rr>8) success=true;
				}
				print ("lol"+success);
				if (success){
					GameObject.FindGameObjectWithTag("Player").SendMessage("rebreAtac",dmg);
				}
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
	
	
	public void showLaser()
	{	
		Debug.Log("LASERLASERLASER ON");
		if(isShowingLaser){
				return;
		}
		isShowingLaser = true;
		linerenderer.enabled = true;
		Invoke ("resetLaser", 0.1f);
	}
		
	
	public void resetLaser()
	{    
		linerenderer.enabled = false;
		isShowingLaser = false;
		Debug.Log("LASERLASERLASER OFF");
	}

}