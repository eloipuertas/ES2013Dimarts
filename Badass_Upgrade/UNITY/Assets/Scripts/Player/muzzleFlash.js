var muzzleFlash : Renderer;

var muzzleLight : Light;

 

function Start(){
<<<<<<< HEAD
=======
	Debug.Log("yoo");
>>>>>>> origin/dev
    muzzleFlash.enabled = false;
    muzzleLight.enabled = false;
}

function Update()

{

    if(Input.GetButtonDown("Disparar"))

    {

        Shoot();

    }

}
 
<<<<<<< HEAD
function Shoot(){
=======
public function Shoot(){
	Debug.Log("yolo");
>>>>>>> origin/dev
    muzzleFlash.renderer.enabled = true;
    muzzleLight.enabled = true;
    yield WaitForSeconds(0.02);
    muzzleFlash.renderer.enabled = false;
    muzzleLight.enabled = false;
}