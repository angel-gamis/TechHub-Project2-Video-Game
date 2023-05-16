using UnityEngine;

public class skeletonController : MonoBehaviour
{
	[SerializeField] private float attackCooldown;
	[SerializeField] private int damage;
	[SerializeField] private BoxCollider2D boxCollider;
	[SerializeField] private LayerMask playerLayer;
	
	private float attackCooldownTimer = Mathf.Infinity;

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
				attackCooldownTimer = 0;
			}

		}
	}

	private bool PlayerInSight()
	{
		// Check if player is in the skeletons sight (position, size, angle, direction, distance, layerMask)
		RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left);
		// temp
		return false;
	}
}
