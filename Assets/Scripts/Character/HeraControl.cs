using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeraControl : MonoBehaviour
{

    public CharacterController characterController;
    public float charSpeed = 10.0f, turnSpeed = 100.0f, gravity = 50.0f, jumpForce = 15.0f;
    public bool water = false;

    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private float oldY = 0.0f, moveCooldown = 0.5f, moveCooldownCounter;
    private int jumpStep = 2, kicktime = 0, maxKickTime = 40, slapTime = 0, maxSlapTime = 20;
    private int comboMove = 0, comboTime = 0, maxcomboTime = 25;
    private bool isGround, takeNextCombo = false;

    private enum HeraAction { Standing, Walking, Comboing, Kicking, Slaping, Die }
    private HeraAction heraAction, oldAction;



    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        heraAction = HeraAction.Standing;
        oldAction = HeraAction.Standing;
    }

    // Update is called once per frame
    void Update()
    {
        if(heraAction != HeraAction.Die)
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
        }
        
        // Face direction
        if (Input.GetKey("up") && Input.GetKey("left")) getRotation(new Vector3(-1f, 0f, 1f));
        else if (Input.GetKey("up") && Input.GetKey("right")) getRotation(new Vector3(1f, 0f, 1f));
        else if (Input.GetKey("down") && Input.GetKey("left")) getRotation(new Vector3(-1f, 0f, -1f));
        else if (Input.GetKey("down") && Input.GetKey("right")) getRotation(new Vector3(1f, 0f, -1f));

        else if (Input.GetKey("up")) getRotation(new Vector3(0f, 0f, 1f));
        else if (Input.GetKey("down")) getRotation(new Vector3(0f, 0f, -1f));
        else if (Input.GetKey("left")) getRotation(new Vector3(-1f, 0f, 0f));
        else if (Input.GetKey("right")) getRotation(new Vector3(1f, 0f, 0f));
        

        //=== Check key ===
        if(Input.GetKeyDown(KeyCode.Keypad0) && (heraAction == HeraAction.Standing || heraAction == HeraAction.Walking) 
            && isGround && Time.time > moveCooldownCounter)
        {
            oldAction = heraAction;
            heraAction = HeraAction.Comboing;
            comboTime = 0;
            comboMove = 1;
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2) && (heraAction == HeraAction.Standing || heraAction == HeraAction.Walking) && Time.time > moveCooldownCounter)
        {
            oldAction = heraAction;
            heraAction = HeraAction.Slaping;
            slapTime = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Keypad1) && (heraAction == HeraAction.Standing || heraAction == HeraAction.Walking) && Time.time > moveCooldownCounter)
        {
            oldAction = heraAction;
            heraAction = HeraAction.Kicking;
            kicktime = 0;
        }
        // Animation and Jump direction
        else if(heraAction == HeraAction.Standing || heraAction == HeraAction.Walking) //not in attacking move
        {
            if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
                heraAction = HeraAction.Walking;
            else
            {
                heraAction = HeraAction.Standing;
            }
        }
        
        
        if (heraAction == HeraAction.Standing)
        {
            anim.SetInteger("Moving", 0);
            moveDirection.x = 0;
            moveDirection.z = 0;
            if (isGround)
            {
                jumpStep = 2;
                anim.SetInteger("Jumping", 0);
            }

            //Jump control
            if (Time.deltaTime - characterController.transform.position.y >= oldY)
            {
                anim.SetInteger("Jumping", 3);
            }
            if (Input.GetKeyDown("space") && jumpStep > 0)
            {
                jumpStep--;
                moveDirection.y = jumpForce;
                anim.SetInteger("Jumping", 1);
            }
            
        }

        else if (heraAction == HeraAction.Walking)
        {
            anim.SetInteger("Moving", 1);

            if (!isGround) //floating
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

                if (Time.deltaTime - characterController.transform.position.y >= oldY)
                {
                    anim.SetInteger("Jumping", 3);
                }

            }
            else if(isGround)
            {
                jumpStep = 2;
                anim.SetInteger("Jumping", 0);

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

            //Jump control
            if (Input.GetKeyDown("space") && jumpStep > 0)
            {
                jumpStep--;
                moveDirection.y = jumpForce;
                anim.SetInteger("Jumping", 1);
            }
        }

        else if (heraAction == HeraAction.Comboing)
        {
            moveDirection.x = transform.forward.x * charSpeed * 0.2f;
            moveDirection.z = transform.forward.z * charSpeed * 0.2f;
            comboTime += 1;
            if(comboMove > 6) //end of combo
            {
                comboTime = 0;
                heraAction = oldAction;
                anim.SetInteger("ComboAttack", 0);
                moveCooldownCounter = Time.time + moveCooldown;
            }
            else if(comboMove == 6) //if action to pre end state will stand for 2 frame for transition
            {
                anim.SetInteger("ComboAttack", 6);
                if(comboTime > 2)
                {
                    comboMove = 7;
                }
            }
            else if(comboTime >= maxcomboTime && comboMove < 7) //call when end each move
            {
                comboTime = 0;
                if(takeNextCombo && comboMove < 6){comboMove += 1;}
                else{comboMove = 6; comboTime = 0;}
            }
            else if(comboTime < maxcomboTime && comboMove < 7) //for give action 1-6  And action not end that time
            {          
                anim.SetInteger("ComboAttack", comboMove);
                if(comboMove == 3){moveDirection.y = jumpForce/2;}
                if(comboMove == 4){moveDirection.y -= gravity *2 * Time.deltaTime;}
            }
            //get next combo
            if(comboTime < (maxcomboTime/2) && comboMove < 7){takeNextCombo = false;}
            else if(Input.GetKeyDown(KeyCode.Keypad0) && !takeNextCombo && comboMove < 5){takeNextCombo = true;}

        }

        else if (heraAction == HeraAction.Kicking)
        {
            moveDirection.x = transform.forward.x * charSpeed * 1.1f;
            moveDirection.z = transform.forward.z * charSpeed * 1.1f;
            kicktime += 1;
            if(kicktime > maxKickTime)
            {
                heraAction = oldAction;
                anim.SetInteger("Kicking", 0);
                kicktime = 0;
            }
            else if(kicktime < (maxKickTime/2))
            {
                anim.SetInteger("Kicking", 1);
            }
            else
            {
                anim.SetInteger("Kicking", 2);
            }
        }

        else if (heraAction == HeraAction.Slaping)
        {
            anim.SetInteger("Slapping", 1);
            moveDirection.x = transform.forward.x * charSpeed * 0.2f;
            moveDirection.z = transform.forward.z * charSpeed * 0.2f;
            slapTime += 1;
            
            if(slapTime > maxSlapTime)
            {
                heraAction = oldAction;
                anim.SetInteger("Slapping", 0);
                slapTime = 0;
            }
        } 

        if (heraAction == HeraAction.Die)
        {
            anim.SetInteger("die", 2);

            moveDirection = Vector3.zero;
            
        }

        if(GameData.data.curHp <= 0)
        {
            if(anim.GetInteger("ComboAttack") != 6 || anim.GetInteger("ComboAttack") != 0)
            {
                anim.SetInteger("ComboAttack", 6);
            }
            anim.SetInteger("die", 1);
            heraAction = HeraAction.Die;
        }


        if(moveDirection.y < (gravity * -1))
        {
            moveDirection.y = (gravity * -1);
        }

        //calculate direction afrer all
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

    private bool isWalkPressing()
    {
        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
        {
            return true;
        }
        return false;
    }
    // public void dieHera()
    // {
    //     heraAction = HeraAction.Die;
    //     anim.SetInteger("die", 1);
    //     moveDirection = Vector3.zero;
    // }
}
