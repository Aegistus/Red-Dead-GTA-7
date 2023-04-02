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

    public int CurrentWantedLevel { get; private set; } = 0;

    int oneStarReq = 1;
    int twoStarReq = 3;
    int threeStarReq = 6;
    int fourStarReq = 12;
    int fiveStarReq = 24;

    int gainStarSoundID;

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
        int cityAmbianceSoundID = SoundManager.Instance.GetSoundID("City_Ambiance");
        gainStarSoundID = SoundManager.Instance.GetSoundID("Gain_Star");
        SoundManager.Instance.PlayGlobalFadeIn(cityAmbianceSoundID, 2f);
    }

    public void PlayerEnterCar()
    {
        player.gameObject.SetActive(false);
        car.enabled = true;
        car.gameObject.AddComponent<PlayerHealth>();
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
        int lastWantedLevel = CurrentWantedLevel;
        if (numOfDeadPedestrians >= oneStarReq)
        {
            CurrentWantedLevel = 1;
        }
        if (numOfDeadPedestrians >= twoStarReq)
        {
            CurrentWantedLevel = 2;
        }
        if (numOfDeadPedestrians >= threeStarReq)
        {
            CurrentWantedLevel = 3;
        }
        if (numOfDeadPedestrians >= fourStarReq)
        {
            CurrentWantedLevel = 4;
        }
        if (numOfDeadPedestrians >= fiveStarReq)
        {
            CurrentWantedLevel = 5;
        }
        if (CurrentWantedLevel > lastWantedLevel)
        {
            SoundManager.Instance.PlaySoundGlobal(gainStarSoundID);
        }
        OnWantedLevelChange.Invoke(CurrentWantedLevel);
    }
}
