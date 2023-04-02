using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CarMovement : MonoBehaviour
{
	public LayerMask groundLayer;
    public float maxSpeed = 20f;
	public float turnSpeed = 20f;
	[Range(0f, 1f)]
	public float acceleration = .5f;
	[Range(0f, 1f)]
	public float brakeDeceleration = .5f;
	[Range(0f, 1f)]
	public float coastDeceleration = .1f;
    
	float currentSpeed = 0f;
	int runningSoundID;
	Rigidbody rb;
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void Start()
	{
		runningSoundID = SoundManager.Instance.GetSoundID("Car_Running");
		SoundManager.Instance.PlayGlobalFadeIn(runningSoundID, 1f);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			SoundManager.Instance.PlayGlobalFadeIn(runningSoundID, 1f);
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			SoundManager.Instance.StopPlayGlobalFadeOut(runningSoundID, 1f);
		}
		if (!IsGrounded())
		{
			return;
		}
		float forwardInput = Input.GetAxis("Vertical");
		float horizontalInput = Input.GetAxis("Horizontal");
		if (forwardInput > 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
		}
		else if (forwardInput < 0)
		{
			currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed, brakeDeceleration * Time.deltaTime);
		}
		else
		{
			currentSpeed = Mathf.Lerp(currentSpeed, 0f, coastDeceleration * Time.deltaTime);
		}
		currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
		transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime, Space.Self);
		transform.Rotate(Vector3.up * horizontalInput * turnSpeed * currentSpeed * Time.deltaTime, Space.Self);
	}

	public bool IsGrounded()
	{
		return Physics.Raycast(transform.position + Vector3.up, Vector3.down, 3f, groundLayer);
	}
}
