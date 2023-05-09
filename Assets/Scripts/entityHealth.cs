using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entityHealth
{
	// Fields
	int currentHealth;
	int currentMaxHealth;

	// Properties
	public int Health
	{
		get
		{
			return currentHealth;
		}
		set
		{
			currentHealth = value;
		}
	}

	public int MaxHealth
	{
		get
		{
			return currentMaxHealth;
		}
		set
		{
			currentMaxHealth = value;
		}
	}

	
	public entityHealth(int health, int maxHealth)
	{
		currentHealth = health;
		currentMaxHealth = maxHealth;
	}

	public void DamageEntity(int damageAmount)
	{
		if(currentHealth > 0)
		{
			currentHealth -= damageAmount;
		}
	}

	public void HealEntity(int healAmount)
	{
		if(currentHealth < currentMaxHealth)
		{
			currentHealth += healAmount;
		}
		if(currentHealth > currentMaxHealth)
		{
			currentHealth = currentMaxHealth;
		}
	}
}
