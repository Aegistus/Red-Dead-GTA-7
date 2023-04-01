using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public LayerMask targetLayers;
    public Transform target;
    public GameObject explosionPrefab;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float explosionRadius = 5f;
    public float damage = 10f;

    private ParticleSystem smoke;

    private void Awake()
    {
        smoke = GetComponentInChildren<ParticleSystem>();
    }

    void OnEnable()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        if (target == null)
        {
            target = FindObjectOfType<CarMovement>().transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    private void FixedUpdate()
    {
        AdjustDirection();
    }

    Quaternion startRotation;
    Quaternion targetRotation;
    private void AdjustDirection()
    {
        if (target != null)
        {
            startRotation = transform.rotation;
            transform.LookAt(target.position);
            targetRotation = transform.rotation;
            transform.rotation = startRotation;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    Collider[] results = new Collider[5];
    public void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, results, targetLayers);
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] != null)
            {
                PlayerHealth health = results[i].GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.Damage(damage);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}
