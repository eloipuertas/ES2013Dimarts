// speed of the ball

var speed = 5.0;

var radius = 5.0;

var jumpForce = 1;

var jumpTimer = 0.0;

var localScaleY;

var jumpBool;

var cooldown;

private var velocity : Vector3 = Vector3.zero;

 

function Start(){
    transform.localScale = Vector3.one * radius * 2;
	
    var hit : RaycastHit;

    if(Physics.Linecast(transform.position, transform.position - Vector3.up * 500, hit)){

        transform.position = hit.point + Vector3.up * radius;

    }

    // add a rigidbody if we dont have one.

    if(!rigidbody)

        gameObject.AddComponent(Rigidbody);

    // set the mass according to the radius.

    rigidbody.mass = 100 * radius;
    cooldown = 1;
    localScaleY = transform.localScale.y;
    jumpBool = false;
    
    Time.timeScale =1;

}

  

function FixedUpdate () {

    // let see if our body is on the ground.

    var hit : RaycastHit;

    var isGrounded = Physics.Raycast(transform.position, -Vector3.up, hit, radius * 1.5);

    // base movement off of the camera, not the object.

    // reset the camera's X to zero, so that it is always looking horizontally.

    //var x = Camera.main.transform.localEulerAngles.x;

    //Camera.main.transform.localEulerAngles.x = 0;

    

    // now collect the movement stuff This is generic direction.

    var direction = Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

    

    // prevent the ball from moving faster diagnally

    if(direction.magnitude > 1.0) direction.Normalize();

    

    // If we are grounded, then lets see if we want to jump.

    if(isGrounded && Input.GetKey(KeyCode.Space)){
    	
        if(jumpTimer == 0.0){
        	jumpTimer = Time.time;
        }
        if(jumpTimer < (Time.time - cooldown))
        	jumpBool = true;
        
   
        if (jumpBool){
        	//if(jumpForce <10 )transform.localScale.y = transform.localScale.y*0.1;
        	jumpForce++;
        	jumpBool = false;
 			jumpTimer = 0.0;
        }
     }
     
     if(isGrounded && Input.GetKeyUp(KeyCode.Space)){
     	Debug.Log(jumpForce);
      	rigidbody.AddForce(Vector3.up * rigidbody.mass * 500 * jumpForce);
      	jumpTimer = cooldown;
      	jumpForce = 1;
      	//transform.localScale.y = localScaleY;
      	
      }

    

    // if we arent pressing anything, dont mess with the physics.

    if(direction.magnitude > 0){

        // convert isGrounded into something we can use (if (isGrounded = !null - return 3.0, else return 0.5))

        var modifier = isGrounded ? 3.0 : 0.5;

        // lets set the direction according to the camera now.

        direction = Camera.main.transform.TransformDirection(direction) * speed * 2;

        // lets take the downward velocity from the current so that we dont get wierd physics results

        if(!isGrounded)
        direction.y = rigidbody.velocity.y;

        

        // Now, lets keep track of a velocity.

        // This will let the ball move while we are not pressing anything.

        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction, modifier * Time.deltaTime);
        if(isGrounded)rigidbody.velocity.y = 0;

        // Now, lets break the rotation out from the movement.

        var rotation = Vector3(rigidbody.velocity.z,0,-rigidbody.velocity.x) * 20;

        

        

        // Lets add some spin to make the ball move better

        rigidbody.angularVelocity = Vector3.Lerp(rigidbody.angularVelocity, rotation, modifier * Time.deltaTime);

    }

    

    // return the camera's x rotation.

    //Camera.main.transform.localEulerAngles.x = x;

}