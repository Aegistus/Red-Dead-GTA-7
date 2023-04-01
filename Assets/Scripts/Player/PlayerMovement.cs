using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    CharacterController charController;

    [SerializeField] float moveSpeed;

    Vector3 moveVector;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveVector -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVector -= Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVector += Vector3.right;
        }

        moveVector = moveVector.normalized;

        charController.Move(moveVector * moveSpeed * Time.deltaTime);
    }
}
