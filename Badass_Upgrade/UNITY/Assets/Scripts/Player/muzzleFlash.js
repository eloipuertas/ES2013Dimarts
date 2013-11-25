var muzzleFlash : Renderer;

var muzzleLight : Light;

 

function Start(){
    muzzleFlash.enabled = false;
    muzzleLight.enabled = false;
	
	muzzleFlash.active = false; //Grupo A add
	muzzleLight.active = false; //Grupo A add
}

 
public function Shoot(){
    muzzleFlash.renderer.enabled = true;
    muzzleLight.enabled = true;
	
	muzzleLight.active = true; //Grupo A add
	muzzleFlash.active = true; //Grupo A add
    
	yield WaitForSeconds(0.02);
    muzzleFlash.renderer.enabled = false;
    muzzleLight.enabled = false;
	
	muzzleLight.active = false; //Grupo A add
	muzzleFlash.active = false; //Grupo A add
}