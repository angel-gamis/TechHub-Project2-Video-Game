using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using System.Threading;

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
	public bool isWalking;
	private bool enemyAttacking;

	private void Update()
	{
		MoveInDirection();
		Debug.Log(enemyAttacking);
	}

	#region Switch Functions **

	private void SwitchToLeft()
	{
		// Flip the enemy to face left waypoints direction
		enemy.localScale = movingLeftScale;
		// Switch waypoint to the left waypoint
		movingRight = false;
	}
	private void SwitchToRight()
	{
		// Flip the enemy to face right waypoints direction
		enemy.localScale = movingRightScale;
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
				if (enemy.position.x >= rightEdge.position.x)
				{
					// Stop Skeleton Movement
					enemy.position = new Vector2(enemy.position.x, enemy.position.y);
					// Skeleton is not walking
					isWalking = false;
					// Delay Skeleton Flip and Switch of bool
					Invoke("SwitchToLeft", 2);

				}
				else
				{
					// Skeleton is walking
					isWalking = true;
					// Move in direction of waypoint
					enemy.position = new Vector2(enemy.position.x + speed, enemy.position.y);
				}
			}
		if (!movingRight)
			{
				// If player is at waypoint
				if (enemy.position.x <= leftEdge.position.x)
				{
					// Stop Skeleton Movement
					enemy.position = new Vector2(enemy.position.x, enemy.position.y);
					// Skeleton is not walking
					isWalking = false;
					// Delay Skeleton Flip and Switch of bool
					Invoke("SwitchToRight", 2);
				}
				else
				{
					// Skeleton is walking
					isWalking = true;
					// Move in direction of waypoint
					enemy.position = new Vector2(enemy.position.x - speed, enemy.position.y);
				}
			}
	}
}
