using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
    public float walkSpeed;
	public float runSpeed;
	public float timeElapsedToRun;
	public bool canMove = true;

    public enum motionStates {idle, walking, running}
	public motionStates motionState;
    private float timeMoving;
    private Animator anim;
    private Rigidbody2D rigidbod2d;
    private SpriteRenderer spriterend;
    private PlayerController playercontrl;
    


    // Collects object references
    void Awake () {
        anim = GetComponent<Animator>();
        rigidbod2d = GetComponent<Rigidbody2D>();
        spriterend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
    	
    }

    //Main update loop. Physics based game so use FixedUpdate instead
    void FixedUpdate() {
        //anim.SetFloat("Speed", Mathf.Abs(h)); //Once we include an animated sprite this will alter anim playback to match speed
        if (rigidbod2d.velocity.x > 0 && !facingRight)
            Flip ();
        else if (rigidbod2d.velocity.x < 0 && facingRight)
            Flip ();
        if (canMove)
        	Move();
        else
            rigidbod2d.velocity = new Vector2 (0, 0);
    }

    //Handles the motion of the player
    void Move() {
    	float movex = Input.GetAxis("LeftStickX");
    	float movey = Input.GetAxis("LeftStickY");

    	if (movex == 0 && movey == 0) {
    		motionState = motionStates.idle;
            rigidbod2d.velocity = new Vector2 (0, 0);
    		timeMoving = 0;
    	}
        else if (timeMoving < timeElapsedToRun) { //If they move continuously for X seconds they start "running" and move faster
    		motionState = motionStates.walking;
            rigidbod2d.velocity = new Vector2 (movex * walkSpeed, movey * walkSpeed);
    		timeMoving += Time.deltaTime;
    	}
    	else {
    		motionState = motionStates.running;
            rigidbod2d.velocity = new Vector2 (movex * runSpeed, movey * runSpeed);
    		timeMoving += Time.deltaTime;
    	}
    }

    //Makes sure the sprite is facing the right way according to motion. Called in FixedUpdate
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Will handle all collision fuctions. Might eventually become too long...
    void OnCollisionEnter2D(Collision2D coll) {
        //When you hit stuff do stuff here
        //check what you hit and perform corresponding thing
    }

    //In case the player needs to stop for game reasons
    void SetCanMove(bool i) {
    	canMove = i;
    }

    //Used for Testing There's only Debug stuff here
	void OnGUI()
	{
		//float temp = Input.GetAxis("RightTrigger");
		GUI.Label(new Rect(10, 10, 100, 20), (Input.GetAxis("LeftStickX").ToString()));
		//GUI.Label(new Rect (10,10,100,90), ("Right Trigger = " + temp.ToString()));
	}
}


