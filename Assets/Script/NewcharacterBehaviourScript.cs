using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterBehaviourScript : MonoBehaviour
{

    private Animator anim;
    public CharacterController characterController;
    public float charSpeed = 10.0f;
    public float turnSpeed = 100.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 50.0f;
    public float jumpForce = 15.0f;
    private float oldY = 0.0f;
    private float currentRot = 270.0f;
    private float oldRot = 0.0f;
    private int jumpStep = 2;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //translate control
        float angle = 0; //= Mathf.Ceil(((Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"))) * Mathf.Rad2Deg) + 270.0f);
        /*
        if (Input.GetKey("w")) { angle = 0 ; }
        else if(Input.GetKey("w") && Input.GetKey("d")) { angle = 45; }
        else if (Input.GetKey("d")) { angle = ; }
        else if (Input.GetKey("d") && Input.GetKey("s")) { angle = ; }
        else if (Input.GetKey("s")) { angle = ; }
        else if (Input.GetKey("s") && Input.GetKey("a")) { angle = ; }
        else if (Input.GetKey("a")) { angle = ; }
        else if (Input.GetKey("a") && Input.GetKey("w")) { angle = ; }*/

        anim.SetInteger("aniparam", 1);

        if (angle != currentRot)
        {
            currentRot = angle;
        }

        if (oldRot != currentRot)
        {
            currentRot = angle;
            transform.Rotate(0, oldRot - currentRot, 0);
        }

        oldRot = currentRot;

        if (Input.GetKey("w")) { }

        if (characterController.isGrounded)
        {
            moveDirection = transform.forward * charSpeed;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.x = transform.forward.x * charSpeed * 0.5f;
            moveDirection.z = transform.forward.z * charSpeed * 0.5f;
        }

        else
        {
            anim.SetInteger("aniparam", 0);
            moveDirection.x = 0;
            moveDirection.z = 0;
        }

        //jump control
        if (characterController.isGrounded)
        {
            jumpStep = 2;
        }

        if (Input.GetKeyDown("space") && jumpStep > 0)
        {  //
            jumpStep -= 1;
            moveDirection.y = jumpForce;
            anim.SetInteger("jumpAni", 1);
        }
        else if (Time.deltaTime - characterController.transform.position.y >= oldY)
        { //
            anim.SetInteger("jumpAni", 0);
        }

        oldY = Time.deltaTime - characterController.transform.position.y;
        //Debug.Log(oldRot + " " + transform.eulerAngles.y + " " + angle);


        characterController.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
    }
}



// moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
// moveDirection = transform.TransformDirection(moveDirection);
// moveDirection *= charSpeed;

//  transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);