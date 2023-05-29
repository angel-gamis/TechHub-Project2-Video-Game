using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager gameManager { get; private set; }
	public entityHealth playerHealth = new entityHealth(100, 100);

	#region **Player Damage Variables**
	[SerializeField] private GameObject player;
	private SpriteRenderer playerRend;
	[SerializeField] private playerHealthBar playerHealthBar;
	private Material playersOriginalMaterial;
	
	// Flash
	private float flashDuration = 0.130f;
	// White flash material
	[SerializeField] private Material flashMaterial;
	[SerializeField] private Material healFlashMaterial;
	// Currently running flash coroutine
	private Coroutine flashRoutine;
	private Coroutine healFlashRoutine;
	#endregion

	private void Awake()
	{
		// Assigning player damage variables
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

	#region **Flash**
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

	private IEnumerator HealFlashRoutine()
	{
		// Change material to flashing
		playerRend.material = healFlashMaterial;

		// Pause for "flashDuration" amount of seconds
		yield return new WaitForSeconds(flashDuration);

		// After change back to original material
		playerRend.material = playersOriginalMaterial;

		// Signify that the routine is finished
		healFlashRoutine = null;
	}

	private void HealFlash()
	{
		if (healFlashRoutine != null)
		{
			// Stop the routine
			StopCoroutine(healFlashRoutine);
		}

		// Start routine, and find the reference for it
		healFlashRoutine = StartCoroutine(HealFlashRoutine());
	}

	#endregion

	// Damage Player
	public void PlayerDamager(int dmg)
	{
		playerHealth.DamageEntity(dmg);
		Flash();
		playerHealthBar.SetHealth(gameManager.playerHealth.Health);
	}

	// Heal Player
	public void PlayerHeal(int heal)
	{
		// Use parameter heal to change the game manager player health variable
		playerHealth.HealEntity(heal);
		HealFlash();
		// Change the slider using the health bar scripts method set health
		playerHealthBar.SetHealth(gameManager.playerHealth.Health);
	}
}
