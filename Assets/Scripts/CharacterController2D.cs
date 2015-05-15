using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour {

    private Vector2 jumpForce = new Vector2(0, 35);
    private Vector2 jumpForce2 = new Vector2(0, 15);
    private bool hasExtraJump;
    private bool isShrunk;
    private float timePassed;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
        
        var pos = (Vector2)transform.position;

        // check death state
        if (pos.y < -10 || pos.x < -40)
        {
            Destroy(gameObject);
            // TODO: death screen
            Application.LoadLevel(Application.loadedLevel);
            return;
        }

        var hit = Physics2D.OverlapArea(
            pointA: pos + new Vector2(-0.45f, -1.1f),
            pointB: pos + new Vector2( 0.45f,  0.0f),
            layerMask: (1 << LayerMask.NameToLayer("Platform")));

        // it is grounded if is touching any platform
        var isGrounded = hit != null;
        
        // reeset extra jump whenever player hits the ground
        if (isGrounded) hasExtraJump = true;

        var jumpPressed = Input.GetButtonDown("Jump");
        var jump = jumpPressed && (isGrounded || hasExtraJump);
        
        var shrinkPressed = Input.GetButtonDown("Fire1");

        // Deals with shrinking:
        if (shrinkPressed)
        {
            isShrunk = true;
            transform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
        }

        if (isShrunk)
        {
            timePassed++;
        }

        if (timePassed >= 30)
        {
            isShrunk = false;
            timePassed = 0;
            transform.localScale = new Vector3(1.0f, 2.0f, 1.0f);
        }
        
        
        // Deals with Jumping
        var vel = rigidbody2D.velocity;
        vel.x = 0;
 
       // clear vertical velocity if player is about to jump
        if (jump) vel.y = 0;

        rigidbody2D.velocity = vel;

        if (jump) 
        {
            // apply force
            rigidbody2D.AddForce(jumpForce, ForceMode2D.Impulse);
            // burn extra jump if was not grounded
            if (!isGrounded) hasExtraJump = false;
        }
    }

    void FixedUpdate() {
	
    }
}
