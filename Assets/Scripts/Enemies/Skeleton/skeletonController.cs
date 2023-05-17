using UnityEngine;

public class skeletonController : MonoBehaviour
{
	private Animator anim;
	[SerializeField] private float attackCooldown;
	[SerializeField] private float range;
	[SerializeField] private int damage;
	[SerializeField] private BoxCollider2D boxCollider;
	[SerializeField] healthBar playerHealthBar;
	[SerializeField] private LayerMask playerLayer;

	// Skeleton Status Variables
	private bool isWalking;
	
	private float attackCooldownTimer = Mathf.Infinity;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		
		attackCooldownTimer += Time.deltaTime;

		// Attack only if the player is in skeletons range of sight
		if (PlayerInSight())
		{
			// Attack if cooldown is finished
			if (attackCooldownTimer > attackCooldown)
			{
				// Attack
				Debug.Log("Attack");
				anim.SetTrigger("attack_1");
				attackCooldownTimer = 0;
			}

		}

		// ** Animation **
		anim.SetBool("isWalking", isWalking);
	}

	// Checks to see if player is in sight then do --
	private bool PlayerInSight()
	{
		// Check if player is in the skeletons sight (position, size, angle, direction, distance, layerMask)
		RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center * transform.localScale.x, boxCollider.bounds.size, 0, Vector2.left, 0, playerLayer);

		return hit.collider != null;
	}

	// Draws the area in which the skeleton can see the player
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
	}
}
