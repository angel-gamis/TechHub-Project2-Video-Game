using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;

public class playerController : MonoBehaviour
{
	private Animator anim;
	[SerializeField] playerHealthBar healthBar;
	[SerializeField] GameObject deathMenu;

	// Player Inputs
	private float xMove;
	private float yMove;

	// Player Variables
	[Header("** Player Variables")]
	public bool playerStatus = true;
	private float speed = 20f;
	private float climbingSpeed = 8f;
	private float jumpingPower = 35f;
	private int deathCounter = 0;
	private bool onSpike;

	[Header("** Attack Variables")]
	private bool isAttacking = false;
	[SerializeField] private float attackCooldown;
	[SerializeField] private float range;
	[SerializeField] private float colliderDistance;
	[SerializeField] private int damage;
	[SerializeField] private BoxCollider2D boxCollider;
	[SerializeField] private LayerMask enemyLayer;
	private float attackCooldownTimer = Mathf.Infinity;

	// Variables
	float spikeTimer = 1.5f;

	// Player Status
	private bool isFacingLeft;
	private bool isWalking;
	private bool isJumping;
	private bool isVines = false;

	private Rigidbody2D rigid;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();
		rigid = gameObject.GetComponent<Rigidbody2D>();
	}

	// Input direction, jumping, animations, attack, and health check/death
	void Update()
	{
		if(GameManager.gameManager.playerHealth.Health <= 0)
		{
			Death();
		}

		if (playerStatus)
		{
			// Checks input direction
			xMove = Input.GetAxisRaw("Horizontal");
			// Checks 
			yMove = Input.GetAxis("Vertical");

			// Checks if it needs to flip the player
			Flip();

			// If player is on spike deal damage every 1.5 seconds
			if (onSpike)
			{
				float spikeTime = 1.0f;
				spikeTimer += Time.deltaTime;
				if (spikeTimer > spikeTime)
				{
					GameManager.gameManager.PlayerDamager(20);
					spikeTimer = 0;
				} 

			}

			#region **Jumping
			// Checks if player is grounded
			// *** Jump ***
			if (Input.GetButtonDown("Jump") && IsGrounded() || Input.GetButtonDown("Jump") && isVines)
			{
				isVines = false;
				rigid.gravityScale = 8f;
				rigid.velocity = new Vector2(rigid.velocity.x, jumpingPower);
				isJumping = true;
			}
			if (Input.GetButtonUp("Jump") && (rigid.velocity.y > 0f))
			{
				rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.5f);
				isJumping = false;
			}
			#endregion

			#region **Attacking

			// Update Timer
			attackCooldownTimer += Time.deltaTime;

			// Attack only if the player is in skeletons range of sight
			if (IsEnemyInRange())
			{
				// Attack if cooldown is finished
				if (attackCooldownTimer > attackCooldown)
				{
					transform.position = new Vector2(transform.position.x, transform.position.y);
					isAttacking = true;
					// Attack Animation
					anim.SetTrigger("attack");
					// Delay Damage to hit a little bit into te skeleton animation
					Invoke("DamagePlayer", .5f);
					// Reset attack cooldown
					attackCooldownTimer = 0;
				}

			}
			#endregion

			// *** Animations ***
			anim.SetBool("isFacingLeft", isFacingLeft);
			anim.SetBool("isWalking", isWalking);
			anim.SetBool("isJumping", isJumping);

		}

		// ** Test Damage + Heal **

		
		if (Input.GetKeyDown("b"))
		{
			PlayerDamager(10);
		}
		if (Input.GetKeyDown("n"))
		{
			PlayerHeal(10);
		}
		
	}

	// Update for physics - Walking, Climbing
	private void FixedUpdate(){

		if (playerStatus)
		{
			// *** Walking ***
			// Checking if the player is walking currently
			rigid.velocity = new Vector2(xMove * speed, rigid.velocity.y);
			if (xMove < 0 || xMove > 0)
			{
				isWalking = true;
			}
			else
			{
				isWalking = false;
			}

			// *** Climbing/Vines ***
			// if is climbing than make the gravity scale 0 to allow player to float
			if (isVines)
			{
				rigid.gravityScale = 0;
				rigid.velocity = new Vector2(rigid.velocity.x, yMove * climbingSpeed);
			}
			else
			{
				rigid.gravityScale = 8f;
			}
		}
	}

	// Draws Ground Check Radius
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(groundCheck.position, 0.2f);
	}

	// If it enters the Vines make isVines true
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Vines"))
		{
			isVines = true;
		}
		if(collision.CompareTag("World Colliders"))
		{
			PlayerDamager(200);
		}
		if (collision.CompareTag("Spike"))
		{
			onSpike = true;
		}
	}

	// If it leaves the Vines make isVines false
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Vines"))
		{
			isVines = false;
		}
		if (collision.CompareTag("Spike"))
		{
			onSpike = false;
		}
	}

	// Flips player on input control
	private void Flip()
	{
		if (isFacingLeft && xMove > 0f || !isFacingLeft && xMove < 0f)
		{
			isFacingLeft = !isFacingLeft;
			Vector3 localScale = transform.localScale;
			localScale.x *= -1f;
			transform.localScale = localScale;
		}
	}

	private void Death()
	{
		playerStatus = false;
		rigid.gravityScale = 0;
		if(deathCounter <= 0)
		{
			GameObject deathPopUp = Instantiate(deathMenu, new Vector3(0, 0, 0), Quaternion.identity);
		}
		deathCounter++;

	}

	// Checks if player is on the ground
	private bool IsGrounded()
	{
		// If the ground check of the player is on the ground return is grounded tru
		return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
	}
	
	private void PlayerDamager(int dmg)
	{
		GameManager.gameManager.PlayerDamager(dmg);
	}

	private void PlayerHeal(int heal)
	{
		GameManager.gameManager.PlayerHeal(heal);
	}

	// Attack

	private bool IsEnemyInRange()
	{
		// Check if enemy is in the players range (position, size, angle, direction, distance, layerMask)
		RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y), 0, Vector2.left, 0, enemyLayer);
		return false;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y));
	}
}
