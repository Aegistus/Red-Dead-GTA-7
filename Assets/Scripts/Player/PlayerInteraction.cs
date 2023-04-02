using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask carLayer;
    public float interactionDistance = 1f;
    int openDoorSoundID;

    void Start()
    {
        openDoorSoundID = SoundManager.Instance.GetSoundID("Car_Door_Open");
    }

    RaycastHit rayHit;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out rayHit, interactionDistance, carLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CarMovement car = rayHit.collider.GetComponentInParent<CarMovement>();
                if (car != null)
                {
                    SoundManager.Instance.PlaySoundAtPosition(openDoorSoundID, transform.position);
                    GameManager.Instance.PlayerEnterCar(car);
                }
            }
            //print("In interaction range");
        }
    }
}
