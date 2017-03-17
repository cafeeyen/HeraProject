using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl: MonoBehaviour
{

    private Animator anim;
    public CharacterController characterController;
    public float charSpeed = 10.0f;
    public float turnSpeed = 100.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 50.0f;
    public float jumpForce = 15.0f;
    private float oldY = 0.0f;
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
        // Face direction
        if (Input.GetKey("w") && Input.GetKey("a")) getRotation(new Vector3(-1f, 0f, 1f));
        else if (Input.GetKey("w") && Input.GetKey("d")) getRotation(new Vector3(1f, 0f, 1f));
        else if (Input.GetKey("s") && Input.GetKey("a")) getRotation(new Vector3(-1f, 0f, -1f));
        else if (Input.GetKey("s") && Input.GetKey("d")) getRotation(new Vector3(1f, 0f, -1f));

        else if (Input.GetKey("s")) getRotation(new Vector3(0f, 0f, -1f));
        else if (Input.GetKey("w")) getRotation(new Vector3(0f, 0f, 1f));
        else if (Input.GetKey("a")) getRotation(new Vector3(-1f, 0f, 0f));
        else if (Input.GetKey("d")) getRotation(new Vector3(1f, 0f, 0f));

        if (!characterController.isGrounded)
        {
            // Jump movement speed
            moveDirection.x = transform.forward.x * charSpeed * 0.7f;
            moveDirection.z = transform.forward.z * charSpeed * 0.7f;
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
            moveDirection *= charSpeed;
        }

        // Animation and Jump direction
        if (Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("d"))
            anim.SetInteger("aniparam", 1);
        else
        {
            anim.SetInteger("aniparam", 0);
            moveDirection.x = 0;
            moveDirection.z = 0;
        }

        //Jump control
        if (Input.GetKeyDown("space") && jumpStep > 0)
        {
            jumpStep--;
            moveDirection.y = jumpForce;
            anim.SetInteger("jumpAni", 1);
        }
        else if (Time.deltaTime - characterController.transform.position.y >= oldY)
            anim.SetInteger("jumpAni", 0);

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
}