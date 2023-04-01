using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static Action OnDeath;

    public float maxHealth;
    
    public static float currentHealth;
	CharacterController controller;
	void Awake()
	{
		controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
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
		GetComponent<PlayerMovement>().enabled = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = false;
        OnDeath?.Invoke();
	}
}