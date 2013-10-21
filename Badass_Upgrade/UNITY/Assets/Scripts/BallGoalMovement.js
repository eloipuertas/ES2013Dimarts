#pragma strict
var radius = 5.0;

function Start () {
	transform.localScale = Vector3.one * radius * 2;
}

function FixedUpdate () {

	var hit : RaycastHit;
    var isGrounded = Physics.Raycast(transform.position, -Vector3.up, hit, radius * 1.5);
    

        // convert isGrounded into something we can use (if (isGrounded = !null - return 3.0, else return 0.5))

        var modifier = isGrounded ? 3.0 : 0.5;
        
        var rotation = Vector3(rigidbody.velocity.z,0,-rigidbody.velocity.x) * 20;

        // Lets add some spin to make the ball move better
        //rigidbody.angularVelocity = Vector3.Lerp(rigidbody.angularVelocity, rotation, modifier * Time.deltaTime);

}
        
