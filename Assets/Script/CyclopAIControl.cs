using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopAIControl : MonoBehaviour {

	 //public GameObject target;
	 private Animator animator;
	 public CharacterController control;
	 public GameObject player;
 
     public int followRange;
	 public int hitRange;
	 public int dashRange;
	 public float moveSpeed, maxDashTime, maxHitTime;

	 private Vector3 moveVector;
	 private int cyclopSkill = 1;
	 private bool dashFlag = false, inRange =false;
	 private float currentDashTime = 0, currentHitTime = 0, currentSpeed;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponentInChildren<Animator>();
		control = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindWithTag("Player");
		currentSpeed = moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
		float distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = distance < followRange;

		if(animator.GetInteger("attacking") == 0 && distance < followRange){
			animator.SetInteger("attacking", 1);
		}
		
		if(inRange){
			//Debug.Log(distance + " " + animator.GetInteger("attacking"));
			if(animator.GetInteger("attacking") == 1){
				transform.LookAt(new Vector3( player.transform.position.x, 0, player.transform.position.z ));
				moveVector = new Vector3(Vector3.forward.x * Time.deltaTime * currentSpeed, 0, Vector3.forward.z * Time.deltaTime * currentSpeed);

				if(cyclopSkill == 1){
					if(distance < dashRange && currentDashTime == 0){
						animator.SetInteger("attacking", 2);
						control.Move(new Vector3(0, 2, 0));
						currentSpeed += 15;
						Debug.Log(" Dashhhh-- ");
					}
					if(currentDashTime != 0){
						currentDashTime = 0;
					}
				}
				
				if(cyclopSkill == 2){
					
					if(distance < hitRange){
						Debug.Log(" POK! ");
						moveVector = Vector3.zero;
						animator.SetInteger("hitting", 1);
						currentHitTime += 1;
					}
					if(currentHitTime < maxHitTime && currentHitTime > 0){
						currentHitTime += 1;
						moveVector = Vector3.zero;
					}
					if(currentHitTime > maxHitTime){
						animator.SetInteger("hitting", 0);
						currentHitTime = 0;
						currentDashTime = 0;
						cyclopSkill = 1;
					}
				}
				transform.Translate(moveVector);
			}
			else if(animator.GetInteger("attacking") == 2){
				transform.Translate(Vector3.forward.x * Time.deltaTime * currentSpeed, 0, Vector3.forward.z * Time.deltaTime * currentSpeed);
				currentDashTime += 1;
				if(currentDashTime > maxDashTime ){
					animator.SetInteger("attacking", 1);
					currentSpeed -= 15;
					currentDashTime = 0;
					cyclopSkill = 2;
				}
			}
			
			
		}
		else{
			animator.SetInteger("attacking", 0);
			currentSpeed = moveSpeed;
		}
		
	}
}
