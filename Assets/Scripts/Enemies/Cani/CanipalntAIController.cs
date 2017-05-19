using UnityEngine;

public class CanipalntAIController : MonoBehaviour {

	
	public CharacterController control;
	public int lookingRange, attackRange, turnSpeed;

	private float attackCooldown = 1;
	private float distance, attackCooldownCounter;
	private bool inRange;
    private string action = "";

    private GameObject player;
    private Animator animator;
    private static Cani status;
    private Vector3 playerXZPosition, caniplantXZPosition;

	private enum CaniplantAction {Neutral, Looking, Biting, Hitting}
	private CaniplantAction caniplantAction;

	// Use this for initialization
	void Start ()
    {
		animator = gameObject.GetComponentInChildren<Animator>();
		control = gameObject.GetComponent<CharacterController>();
		
		player = GameObject.FindWithTag("Player");
        status = new Cani(Random.Range(Mathf.Max(1, GameData.data.lv - 3), Mathf.Min(20, GameData.data.lv + 3)));
        // For check Ngua status each Lv.
        //Debug.Log(status.LV + " " + status.ATK + " " + status.DEF + " " + status.CurHP + "/" +status.HP);

        caniplantAction = CaniplantAction.Neutral;
	}
	
	// Update is called once per frame
	void Update ()
    {
		distance = Vector3.Distance(transform.position, player.transform.position);
		inRange = distance < lookingRange;
		if(inRange)
		{
			if(caniplantAction == CaniplantAction.Looking)
			{
				animator.SetInteger("attacking", 0);
				caniplantXZPosition = new Vector3(transform.position.x, 0, transform.position.z);
				playerXZPosition = new Vector3(player.transform.position.x, 0, player.transform.position.z);
				transform.rotation = Quaternion.Lerp(transform.rotation, 
						Quaternion.LookRotation(playerXZPosition - caniplantXZPosition), turnSpeed * Time.deltaTime);
				
				if(distance < attackRange && Time.time > attackCooldownCounter)
				{
					int randnum = Random.Range(0, 2);
					if(randnum == 0)
					{
						caniplantAction = CaniplantAction.Hitting;
					}
					else if(randnum == 1)
					{
						caniplantAction = CaniplantAction.Biting;
					}
				}
				
			}
			else if(caniplantAction == CaniplantAction.Hitting)
			{
				animator.SetInteger("attacking", -1);
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|caniplant_attack2"))
				{
					caniplantAction = CaniplantAction.Looking;
					attackCooldownCounter = Time.time + attackCooldown;
				}
			}
			else if(caniplantAction == CaniplantAction.Biting)
			{
				animator.SetInteger("attacking", 1);
				if(animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|caniplant_attack"))
				{
					caniplantAction = CaniplantAction.Looking;
					attackCooldownCounter = Time.time + attackCooldown;
				}
			}
			else if(caniplantAction == CaniplantAction.Neutral)
			{
				caniplantAction = CaniplantAction.Looking;
			}
			
		}
		else if(!inRange)
		{
			caniplantAction = CaniplantAction.Neutral;
			animator.SetInteger("attacking", 0);
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

    public string Action
    {
        get { return action; }
        set { action = value; }
    }

    public Cani Status
    {
        get { return status; }
        set { status = value; }
    }
}
