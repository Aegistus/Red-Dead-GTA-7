using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    
    float currentHealth;
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
		GetComponentInChildren<CameraController>().enabled = false;
	}
}