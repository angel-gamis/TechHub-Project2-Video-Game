using UnityEngine;

public class skeletonController : MonoBehaviour
{
	[Header("** Skeleton Animator")]
	private Animator anim;

	[Header ("** Skeleton Attack Variables")]
	[SerializeField] private float attackCooldown;
	[SerializeField] private float range;
	[SerializeField] private float colliderDistance;
	[SerializeField] private int damage;
	[SerializeField] private BoxCollider2D boxCollider;
	private float attackCooldownTimer = Mathf.Infinity;

	[Header("** Skeleton Status Variables")]
	// Skeleton Status Variables
	private bool isWalking;
	public bool isAttacking;

	[Header("Movement Variables")]
	private bool movingRight = true;
	[SerializeField] private float speed = .12f;
	private Vector2 movingRightScale = new Vector2(10, 10);
	private Vector2 movingLeftScale = new Vector2(-10, 10);

	[Header("Patrol Waypoints")]
	[SerializeField] private Transform leftEdge;
	[SerializeField] private Transform rightEdge;

	[Header("** Outside Variables")]
	[SerializeField] enemyPatrol enemyPatrol;
	[SerializeField] healthBar playerHealthBar;
	[SerializeField] private LayerMask playerLayer;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		isAttacking = false;

		// Update Timer
		attackCooldownTimer += Time.deltaTime;

		// Attack only if the player is in skeletons range of sight
		if (PlayerInSight())
		{
			// Attack if cooldown is finished
			if (attackCooldownTimer > attackCooldown)
			{
				transform.position = new Vector2(transform.position.x, transform.position.y);
				isAttacking = true;
				// Attack Animation
				anim.SetTrigger("attack_1");
				// Delay Damage to hit a little bit into te skeleton animation
				Invoke("DamagePlayer", .5f);
				// Reset attack cooldown
				attackCooldownTimer = 0;
			}

		}
		if (isAttacking == false)
		{
			MoveInDirection();
		}

		// ** Animation **
		anim.SetBool("isWalking", isWalking);
	}

	// Checks to see if player is in sight then do --
	private bool PlayerInSight()
	{
		// Check if player is in the skeletons sight (position, size, angle, direction, distance, layerMask)
		RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

		// Set to bool to true
		return hit.collider != null;
	}

	// Draws the area in which the skeleton can see the player
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y));
	}
	
	// Damage the player
	private void DamagePlayer()
	{
		// If player still in skeleton range
		if (PlayerInSight())
		{
			GameManager.gameManager.PlayerDamager(damage);
		}
	}

	#region Switch Functions **

	private void SwitchToLeft()
	{
		// Flip the enemy to face left waypoints direction
		transform.localScale = movingLeftScale;
		// Switch waypoint to the left waypoint
		movingRight = false;
	}
	private void SwitchToRight()
	{
		// Flip the enemy to face right waypoints direction
		transform.localScale = movingRightScale;
		// Switch waypoint to the right waypoint
		movingRight = true;
	}
	#endregion

	private void MoveInDirection()
	{
		// Get the next waypoint
		if (movingRight)
		{
			// If player is at waypoint
			if (transform.position.x >= rightEdge.position.x)
			{
				// Stop Skeleton Movement
				transform.position = new Vector2(transform.position.x, transform.position.y);
				// Skeleton is not walking
				isWalking = false;
				// Delay Skeleton Flip and Switch of bool
				Invoke("SwitchToLeft", 2);

			}
			else if(!isAttacking)
			{
				// Skeleton is walking
				isWalking = true;
				// Move in direction of waypoint
				transform.position = new Vector2(transform.position.x + speed, transform.position.y);
			}
			else
			{
				isWalking = false;
				transform.position = new Vector2(transform.position.x, transform.position.y);
			}
		}
		if (!movingRight)
		{
			// If player is at waypoint
			if (transform.position.x <= leftEdge.position.x)
			{
				// Stop Skeleton Movement
				transform.position = new Vector2(transform.position.x, transform.position.y);
				// Skeleton is not walking
				isWalking = false;
				// Delay Skeleton Flip and Switch of bool
				Invoke("SwitchToRight", 2);
			}
			else if(!isAttacking)
			{
				// Skeleton is walking
				isWalking = true;
				// Move in direction of waypoint
				transform.position = new Vector2(transform.position.x - speed, transform.position.y);
			}
			else
			{
				isWalking = false;
				transform.position = new Vector2(transform.position.x, transform.position.y);
			}
		}
	}
}
