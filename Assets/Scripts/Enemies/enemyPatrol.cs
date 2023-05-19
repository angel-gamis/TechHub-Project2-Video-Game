using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
	[Header ("Patrol Waypoints")]
	[SerializeField] private Transform leftEdge;
	[SerializeField] private Transform rightEdge;

	[Header("Movement Variables")]
	private bool movingRight = true;
	[SerializeField] private float speed = 2;
	private Vector2 movingRightScale = new Vector2(10, 10);
	private Vector2 movingLeftScale = new Vector2(-10,10);

	[Header ("Enemy")]
	[SerializeField] private Transform enemy;

	private void Update()
	{
		MoveInDirection();
	}

	private void MoveInDirection()
	{
		// Get the next waypoint
		if (movingRight)
		{
			// Flip the enemy to next waypoints direction
			enemy.localScale = movingRightScale;
			// Move in direction of waypoint
			enemy.position = new Vector2(enemy.position.x + speed, enemy.position.y);
			// Check if enemy is at or further than way point
			if (enemy.position.x >= rightEdge.position.x)
			{
				movingRight = false;
			}
		}
		if (!movingRight)
		{
			// Flip the enemy to next waypoints direction
			enemy.localScale = movingLeftScale;
			// Move in direction of waypoint
			enemy.position  = new Vector2(enemy.position.x - speed, enemy.position.y);
			// Check if enemy is at or further than way point
			if (enemy.position.x <= leftEdge.position.x)
			{
				// Switch waypoint
				movingRight = true;
			}
		}
	}
}
