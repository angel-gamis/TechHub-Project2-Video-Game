using UnityEngine;
using UnityEngine.UI;

public class skeletonHealthBar : MonoBehaviour
{
	private Slider healthBarSlider;
	[SerializeField] private skeletonController skeletonController;

	// Grabbing the health bar slider
	private void Start()
	{
		healthBarSlider = GetComponent<Slider>();
	}

	// Grabs the max health of entity and gives it to the bar -- sets it as max value and current value
	public void SetMaxHealth(int maxHealth)
	{
		healthBarSlider.maxValue = (skeletonController.enemyHealth.MaxHealth - maxHealth);
		healthBarSlider.value = (skeletonController.enemyHealth.MaxHealth - maxHealth);
	}


	// Sets health variable to the current value on the bar
	public void SetHealth(int health)
	{
		healthBarSlider.value = skeletonController.enemyHealth.MaxHealth - health;
	}
}

