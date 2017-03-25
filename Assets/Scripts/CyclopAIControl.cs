using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopAIControl : MonoBehaviour {

	 private Animator animator;
	 public CharacterController control;
	 public GameObject player;
 
     public int gravity, followRange, hitRange, dashRange;
	 public float moveSpeed, turnSpeed, maxDashTime, maxHitTime;

	 private Vector3 moveVector, playerRotation, monsterRotation;
	 private bool isColliding = false, inRange =false;
	 private float currentDashTime = 0, currentHitTime = 0, currentSpeed, dashCoolDownCounter, hitCoolDownCounter, dashCoolDown = 8, hitCoolDown = 1;
	 private enum CyclopAction {Neutral, Attacking, Hitting, Dashing}
	 private CyclopAction cyclopAction;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponentInChildren<Animator>();
		control = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindWithTag("Player");
		currentSpeed = moveSpeed;
		cyclopAction = CyclopAction.Neutral;
		animator.SetInteger("attacking", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		float distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = distance < followRange;

		if(cyclopAction == CyclopAction.Neutral && inRange){
			animator.SetInteger("attacking", 1);
			cyclopAction = CyclopAction.Attacking;
		}
		
		if(inRange){
			///Debug.Log(cyclopAction);
			if(cyclopAction == CyclopAction.Hitting){
				animator.SetInteger("hitting", 1);
				moveVector = Vector3.zero;
				currentHitTime += 1;
				if(currentHitTime > maxHitTime){
					currentHitTime = 0;
					animator.SetInteger("hitting", 0);
					animator.SetInteger("attacking", 1);
					cyclopAction = CyclopAction.Attacking;
				}
			}
			else if(cyclopAction == CyclopAction.Dashing){
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				animator.SetInteger("attacking", 2);
				currentDashTime += 1;
				if(currentDashTime > maxDashTime){ //|| isColliding
					currentDashTime = 0;
					currentSpeed -= 30;
					cyclopAction = CyclopAction.Attacking;
				}
			}
			else if(cyclopAction == CyclopAction.Attacking){
				animator.SetInteger("attacking", 1);
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				monsterRotation = new Vector3(transform.position.x, 0, transform.position.z);
				playerRotation = new Vector3(player.transform.position.x, 0, player.transform.position.z);
				transform.rotation = Quaternion.Slerp(transform.rotation, 
					Quaternion.LookRotation(playerRotation - monsterRotation), turnSpeed * Time.deltaTime);
				if(distance < 1){ //in normal mode, very near cant walk
					moveVector = Vector3.zero;
				}
				if(distance < hitRange && Time.time > hitCoolDownCounter){
					hitCoolDownCounter = Time.time + hitCoolDown;
					currentHitTime = 0;
					cyclopAction = CyclopAction.Hitting;
				}
				else if(distance < dashRange && Time.time > dashCoolDownCounter){
					dashCoolDownCounter = Time.time + dashCoolDown;
					currentSpeed += 30;
					currentDashTime = 0;
					cyclopAction = CyclopAction.Dashing;
				}
			}			
		}
		else if(!inRange){
			animator.SetInteger("attacking", 0);
			currentSpeed = moveSpeed;
			cyclopAction = CyclopAction.Neutral;
			moveVector = Vector3.zero;
		}
		moveVector.y -= gravity * Time.deltaTime;
		control.Move(moveVector);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if(hit.gameObject.tag == "Player" || hit.gameObject.tag == "Wall"){
			isColliding = true;
		}
		else{
			isColliding = false;
		}
    }
}
