using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NguaAIController : MonoBehaviour {

	private Animator animator;
	public CharacterController control;
	public GameObject player;
	public Collider headCollider;

	public int followRange, attackRange, gravity;
	public float moveSpeed, turnSpeed, maxDashTime, cooldown = 1;
	private float distance, currentSpeed, currentDashTime, cooldownCounter;
	private bool inRange;
	private Vector3 moveVector, playerXZPosition, nguaXZPosition;
	private enum NguaAction {Neutral, Following, Punching, Dashing, TailAttack}
	private NguaAction nguaAction;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponentInChildren<Animator>();
		control = gameObject.GetComponent<CharacterController>();
		
		player = GameObject.FindWithTag("Player");
		nguaAction = NguaAction.Neutral;
		currentSpeed = moveSpeed;
		headCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = distance < followRange;
		if(inRange)
		{
			Debug.Log(nguaAction + " " + distance );
			if(nguaAction == NguaAction.Following)
			{
				animator.SetInteger("waking", 1);
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				nguaXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
				playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
				transform.rotation = Quaternion.Lerp(transform.rotation, 
						Quaternion.LookRotation(playerXZPosition - nguaXZPosition), turnSpeed * Time.deltaTime);
				
				if(distance < 6.5){ 
					moveVector = Vector3.zero;
				}

				if(distance < attackRange && Time.time > cooldownCounter)
				{
					int randnum = Random.Range(0, 3);
					switch (randnum)
					{
						case 0: 
							nguaAction = NguaAction.Dashing;
							currentSpeed += 40;
							currentDashTime = 0;
							break;
						case 1: 
							nguaAction = NguaAction.Punching;
							break;
						case 2: 
							nguaAction = NguaAction.TailAttack;
							break;
						default: nguaAction = NguaAction.Neutral; break;
					}
				}	
			}
			else if(nguaAction == NguaAction.Dashing)
			{
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				currentDashTime += 1;

				if(currentDashTime > maxDashTime){
					animator.SetInteger("dashing", 0);
					headCollider.enabled = false;
					currentSpeed -= 40;
					cooldownCounter = Time.time + cooldown;
					nguaAction = NguaAction.Following;
				}
				else if(currentDashTime+(maxDashTime*0.2)> maxDashTime){
					animator.SetInteger("dashing", 3);
					headCollider.enabled = false;
				}
				else if(currentDashTime < 10){
					animator.SetInteger("dashing", 1);
					moveVector = Vector3.zero;
					nguaXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
					playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
					transform.rotation = Quaternion.Lerp(transform.rotation, 
						Quaternion.LookRotation(playerXZPosition - nguaXZPosition), turnSpeed * Time.deltaTime);
				}
				else if(currentDashTime < maxDashTime){
					animator.SetInteger("dashing", 2);
					moveVector.y = ((player.transform.position.y-5) - transform.position.y) * Time.deltaTime;
					headCollider.enabled = true;
				}
			}
			else if(nguaAction == NguaAction.Punching)
			{
				animator.SetInteger("attackMove", 1);
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Ngua_attack"))
				{
					animator.SetInteger("attackMove", 0);
					nguaAction = NguaAction.Following;
					cooldownCounter = Time.time + cooldown;
				}
			}
			else if(nguaAction == NguaAction.TailAttack)
			{
				animator.SetInteger("attackMove", 2);
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Ngua_attack3"))
				{
					animator.SetInteger("attackMove", 0);
					nguaAction = NguaAction.Following;
					cooldownCounter = Time.time + cooldown;
				}
			}
			else if(nguaAction == NguaAction.Neutral)
			{
				nguaAction = NguaAction.Following;
			}
		}
		else if(!inRange)
		{
			nguaAction = NguaAction.Neutral;
			currentDashTime = 0;
			animator.SetInteger("waking", 0);
			moveVector = Vector3.zero;
			currentSpeed = moveSpeed;
		}
		moveVector.y -= gravity * Time.deltaTime;
		control.Move(moveVector);
	}
}
