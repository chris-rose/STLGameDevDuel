using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
    //[HideInInspector] public bool jump = false;
    public float walkSpeed;
    public float eatWalkSpeed;
	public float runSpeed;
	public float timeElapsedRun;
	public float growthSize;
	public bool canMove = true;

    public enum motionStates {idle, walking, running}
	public enum actionStates {none, eating}
    public enum formStates {none, apple, taco}
	public motionStates mState;
	public actionStates aState;
    public actionStates fState;
    private float timeMoving;
    private Animator anim;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprd;
    private PlayerController plyrc;
    


    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        sprd = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
    	
    }

    void FixedUpdate() {
        //anim.SetFloat("Speed", Mathf.Abs(h));
        if (rb2d.velocity.x > 0 && !facingRight)
            Flip ();
        else if (rb2d.velocity.x < 0 && facingRight)
            Flip ();

        Action();
        if (canMove)
        	Move();
        else
        	rb2d.velocity = new Vector2 (0, 0);
    }

    void Move() {
    	float movex = Input.GetAxis("LeftStickX");
    	float movey = Input.GetAxis("LeftStickY");

    	if (movex == 0 && movey == 0) {
    		mState = motionStates.idle;
    		rb2d.velocity = new Vector2 (0, 0);
    		timeMoving = 0;
    	}
    	else if (aState == actionStates.eating) {
    		mState = motionStates.walking;
    		rb2d.velocity = new Vector2 (movex * eatWalkSpeed, movey * eatWalkSpeed);
    		timeMoving = 0;
    	}
    	else if (timeMoving < timeElapsedRun) {
    		mState = motionStates.walking;
    		rb2d.velocity = new Vector2 (movex * walkSpeed, movey * walkSpeed);
    		timeMoving += Time.deltaTime;
    	}
    	else {
    		mState = motionStates.running;
    		rb2d.velocity = new Vector2 (movex * runSpeed, movey * runSpeed);
    		timeMoving += Time.deltaTime;
    	}
    }

    void Action() {
    	Vector3 theScale = transform.localScale;
    	if((Input.GetAxis("LeftTrigger") > 0) || (Input.GetAxis("RightTrigger") > 0))
		{
			aState = actionStates.eating;
			theScale.x = growthSize;
			theScale.y = growthSize;
			transform.localScale = theScale;
			sprd.color = Color.red;
			return;
		}
    	//if(Input.GetButtonDown("Y"))
		//{
			//print ("SPECIAL");
			//spriteRenderer.color(red);
			//return;
		//}
		aState = actionStates.none;
		theScale.x = 1;
		theScale.y = 1;
		transform.localScale = theScale;
		sprd.color = Color.white;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(aState == actionStates.eating)
        	if (coll.gameObject.tag == "Food")
            	coll.gameObject.SendMessage("GetEaten", this.gameObject);
    }

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


