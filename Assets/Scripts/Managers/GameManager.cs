using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gameManager { get; private set; }
	public entityHealth playerHealth = new entityHealth(100, 100);


	// ** Player Damage **
	private GameObject player;
	private SpriteRenderer playerRend;
	[SerializeField] private healthBar playerHealthBar;
	private Material playersOriginalMaterial;
	// Flash
	private float flashDuration = 0.125f;
	// White flash material
	[SerializeField] private Material flashMaterial;
	// Currently running flash coroutine
	private Coroutine flashRoutine;
	// ** - Player Damage - **


	private void Awake()
	{
		// Assigning player damage variables
		player = GameObject.FindGameObjectWithTag("player");
		playerRend = player.GetComponent<SpriteRenderer>();
		playersOriginalMaterial = playerRend.material;

		// Creating Game Manager
		if(gameManager != null && gameManager != this)
		{
			Destroy(this);
		}
		else
		{
			gameManager = this;
		}
	}

	private IEnumerator FlashRoutine()
	{
		// Change material to flashing
		playerRend.material = flashMaterial;

		// Pause for "flashDuration" amount of seconds
		yield return new WaitForSeconds(flashDuration);

		// After change back to original material
		playerRend.material = playersOriginalMaterial;

		// Signify that the routine is finished
		flashRoutine = null;
	}
	
	private void Flash()
	{
		if(flashRoutine != null)
		{
			// Stop the routine
			StopCoroutine(flashRoutine);
		}

		// Start routine, and find the reference for it
		flashRoutine = StartCoroutine(FlashRoutine());
	}

	// Damage Player
	public void PlayerDamager(int dmg)
	{
		playerHealth.DamageEntity(dmg);
		Flash();
		playerHealthBar.SetHealth(GameManager.gameManager.playerHealth.Health);
	}
}
