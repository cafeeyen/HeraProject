using UnityEngine;

public class HarpyRedAIController : MonoBehaviour
{

	
	public CharacterController control;
    public Collider headCollider;
    public int followRange, dashRange, swipeRange;
	public float height, moveSpeed, turnSpeed, maxDashTime, maxSwipeTime, dashCooldown = 10, swipeCooldown = 3;

	private float distance, currentSpeed, currentDashTime, currentSwipeTime, dashCooldownCounter, swipeCooldownCounter;
	private bool inRange, isColliding;
    private string action = "";

    private GameObject player;
    private Animator animator;
    private static HarpyRed status;
    private Vector3 moveVector, playerXZPosition, harpyXZPosition;

	private enum HarpyRedAction {Neutral, Following, Dashing, Swiping}
	private HarpyRedAction harpyRedAction;
	

	// Use this for initialization
	void Start ()
    {
		animator = gameObject.GetComponentInChildren<Animator>();
		control = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindWithTag("Player");
		currentSpeed = moveSpeed;
        status = new HarpyRed(1);
        // For check Ngua status each Lv.
        //Debug.Log(status.LV + " " + status.ATK + " " + status.DEF + " " + status.CurHP + "/" +status.HP);

        headCollider.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
		distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = distance < followRange;
		moveVector.y = (height - transform.position.y)*Time.deltaTime; //slowly move to default height

		if(harpyRedAction == HarpyRedAction.Neutral && inRange)
        {
			animator.SetInteger("flying", 0);
			harpyRedAction = HarpyRedAction.Following;
		}

		if(inRange)
        {
			if(harpyRedAction == HarpyRedAction.Following)
            {
				animator.SetInteger("flying", 0);
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				harpyXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
				playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
				transform.rotation = Quaternion.Lerp(transform.rotation, 
					Quaternion.LookRotation(playerXZPosition - harpyXZPosition), turnSpeed * Time.deltaTime);
						moveVector.y = (height - transform.position.y)*Time.deltaTime; 
				if(distance < 2)
					moveVector = Vector3.zero;			
				
				if(distance < swipeRange && Time.time > swipeCooldownCounter)
                {
					harpyRedAction = HarpyRedAction.Swiping;
					currentSwipeTime = 0;
				}
				else if(distance < dashRange && Time.time > dashCooldownCounter)
                {
                    harpyRedAction = HarpyRedAction.Dashing;
					currentDashTime = 0;
					currentSpeed += 40;
                    headCollider.enabled = true;
                }
			}
			else if(harpyRedAction == HarpyRedAction.Dashing)
			{
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				currentDashTime += 1;
				
				if(currentDashTime > maxDashTime || isColliding)
				{
					animator.SetInteger("flying", 0);
					currentSpeed -= 40;
					dashCooldownCounter = Time.time + dashCooldown;
					harpyRedAction = HarpyRedAction.Following;
                    headCollider.enabled = false;
                    isColliding = false;
                }
				else if(currentDashTime+(maxDashTime*0.2)> maxDashTime)
				{
					animator.SetInteger("flying", 3);
				}
				else if(currentDashTime < 20)
				{
					animator.SetInteger("flying", 1);
					moveVector = Vector3.zero;
					harpyXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
					playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
					transform.rotation = Quaternion.Lerp(transform.rotation, 
						Quaternion.LookRotation(playerXZPosition - harpyXZPosition), turnSpeed * Time.deltaTime);
				}
				else if(currentDashTime < maxDashTime)
				{
					animator.SetInteger("flying", 2);
					moveVector.y = ((player.transform.position.y-5) - transform.position.y) * Time.deltaTime;
				}
			}

			else if(harpyRedAction == HarpyRedAction.Swiping)
			{
				currentSwipeTime += 1;
				moveVector = Vector3.zero;
				animator.SetInteger("swiping", 1);
				if(currentSwipeTime > maxSwipeTime)
				{
					animator.SetInteger("swiping", 0);
					swipeCooldownCounter = Time.time + swipeCooldown;
					harpyRedAction = HarpyRedAction.Following;
				}
			}
		}


		else if(!inRange)
        {
			animator.SetInteger("flying", 0);
			currentSpeed = moveSpeed;
			harpyRedAction = HarpyRedAction.Neutral;
			moveVector = Vector3.zero;
		}
		control.Move(moveVector);

        // Attack-Damage Zone
        if (!action.Equals(""))
        {
            DamageSystem.DamageToPlayer(status.ATK, action);
            action = "";
        }
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
