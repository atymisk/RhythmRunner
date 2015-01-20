using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public bool debug = false;
	long memcheck;
	System.Diagnostics.Stopwatch timestamper;

	bool sliding;//prevent INF sliding
	float slideCheck;
	bool grounded;//preventing INF jumps
	float collider_x;
	float collider_y;
	float init_speed;

	public float speed = 1.0f;
	public float jumpspeed = 3.0f;
	public float slideDuration = 1.5f;

	public Transform top_left;
	public Transform bottom_right;
	public LayerMask ground_layers;
	// Use this for initialization

	void Start () 
	{
		grounded = false;
		sliding = false;
		slideCheck = 0;
		memcheck = 0;
		collider_x = collider2D.transform.localScale.x;
		collider_y = collider2D.transform.localScale.y;
		timestamper = new System.Diagnostics.Stopwatch();
		timestamper.Start();
		init_speed = speed;
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapArea(top_left.position, bottom_right.position, ground_layers);
	}

	// Update is called once per frame
	void Update () 
	{
		float x,y;
		x=rigidbody2D.velocity.x;
		y=rigidbody2D.velocity.y;
		if(debug)
		{
			if(System.GC.GetTotalMemory(false) < memcheck)
			{
				Debug.Log (System.GC.GetTotalMemory(false) + "\tCleared at: " + timestamper.Elapsed);
				timestamper.Reset();
				timestamper.Start ();
			}
		}
		if(sliding)
		{
			slideCheckTest();
		}
		/*
		if(Input.GetKey(KeyCode.RightArrow))
		{
			x = speed;
		}
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			x = speed*-1;
		}*/
		x = speed;
		if(Input.GetKey(KeyCode.UpArrow) && grounded)
		{
			y = jumpspeed;
		}
		if(Input.GetKey(KeyCode.DownArrow) && !sliding && grounded)
		{
			//slide();
			speed *=1.5f;
			float tempx = collider_x;
			float tempy = collider_y;
			sliding = true;
			collider2D.transform.localScale = new Vector2(tempx,tempy-0.2f);
			//slide duration
		}
		rigidbody2D.velocity = new Vector2(x,y);
		memcheck = System.GC.GetTotalMemory(false);
		//print(System.GC.GetTotalMemory(false));
	}

	void slideCheckTest()
	{
		sliding = !(slideCheck >= slideDuration);//if the slideCheck is equal to the full duration then it should not slide
		
		//if still sliding, keep incrementing slidecheck until it reaches the full duration
		if(sliding)
		{
			slideCheck += Time.deltaTime;
			if(debug){print (slideCheck);}
		}
		else
		{
			slideCheck = 0;
			speed = init_speed;
			collider2D.transform.localScale = new Vector2(collider_x,collider_y);
			//print (slideCheck);
		}
	}

}
