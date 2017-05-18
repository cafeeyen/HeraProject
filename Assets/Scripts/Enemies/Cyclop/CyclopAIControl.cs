using UnityEngine;

public class CyclopAIControl : MonoBehaviour
{
    public CharacterController control;
    public Collider headCollider, clubCollider;
    public int gravity, followRange, hitRange, dashRange, dashForce;
    public float moveSpeed, turnSpeed, maxDashTime, maxHitTime;

    private float currentDashTime = 0, currentHitTime = 0, currentSpeed,
        dashCoolDownCounter, hitCoolDownCounter, dashCoolDown = 8, hitCoolDown = 1, skillCoolDownCounter, skillCoolDown = 1;
    private bool isColliding = false, inRange = false;
    private string action = "";

    private GameObject player;
    private Animator animator;
    private static Cyclop status;
    private Vector3 moveVector, playerRotation, monsterRotation;

    public enum CyclopAction { None, Hitting, Dashing }
    private enum CyclopMoving { Attacking, Standing, Walking }
    private CyclopAction cyclopAction;
    private CyclopMoving cyclopMoving;

    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        control = gameObject.GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
        currentSpeed = moveSpeed;
        cyclopAction = CyclopAction.None;
        cyclopMoving = CyclopMoving.Standing;
        animator.SetInteger("attacking", 0);
        status = new Cyclop(1);
        headCollider.enabled = false;
        clubCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);
        inRange = distance < followRange;

        if (inRange)
        {
            if (distance < 4 && cyclopMoving != CyclopMoving.Attacking)
            {
                cyclopMoving = CyclopMoving.Standing;
            }
            else if (cyclopMoving == CyclopMoving.Standing)
            {
                cyclopMoving = CyclopMoving.Walking;
            }
        }
        else if (!inRange)
        {
            cyclopMoving = CyclopMoving.Standing;
        }

        if (cyclopMoving == CyclopMoving.Attacking)
        {
            if (cyclopAction == CyclopAction.Dashing)
            {
                moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
                animator.SetInteger("attacking", 2);
                currentDashTime += 1;
                if (currentDashTime > maxDashTime || isColliding)
                {
                    currentDashTime = 0;
                    currentSpeed -= 30;
                    cyclopAction = CyclopAction.None;
                    cyclopMoving = CyclopMoving.Walking;
                    skillCoolDownCounter = Time.time + skillCoolDown;
                    headCollider.enabled = false;
                }
            }
            else if (cyclopAction == CyclopAction.Hitting)
            {
                animator.SetInteger("hitting", 1);
                moveVector = Vector3.zero;
                currentHitTime += 1;
                if (currentHitTime > maxHitTime)
                {
                    currentHitTime = 0;
                    animator.SetInteger("hitting", 0);
                    animator.SetInteger("attacking", 0);
                    cyclopAction = CyclopAction.None;
                    cyclopMoving = CyclopMoving.Walking;
                    clubCollider.enabled = false;
                }
            }
        }

        else if (cyclopMoving == CyclopMoving.Walking)
        {
            animator.SetInteger("attacking", 1);
            moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
            monsterRotation = new Vector3(transform.position.x, 0, transform.position.z);
            playerRotation = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(playerRotation - monsterRotation), turnSpeed * Time.deltaTime);
            if (distance < hitRange && Time.time > hitCoolDownCounter && Time.time > skillCoolDownCounter)
            {
                hitCoolDownCounter = Time.time + hitCoolDown;
                currentHitTime = 0;
                cyclopAction = CyclopAction.Hitting;
                cyclopMoving = CyclopMoving.Attacking;
                clubCollider.enabled = true;
            }
            else if (distance < dashRange && Time.time > dashCoolDownCounter && Time.time > skillCoolDownCounter && Time.time + 1.0f > hitCoolDownCounter)
            {
                dashCoolDownCounter = Time.time + dashCoolDown;
                currentSpeed += 30;
                currentDashTime = 0;
                cyclopAction = CyclopAction.Dashing;
                cyclopMoving = CyclopMoving.Attacking;
                headCollider.enabled = true;
            }
        }

        else if (cyclopMoving == CyclopMoving.Standing)
        {
            animator.SetInteger("attacking", 0);
            currentSpeed = moveSpeed;
            moveVector = Vector3.zero;

            if (distance < hitRange && Time.time > hitCoolDownCounter && Time.time > skillCoolDownCounter)
            {
                hitCoolDownCounter = Time.time + hitCoolDown;
                currentHitTime = 0;
                cyclopAction = CyclopAction.Hitting;
                cyclopMoving = CyclopMoving.Attacking;
                clubCollider.enabled = true;
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

    public CyclopAction gsCyclopAction
    {
        get { return cyclopAction; }
        set { cyclopAction = value; }
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
}
