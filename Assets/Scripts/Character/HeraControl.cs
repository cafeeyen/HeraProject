using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeraControl : MonoBehaviour {

	public CharacterController characterController;
    public float charSpeed = 10.0f, turnSpeed = 100.0f, gravity = 50.0f, jumpForce = 15.0f;
    public bool water = false;

    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private float oldY = 0.0f;
    private int jumpStep = 2;
    private bool isGround;

    private enum HeraAction { Standing, Walking, Comboing, Kicking }
    HeraAction heraAction, oldAction;


	// Use this for initialization
	void Start () 
    {
		anim = gameObject.GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () 
    {
		// Face direction
        if (Input.GetKey("up") && Input.GetKey("left")) getRotation(new Vector3(-1f, 0f, 1f));
        else if (Input.GetKey("up") && Input.GetKey("right")) getRotation(new Vector3(1f, 0f, 1f));
        else if (Input.GetKey("down") && Input.GetKey("left")) getRotation(new Vector3(-1f, 0f, -1f));
        else if (Input.GetKey("down") && Input.GetKey("right")) getRotation(new Vector3(1f, 0f, -1f));

        else if (Input.GetKey("up")) getRotation(new Vector3(0f, 0f, 1f));
        else if (Input.GetKey("down")) getRotation(new Vector3(0f, 0f, -1f));
        else if (Input.GetKey("left")) getRotation(new Vector3(-1f, 0f, 0f));
        else if (Input.GetKey("right")) getRotation(new Vector3(1f, 0f, 0f));

        if (!isGround)
        {
            // Jump movement speed
            if (water)
            {
                moveDirection.x = transform.forward.x * charSpeed * 0.7f * 0.5f;
                moveDirection.z = transform.forward.z * charSpeed * 0.7f * 0.5f;
            }
            else
            {
                moveDirection.x = transform.forward.x * charSpeed * 0.7f;
                moveDirection.z = transform.forward.z * charSpeed * 0.7f;
            }
            
        }
        else
        {
            jumpStep = 2;

            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;
            Vector3 turn = new Vector3(forward.z, 0, -forward.x);
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            moveDirection = (h * turn + v * forward);

            if (water)
                moveDirection *= charSpeed * 0.5f;
            else
                moveDirection *= charSpeed;
        }

        // Animation and Jump direction
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
            anim.SetInteger("Moving", 1);
        else
        {
            anim.SetInteger("Moving", 0);
            moveDirection.x = 0;
            moveDirection.z = 0;
        }

        //Jump control
        if (Input.GetKeyDown("space") && jumpStep > 0)
        {
            jumpStep--;
            moveDirection.y = jumpForce;
            anim.SetInteger("Jumping", 1);
        }
        else if (Time.deltaTime - characterController.transform.position.y >= oldY)
        {
            anim.SetInteger("Jumping", 3);
        }
        else if(isGround)
        {
            anim.SetInteger("Jumping", 0);
        }

        oldY = Time.deltaTime - characterController.transform.position.y;

        characterController.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
	}

    void getRotation(Vector3 toRotation)
    {
        // Rotate model while turning
        Vector3 relativePos = Camera.main.transform.TransformDirection(toRotation);
        relativePos.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
    }

    public void isWater(bool water)
    {
        this.water = water;
    }

    public bool IsGround
    {
        get { return isGround; }
        set { isGround = value; }
    }
}
