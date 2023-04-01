using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    PlayerMovement player;
    CarMovement car;
    CameraController cam;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        car = FindObjectOfType<CarMovement>();
        player = FindObjectOfType<PlayerMovement>();
        cam = FindObjectOfType<CameraController>();
        car.enabled = false;
    }

    public void PlayerEnterCar()
    {
        player.gameObject.SetActive(false);
        car.enabled = true;
        cam.targetTransform = car.transform;
    }

    public void PlayerExitCar()
    {
        player.transform.position = car.transform.position;
        player.gameObject.SetActive(true);
        car.enabled = false;
        cam.targetTransform = player.transform;
    }
}
