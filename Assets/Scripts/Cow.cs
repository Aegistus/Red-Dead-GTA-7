using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
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
