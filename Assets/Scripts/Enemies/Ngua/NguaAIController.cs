using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NguaAIController : MonoBehaviour
{

    private Animator animator;
    public CharacterController control;
    public Collider headCollider, tailCollider, LArmCollider, RArmCollider;

    public int followRange, attackRange, slapRange = 8, tailRange = 12, dashRange = 25, gravity;
    public float moveSpeed, turnSpeed, maxDashTime, slapCooldown = 1.55f, tailCooldown = 3.5f, dashCooldown = 5.5f;
    private float distance, currentSpeed, currentDashTime, slapCooldownCounter, tailCooldownCounter, dashCooldownCounter;
    private bool inRange;
    private Vector3 moveVector, playerXZPosition, nguaXZPosition;
    private enum NguaAction { None, Slapping, Dashing, TailAttack }
    private enum NguaMoving { Neutral, Following, Attacking }
    private NguaAction nguaAction;
    private NguaMoving nguaMoving;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        control = gameObject.GetComponent<CharacterController>();

        player = GameObject.FindWithTag("Player");
        nguaMoving = NguaMoving.Neutral;
        currentSpeed = moveSpeed;
        headCollider.enabled = false;
        tailCollider.enabled = false;
        LArmCollider.enabled = false;
        RArmCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nguaAction);
        distance = Vector3.Distance(transform.position, player.transform.position);
        inRange = distance < followRange;
        if (inRange)
        {
            //Debug.Log(nguaAction + " " + nguaMoving + ": " + distance);
            if (distance < 6.5 && nguaMoving == NguaMoving.Following)
            {
                nguaMoving = NguaMoving.Neutral;
            	currentDashTime = 0;
            	animator.SetInteger("waking", 0);
            	moveVector = Vector3.zero;
            	currentSpeed = moveSpeed;
            }
			else if(distance > 7 && nguaMoving == NguaMoving.Neutral)
			{
				nguaMoving = NguaMoving.Following;
			}
        }
        else if (!inRange)
        {
            nguaMoving = NguaMoving.Neutral;
            currentDashTime = 0;
            animator.SetInteger("waking", 0);
            moveVector = Vector3.zero;
            currentSpeed = moveSpeed;
        }

        if (nguaMoving == NguaMoving.Attacking)
        {
            if (nguaAction == NguaAction.Dashing)
            {
                moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
                currentDashTime += 1;

                if (currentDashTime > maxDashTime || NguaCollisionDetector.isColliding)
                {
                    animator.SetInteger("dashing", 0);
                    NguaCollisionDetector.isColliding = false;
                    headCollider.enabled = false;
                    currentSpeed -= 40;
                    dashCooldownCounter = Time.time + dashCooldown;
                    nguaMoving = NguaMoving.Following;
                    nguaAction = NguaAction.None;
                }
                else if (currentDashTime + (maxDashTime * 0.2) > maxDashTime)
                {
                    animator.SetInteger("dashing", 3);
                    headCollider.enabled = false;
                }
                else if (currentDashTime < 10)
                {
                    animator.SetInteger("dashing", 1);
                    moveVector = Vector3.zero;
                    nguaXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
                    playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                        Quaternion.LookRotation(playerXZPosition - nguaXZPosition), turnSpeed * Time.deltaTime);
                }
                else if (currentDashTime < maxDashTime)
                {
                    animator.SetInteger("dashing", 2);
                    moveVector.y = ((player.transform.position.y - 5) - transform.position.y) * Time.deltaTime;
                    headCollider.enabled = true;
                }
            }
            else if (nguaAction == NguaAction.TailAttack)
            {
                animator.SetInteger("attackMove", 2);
                tailCollider.enabled = true;
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Ngua_attack3"))
                {
                    animator.SetInteger("attackMove", 0);
                    nguaMoving = NguaMoving.Following;
                    nguaAction = NguaAction.None;
                    tailCooldownCounter = Time.time + tailCooldown;
                    tailCollider.enabled = false;
                }
            }
            else if (nguaAction == NguaAction.Slapping)
            {
                animator.SetInteger("attackMove", 1);
                LArmCollider.enabled = true;
                RArmCollider.enabled = true;

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Ngua_attack"))
                {
                    animator.SetInteger("attackMove", 0);
                    nguaMoving = NguaMoving.Following;
                    nguaAction = NguaAction.None;
                    slapCooldownCounter = Time.time + slapCooldown;
                    LArmCollider.enabled = false;
                    RArmCollider.enabled = false;
                }
            }
        }

        else if (nguaMoving == NguaMoving.Following)
        {
            animator.SetInteger("waking", 1);
            moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
            nguaXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
            playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(playerXZPosition - nguaXZPosition), turnSpeed * Time.deltaTime);

            //add if inrange
            if (distance < slapRange && Time.time > slapCooldownCounter)
            {
                nguaAction = NguaAction.Slapping;
                nguaMoving = NguaMoving.Attacking;
            }
            else if (distance < tailRange && Time.time > tailCooldownCounter) 
            {
                nguaAction = NguaAction.TailAttack;
                nguaMoving = NguaMoving.Attacking;
            }
            else if (distance < dashRange && Time.time > dashCooldownCounter ) 
            {
                currentSpeed += 40;
                currentDashTime = 0;
                nguaAction = NguaAction.Dashing;
                nguaMoving = NguaMoving.Attacking;
            }
        }
        
        else if (nguaMoving == NguaMoving.Neutral)
        {
            currentDashTime = 0;
            animator.SetInteger("waking", 0);
            moveVector = Vector3.zero;
            currentSpeed = moveSpeed;
            if (distance < slapRange && Time.time > slapCooldownCounter)
            {
                nguaAction = NguaAction.Slapping;
                nguaMoving = NguaMoving.Attacking;
            }
        }

        moveVector.y -= gravity * Time.deltaTime;
        control.Move(moveVector);
    }
}
