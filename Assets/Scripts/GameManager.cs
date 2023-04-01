using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public Action<int> OnWantedLevelChange;

    PlayerMovement player;
    CarMovement car;
    CameraController cam;

    int numOfDeadPedestrians;

    int currentWantedLevel = 0;

    int oneStarReq = 1;
    int twoStarReq = 3;
    int threeStarReq = 6;
    int fourStarReq = 12;
    int fiveStarReq = 24;

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

    public void DeadPedestrian()
    {
        numOfDeadPedestrians++;
        CheckWantedLevel();
    }

    public void CheckWantedLevel()
    {
        if (numOfDeadPedestrians >= oneStarReq)
        {
            currentWantedLevel = 1;
        }
        if (numOfDeadPedestrians >= twoStarReq)
        {
            currentWantedLevel = 2;
        }
        if (numOfDeadPedestrians >= threeStarReq)
        {
            currentWantedLevel = 3;
        }
        if (numOfDeadPedestrians >= fourStarReq)
        {
            currentWantedLevel = 4;
        }
        if (numOfDeadPedestrians >= fiveStarReq)
        {
            currentWantedLevel = 5;
        }
        OnWantedLevelChange.Invoke(currentWantedLevel);
    }
}
