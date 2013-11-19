var muzzleFlash : Renderer;

var muzzleLight : Light;

 

function Start(){
	Debug.Log("yoo");
    muzzleFlash.enabled = false;
    muzzleLight.enabled = false;
    muzzleFlash.active = false;
    muzzleLight.active = false;
}

function Update()

{

    if(Input.GetButtonDown("Disparar"))

    {
        Shoot();

    }

}
 
public function Shoot(){
	//Debug.Log("yolo");
    muzzleFlash.renderer.enabled = true;
    muzzleLight.enabled = true;
    muzzleFlash.active = true;
    muzzleLight.active = true;
    yield WaitForSeconds(0.02);
    muzzleFlash.renderer.enabled = false;
    muzzleLight.enabled = false;
    muzzleFlash.active = false;
    muzzleLight.active = false;
}