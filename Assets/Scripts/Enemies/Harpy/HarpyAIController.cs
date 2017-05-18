using UnityEngine;

public class HarpyAIController : MonoBehaviour
{

	
	public CharacterController control;
    public Collider headCollider;
    public int followRange, dashRange;
	public float height, moveSpeed, turnSpeed, maxDashTime, dashCooldown = 10;

	private float distance, currentSpeed, currentDashTime, dashCooldownCounter;
	private bool inRange, isColliding;
    private string action = "";

    private GameObject player;
    private Animator animator;
    private static Harpy status;
    private Vector3 moveVector, playerXZPosition, harpyXZPosition;

	private enum HarpyAction {Neutral, Following, Dashing}
	private HarpyAction harpyAction;
	

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponentInChildren<Animator>();
		control = gameObject.GetComponent<CharacterController>();
		player = GameObject.FindWithTag("Player");
		currentSpeed = moveSpeed;
        status = new Harpy(1);
        // For check Ngua status each Lv.
        //Debug.Log(status.LV + " " + status.ATK + " " + status.DEF + " " + status.CurHP + "/" +status.HP);

        headCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = distance < followRange;
		moveVector.y = (height - transform.position.y)*Time.deltaTime; //slowly move to default height

		if(harpyAction == HarpyAction.Neutral && inRange){
			animator.SetInteger("flying", 0);
			harpyAction = HarpyAction.Following;
		}
		if(inRange){
			if(harpyAction == HarpyAction.Following){
				animator.SetInteger("flying", 0);
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				harpyXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
				playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
				transform.rotation = Quaternion.Lerp(transform.rotation, 
					Quaternion.LookRotation(playerXZPosition - harpyXZPosition), turnSpeed * Time.deltaTime);
						moveVector.y = (height - transform.position.y)*Time.deltaTime; 
				if(distance < 2)
					moveVector = Vector3.zero;		
				
				if(distance < dashRange && Time.time > dashCooldownCounter)
                {
                    harpyAction = HarpyAction.Dashing;
					currentDashTime = 0;
					currentSpeed += 40;
                    headCollider.enabled = true;
                }
			}
			else if(harpyAction == HarpyAction.Dashing)
            {
				moveVector = transform.TransformDirection(Vector3.forward) * currentSpeed * Time.deltaTime;
				currentDashTime += 1;
				
				if(currentDashTime > maxDashTime || isColliding)
                {
					animator.SetInteger("flying", 0);
					currentSpeed -= 40;
					dashCooldownCounter = Time.time + dashCooldown;
					harpyAction = HarpyAction.Following;
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
				else if(currentDashTime < maxDashTime){
					animator.SetInteger("flying", 2);
					moveVector.y = ((player.transform.position.y-5) - transform.position.y) * Time.deltaTime;
				}
			}
		}
		else if(!inRange){
			animator.SetInteger("flying", 0);
			currentSpeed = moveSpeed;
			harpyAction = HarpyAction.Neutral;
			moveVector = Vector3.zero;
		}
		control.Move(moveVector);

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
