using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask interactableLayers;
    public float interactionDistance = 1f;
    int openDoorSoundID;

    AgentEquipment agentEquipment;
    Vector3 halfExtents = new Vector3(1, 1, 2);

    void Start()
    {
        openDoorSoundID = SoundManager.Instance.GetSoundID("Car_Door_Open");
        agentEquipment = GetComponent<AgentEquipment>();
    }

    RaycastHit rayHit;
    void Update()
    {
        if (Physics.BoxCast(transform.position + Vector3.up, halfExtents, transform.forward, out rayHit, Quaternion.identity, interactionDistance, interactableLayers))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CarMovement car = rayHit.collider.GetComponentInParent<CarMovement>();
                if (car != null)
                {
                    SoundManager.Instance.PlaySoundAtPosition(openDoorSoundID, transform.position);
                    GameManager.Instance.PlayerEnterCar(car);
                }
                else
                {
                    print("TEST");
                    WeaponAttack attack = rayHit.collider.GetComponentInParent<WeaponAttack>();
                    if (attack != null)
                    {
                        agentEquipment.PickupWeapon(attack.gameObject);
                    }
                }
            }
            //print("In interaction range");
        }
    }
}
