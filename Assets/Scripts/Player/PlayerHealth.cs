using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static Action OnDeath;

    public static float maxHealth = 100;
    
    public static float currentHealth;
	CharacterController controller;

	bool isDead = false;

	void Awake()
	{
		controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
		Time.timeScale = 1f;
	}

    public void Damage(float damage)
	{
		currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
	}

	public void Heal(float heal)
	{
		currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	public void Die()
	{
		if (!isDead)
		{
			PlayerMovement playerMove = GetComponent<PlayerMovement>();
			if (playerMove != null)
			{
				playerMove.enabled = false;
			}
			CarMovement car = GetComponent<CarMovement>();
			if (car != null)
			{
				car.enabled = false;
			}
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = false;
			rb.freezeRotation = false;
			isDead = true;
			Time.timeScale = .5f;
			OnDeath?.Invoke();
		}

	}
}