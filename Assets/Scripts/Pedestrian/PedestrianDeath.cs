using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianDeath : MonoBehaviour
{
    [SerializeField] int carLayer;
    [SerializeField] float despawnTime;
    Rigidbody rb;
    PedestrianMovement movement;
    NavMeshAgent navAgent;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<PedestrianMovement>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == carLayer)
        {
            rb.isKinematic = false;
            movement.enabled = false;
            navAgent.enabled = false;
            StartCoroutine(Despawn());
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
