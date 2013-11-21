using UnityEngine;
using System.Collections;

public class AIShield : MonoBehaviour {
    public Transform target;

    //Noms de les animacions:
	//ajupit
	//actiu
	//caminar
	//disparar
	//melee


    //variables modificables segons la ia--------
	public float vida=40.0f;
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
	
    //-------------------------------------------
    
    public Vector3 spawnPoint;
    public string state;
    public float timerAtac,timerEscut;
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
			Debug.Log (target.forward.ToString()+" "+myTransform.position.ToString()+" "+target.position.ToString());
		}else{
			inSight = false;	
		}
		if (inSight && !prev_inSight){
			float percent = 0.0f;
			percent = vida/maxvida;
			percent = percent*100;
			float Size_width = 0.001f;
			float Size_height = 0.010f;
			
			Size_width = percent*Size_width;
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(1*Size_width,(float)Screen.width/Screen.height*Size_height,1);
			prev_inSight = true;
		}else if(!inSight){
			Debug.Log ("NOT PAINTING");
			float Size_width = 0.0001f;
			float Size_height = 0.010f;
			enemy_Healthbar.guiTexture.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
			prev_inSight = false;
		}
		
		//regenerar_escut();
       	//myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		Distance=Vector3.Distance(target.position,transform.position);
		
        Debug.DrawRay(transform.position, transform.forward);
		//Debug.DrawLine(target.position, myTransform.position, Color.yellow);
                
        if(Distance>distancia_alerta && Vector3.Distance(spawnPoint, transform.position)>3){
        	state="away";
			//Debug.Log("Enemic inactiu");
			animation.CrossFade("ajupit");
            //renderer.material.color=Color.blue;
            //retorn al spawnpoint?
		}else if(Distance<distancia_alerta && Distance>distancia_disparar){
			if(state != "alerta"){
				animation.CrossFade("activar");
				Destroy (shield);
				//ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
				//particlesystem.enableEmission = false;				
			}
			state="alerta";
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			/*animation.CrossFade("activar");*/
				
        }else if(Distance<distancia_disparar && Distance>distancia_perseguir){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
			state="shooting";
            attack(dist_dmg,true);
        }else if((Distance <=distancia_perseguir) && (Distance>distancia_melee)){
			moveTo();
            state = "walking";
			animation.CrossFade("caminar");
        }else if(Distance<distancia_melee ){
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	        state="attack";
	        //renderer.material.color=Color.red;
			Debug.Log("Atacant a melee");
	        attack(melee_dmg,false);
        }else{
			if(state != "away"){
				animation.CrossFade("desactivar");
				shield = (GameObject)Instantiate(Resources.Load("Enemy_Shield"),myTransform.position,myTransform.rotation);
				//ParticleSystem particlesystem = (ParticleSystem)gameObject.GetComponent("ParticleSystem");
				//particlesystem.enableEmission = true;
			}
			
			state="away";
			//Debug.Log("Enemic inactiu");
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
		if (state != "away"){
			vida-=dmg;
			
			float percent = 0.0f;
			percent = vida/maxvida;
			percent = percent*100;
			//enemy_Healthbar.guiTexture.pixelInset.Set(enemy_Healthbar.guiTexture.pixelInset.x,enemy_Healthbar.guiTexture.pixelInset.y,percent,enemy_Healthbar.guiTexture.pixelInset.height);
			//Rect temp1 = new Rect(0, 0, percent, 10);
			//enemy_Healthbar.guiTexture.pixelInset=temp1;
			float Size_width = 0.001f;
			float Size_height = 0.010f;
			
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
	
	private void disparar(int distancia,int dmg){
		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, (target.position- transform.position), distancia);
	    int i = 0;
        while (i < hits.Length) {
            RaycastHit hit = hits[i];
			Debug.Log (hits[i]);
	        if (hits[i].collider.tag == "Player"){
				Debug.Log("ataco al player i li faig "+dmg+" punts de dany");
				hit.transform.gameObject.SendMessage("rebreAtac",dmg);
				break;
			}else if(hits[i].collider.tag == null){
				break;
			}
			i++;
	    }
		/*if(Physics.Raycast(transform.position, (target.position- transform.position), out hit, dis)) {
			Debug.DrawLine(target.position, transform.position, Color.green);
			Debug.DrawRay(transform.position, transform.forward,Color.blue);
			//print (hit.collider.gameObject.tag);
			if(hit.collider.gameObject.tag == "Player") {
				
			}
		}*/
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