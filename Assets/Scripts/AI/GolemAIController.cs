using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAIController : MonoBehaviour
{

    private Animator animator;
    public CharacterController control;
    public GameObject player;

    public int followRange, smashRange = 30, throwRange = 60, gravity;
    public float moveSpeed, turnSpeed, smashCooldown = 1.5f, throwCooldown = 3.0f;
    private float distance, currentSpeed, smashCooldownCounter, throwCooldownCounter;
    private int currentSmashTime, maxSmashTime = 70, currentThrowTime, maxThrowTime = 120, stateChanged = 0;
    private bool inRange, isEntering = false, hasBeenOut = true;
    private Vector3 moveVector, playerXZPosition, golemXZPosition;
    private enum GolemAttack { None, Smashing, Throwing }
    private enum GolemMoving { Attacking, Standing, Following }
    private GolemAttack golemAttack;
    private GolemMoving golemMoving;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        control = gameObject.GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");

        golemAttack = GolemAttack.None;
        currentSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        inRange = distance < followRange;
		stateChanged = 0;
        //Debug.Log("^" + golemAttack + " " + animator.GetInteger("attacking") + "  " + golemMoving + " at:" + distance);
        if (inRange)
        {
            if (distance < 10 && golemMoving == GolemMoving.Following)
            {
                golemMoving = GolemMoving.Standing;
                moveVector = Vector3.zero;
            }
            else if (distance > 11 && golemMoving == GolemMoving.Standing)
            {
                golemMoving = GolemMoving.Following;
            }
            if (!isEntering) //check palyer ever been out from range
            {
				hasBeenOut = true;
            }
        }
        else if (!inRange)
        {
            golemMoving = GolemMoving.Standing;
            golemAttack = GolemAttack.None;
            isEntering = false;
        }


        if (golemMoving == GolemMoving.Following)
        {
			stateChanged += 1;
            animator.SetInteger("walking", 1);
            moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
            golemXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
            playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(playerXZPosition - golemXZPosition), turnSpeed * Time.deltaTime);

            if (distance < smashRange && Time.time > smashCooldownCounter)
            {
                golemMoving = GolemMoving.Standing;
            }
            else if (distance < throwRange && distance > smashRange && Time.time > throwCooldownCounter && Time.time > smashCooldownCounter)
            {
                golemMoving = GolemMoving.Standing;
            }
            else //sonetime golemattack go another
            {
                golemAttack = GolemAttack.None;
                golemMoving = GolemMoving.Following;
            }
        }

        if (golemMoving == GolemMoving.Standing)
        {
			stateChanged += 1;
            animator.SetInteger("walking", 0);
            moveVector = Vector3.zero;
            currentSpeed = moveSpeed;

            if (inRange)
            {
                golemXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
                playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(playerXZPosition - golemXZPosition), turnSpeed * Time.deltaTime);
                golemMoving = GolemMoving.Following;
            }

            if (distance < smashRange && Time.time > smashCooldownCounter )
            {
                golemAttack = GolemAttack.Smashing;
                golemMoving = GolemMoving.Attacking;
                currentSmashTime = 0;
            }
            if (distance < throwRange && distance > smashRange && Time.time > throwCooldownCounter && Time.time > smashCooldownCounter)
            {
				if(hasBeenOut) //not instantly use skill while player in range
				{
					animator.SetInteger("attacking", 0);
					hasBeenOut = false;
					golemMoving = GolemMoving.Following;
					throwCooldownCounter = Time.time + throwCooldown;
				}
				else
				{
					golemAttack = GolemAttack.Throwing;
					golemMoving = GolemMoving.Attacking;
					currentThrowTime = 0;
				}
            }
        }

        if (golemMoving == GolemMoving.Attacking)
        {
			stateChanged += 1;
            if (golemAttack == GolemAttack.Throwing)
            {
                animator.SetInteger("attacking", 2);
                moveVector = Vector3.zero;
                currentThrowTime += 1;
                if (currentThrowTime > maxThrowTime)
                {
                    //throw rock 
                    golemAttack = GolemAttack.None;
                    golemMoving = GolemMoving.Standing;
                    currentThrowTime = 0;
                    animator.SetInteger("attacking", 0);
                    animator.SetInteger("walking", 0);
                    throwCooldownCounter = Time.time + throwCooldown;
                }
				else if(currentThrowTime < 80) //(currentThrowTime < (currentThrowTime*0.8)) but it return false
				{
					golemXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
					playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
					transform.rotation = Quaternion.Lerp(transform.rotation,
						Quaternion.LookRotation(playerXZPosition - golemXZPosition), turnSpeed * Time.deltaTime);
				}
            }

            else if (golemAttack == GolemAttack.Smashing)
            {
                animator.SetInteger("attacking", 1);
                moveVector = Vector3.zero;
                currentSmashTime += 1;
                if (currentSmashTime > maxSmashTime || !inRange)
                {
                    golemAttack = GolemAttack.None;
                    golemMoving = GolemMoving.Standing;
                    currentSmashTime = 0;
                    animator.SetInteger("attacking", 0);
                    animator.SetInteger("walking", 0);
                    smashCooldownCounter = Time.time + smashCooldown;
                }
            }
            else if (golemAttack == GolemAttack.None)
            {
                animator.SetInteger("attacking", 0);
                golemMoving = GolemMoving.Standing;
            }
        }

        if (inRange) { isEntering = true; }

		//Debug.Log("v" + golemAttack + " " + animator.GetInteger("attacking") + "  " + golemMoving + " at:" + distance + "  Changed " + stateChanged);
        moveVector.y -= gravity * Time.deltaTime;
        control.Move(moveVector);
    }
}
