using UnityEngine;
using UnityEngine.UI;

public class playerHealthBar : MonoBehaviour
{
	Slider healthBarSlider;

	// Grabbing the health bar slider
	private void Start()
	{
		healthBarSlider = GetComponent<Slider>();
	}

	// Grabs the max health of entity and gives it to the bar -- sets it as max value and current value
	public void SetMaxHealth(int maxHealth)
	{
		healthBarSlider.maxValue = (GameManager.gameManager.playerHealth.MaxHealth - maxHealth);
		healthBarSlider.value = (GameManager.gameManager.playerHealth.MaxHealth - maxHealth);
	}


	// Sets health variable to the current value on the bar
	public void SetHealth(int health)
	{
		healthBarSlider.value = GameManager.gameManager.playerHealth.MaxHealth - health;
	}
}
