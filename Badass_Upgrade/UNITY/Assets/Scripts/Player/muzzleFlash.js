var muzzleFlash : Renderer;

var muzzleLight : Light;

 

function Start(){
	Debug.Log("yoo");
    muzzleFlash.enabled = false;
    muzzleLight.enabled = false;
}

 
public function Shoot(){
	Debug.Log("yolo");
    muzzleFlash.renderer.enabled = true;
    muzzleLight.enabled = true;
    yield WaitForSeconds(0.02);
    muzzleFlash.renderer.enabled = false;
    muzzleLight.enabled = false;
}