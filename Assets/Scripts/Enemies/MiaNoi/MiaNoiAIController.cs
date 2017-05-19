using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiaNoiAIController : MonoBehaviour {

	public CharacterController control;

	public int followRange, atk1Range = 15, atk2Range = 15, atk3Range = 50, dashRange = 80, gravity = 50;
    public float moveSpeed, turnSpeed, maxDashTime;
	public float atk1Cooldown = 1.55f, atk2Cooldown = 3.5f, atk3Cooldown = 3.5f, dashCooldown = 8.0f;

	private int maxAtk1Time = 50, maxAtk2Time = 50, maxAtk3Time = 50;
    private float distance, currentSpeed;
	private float atk1Time, atk2Time, atk3Time, currentDashTime;
	private float atk1CooldownCounter, atk2CooldownCounter, atk3CooldownCounter, dashCooldownCounter;
    private bool inRange, isColliding = false, enableBoss = false;

	private string action = "";

	private GameObject player;
    private Animator animator;
	private static MiaNoi status;
	public Collider leftHandCollider, panCollider;
	private Vector3 moveVector, playerXZPosition, miaNoiXZPosition;

	private enum MiaNoiAction { None, Atk1, Atk2, Atk3, Screem, Dashing }
    private enum MiaNoiMoving { Neutral, Following, Attacking }
    private MiaNoiAction miaNoiAction;
    private MiaNoiMoving miaNoiMoving;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponentInChildren<Animator>();
        control = gameObject.GetComponent<CharacterController>();
		status = new MiaNoi(Mathf.Min(20, GameData.data.lv + 3));

        player = GameObject.FindWithTag("Player");
		currentSpeed = moveSpeed;
		miaNoiMoving = MiaNoiMoving.Neutral;
		leftHandCollider.enabled = false;
		panCollider.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, player.transform.position);

		if(enableBoss)
        {
            if (distance < 12 && miaNoiMoving == MiaNoiMoving.Following)
            {
                miaNoiMoving = MiaNoiMoving.Neutral;
                currentDashTime = 0;
                animator.SetInteger("Walking", 0);
                moveVector = Vector3.zero;
                currentSpeed = moveSpeed;
            }
            else if(distance > 15 && miaNoiMoving == MiaNoiMoving.Neutral)
            {
                miaNoiMoving = MiaNoiMoving.Following;
            }
        }

		//process move
		if(miaNoiMoving == MiaNoiMoving.Attacking)
		{
			if(miaNoiAction == MiaNoiAction.Dashing)
			{
				animator.SetInteger("Dashing", 1);
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
                currentDashTime += 1;
				leftHandCollider.enabled = true;
				if (currentDashTime > maxDashTime || isColliding)
                {
					
                    animator.SetInteger("Dashing", 0);
                    isColliding = false;
                    leftHandCollider.enabled = false;
                    currentSpeed -= 60;
                    dashCooldownCounter = Time.time + dashCooldown;
                    miaNoiMoving = MiaNoiMoving.Following;
                    miaNoiAction = MiaNoiAction.None;
                }
			}

			else if(miaNoiAction == MiaNoiAction.Atk1)
			{
				animator.SetInteger("AttackMove", 1);
				atk1Time += 1;
				panCollider.enabled = true;

				if (atk1Time > maxAtk1Time)
                {
                    animator.SetInteger("AttackMove", 0);
                    miaNoiMoving = MiaNoiMoving.Following;
                    miaNoiAction = MiaNoiAction.None;
                    atk1CooldownCounter = Time.time + atk1Cooldown;
                    panCollider.enabled = false;
                    atk1Time = 0;
                }
			}

			else if(miaNoiAction == MiaNoiAction.Atk2)
			{
				animator.SetInteger("AttackMove", 2);
				atk2Time += 1;
				panCollider.enabled = true;
				moveVector = Vector3.zero;
				if (atk2Time > maxAtk2Time)
                {
                    animator.SetInteger("AttackMove", 0);
                    miaNoiMoving = MiaNoiMoving.Following;
                    miaNoiAction = MiaNoiAction.None;
                    atk2CooldownCounter = Time.time + atk2Cooldown;
                    panCollider.enabled = false;
                    atk2Time = 0;
                }
			}

			else if(miaNoiAction == MiaNoiAction.Atk3)
			{
				animator.SetInteger("AttackMove", 3);
				atk3Time += 1;
				panCollider.enabled = true;

				if (atk3Time > maxAtk3Time)
                {
                    animator.SetInteger("AttackMove", 0);
                    miaNoiMoving = MiaNoiMoving.Following;
                    miaNoiAction = MiaNoiAction.None;
                    atk3CooldownCounter = Time.time + atk3Cooldown;
                    panCollider.enabled = false;
                    atk3Time = 0;
                }
			}

			else if(miaNoiAction == MiaNoiAction.Screem)
			{
				animator.SetInteger("AttackMove", 4);
				moveVector = Vector3.zero;
				if (animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Mianoi_scream"))
				{
					animator.SetInteger("AttackMove", 0);
                    miaNoiMoving = MiaNoiMoving.Following;
                    miaNoiAction = MiaNoiAction.None;
				}
			}
		}
		
		else if(miaNoiMoving == MiaNoiMoving.Following)
		{
			animator.SetInteger("Walking", 1);
			currentSpeed = moveSpeed;
			currentDashTime = 0;
            moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
            miaNoiXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
            playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(playerXZPosition - miaNoiXZPosition), turnSpeed * Time.deltaTime);

			//condition to attack
			if (distance < atk1Range && Time.time > atk1CooldownCounter)
            {
                miaNoiAction = MiaNoiAction.Atk1;
                miaNoiMoving = MiaNoiMoving.Attacking;
                atk1Time = 0;
            }
			else if (distance < atk2Range && Time.time > atk2CooldownCounter)
            {
                miaNoiAction = MiaNoiAction.Atk2;
                miaNoiMoving = MiaNoiMoving.Attacking;
                atk2Time = 0;
            }
			else if (distance < atk3Range && Time.time > atk3CooldownCounter)
            {
                miaNoiAction = MiaNoiAction.Atk3;
                miaNoiMoving = MiaNoiMoving.Attacking;
                atk3Time = 0;
            }
			else if (distance < dashRange && distance > atk3Range && Time.time > dashCooldownCounter ) 
            {
                currentSpeed += 60;
                currentDashTime = 0;
                miaNoiAction = MiaNoiAction.Dashing;
                miaNoiMoving = MiaNoiMoving.Attacking;
            }
		}

		else if(miaNoiMoving == MiaNoiMoving.Neutral)
		{
			animator.SetInteger("Walking", 0);
            moveVector = Vector3.zero;
			currentSpeed = moveSpeed;
			miaNoiXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
            playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(playerXZPosition - miaNoiXZPosition), turnSpeed * Time.deltaTime);
			
			if (distance < atk1Range && Time.time > atk1CooldownCounter)
            {
                miaNoiAction = MiaNoiAction.Atk1;
                miaNoiMoving = MiaNoiMoving.Attacking;
                atk1Time = 0;
            }
			else if (distance < atk2Range && Time.time > atk2CooldownCounter)
            {
                miaNoiAction = MiaNoiAction.Atk2;
                miaNoiMoving = MiaNoiMoving.Attacking;
                atk2Time = 0;
            }
			else if (distance < atk3Range && Time.time > atk3CooldownCounter)
            {
                miaNoiAction = MiaNoiAction.Atk3;
                miaNoiMoving = MiaNoiMoving.Attacking;
                atk3Time = 0;
            }
		}

		moveVector.y -= gravity * Time.deltaTime;
        control.Move(moveVector);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "Wall")
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }

        // Attack-Damage Zone
        if (!action.Equals(""))
        {
            DamageSystem.DamageToPlayer(status.ATK, action);
            action = "";
        }
        if (status.CurHP <= 0)
        {
            status.Alive = false;
            Destroy(gameObject);
        }
    }

	public void enableBossMoving()
	{
        enableBoss = true;
		miaNoiMoving = MiaNoiMoving.Following;
	}

	public bool IsColliding
    {
        get { return isColliding; }
        set { isColliding = value; }
    }

    public string Action
    {
        get { return action; }
        set { action = value; }
    }

    public MiaNoi Status
    {
        get { return status; }
        set { status = value; }
    }
}

