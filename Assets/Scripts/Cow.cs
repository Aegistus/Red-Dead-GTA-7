using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public float MilkMax => milkMax;
    public float MilkRemaining => milkRemaining;

    [SerializeField] float milkMax;
    [SerializeField] float milkSpeed;

    float milkRemaining;

    private void Start()
    {
        milkRemaining = milkMax;
    }

    public void Milk()
    {
        float amount = Time.deltaTime * milkSpeed;
        GameManager.Instance.ReduceMilkDebt(amount);
        milkRemaining -= amount;
    }
}
