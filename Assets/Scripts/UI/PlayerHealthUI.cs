using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] RectTransform healthBar;
	PlayerHealth health;
	float startingWidth;
	void Awake()
	{
		health = FindObjectOfType<PlayerHealth>();
		startingWidth = healthBar.sizeDelta.x;
	}
	void Update()
	{
		Vector2 sizeDelta = healthBar.sizeDelta;
		sizeDelta.x = Mathf.Lerp(0, startingWidth, PlayerHealth.currentHealth / health.maxHealth);
		healthBar.sizeDelta = sizeDelta;
	}
}
