using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask carLayer;
    public float interactionDistance = 1f;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, interactionDistance, carLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.PlayerEnterCar();
            }
            //print("In interaction range");
        }
    }
}
