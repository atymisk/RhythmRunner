using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {


	public float speed;
	public float jumpspeed = 3.0f;
	float distToGround;
	bool grounded = true;
	Transform top_left;
	Transform bottom_right;
	LayerMask ground_layers;
	// Use this for initialization
	void Start () {
		speed = 1.0f;
		distToGround = collider2D.bounds.extents.y;
	}

	void FixedUpdate()
	{
		grounded = Physics2D.OverlapArea(top_left.position, bottom_right.position, ground_layers);
	}

	// Update is called once per frame
	void Update () {
		float x,y;
		x=rigidbody2D.velocity.x;
		y=rigidbody2D.velocity.y;

		if(Input.GetKey(KeyCode.RightArrow))
		{
			x = speed;
		}
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			x = speed*-1;
		}
		if(Input.GetKey(KeyCode.UpArrow) && grounded)
		{
			y = jumpspeed;
		}
		rigidbody2D.velocity = new Vector2(x,y);
	
	}
}
