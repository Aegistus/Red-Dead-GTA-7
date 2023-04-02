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

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, interactionDistance, carLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SoundManager.Instance.PlaySoundAtPosition(openDoorSoundID, transform.position);
                GameManager.Instance.PlayerEnterCar();
            }
            //print("In interaction range");
        }
    }
}
