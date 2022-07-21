using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorryStats : MonoBehaviour
{
    public static LorryStats instance;
    private void Awake()
    {
        instance = this;
    }
    
    public int capacity;
    public float speedAkaTimeOfTravel;

    [SerializeField]
    float timeOfTravelDecrementer;

    [SerializeField]
    int capacityIncrementer;

    public bool capacityDone = false;

    public bool speedDone = false;

    public static Action upgradeLorry;

    public int currentLorryLevel;

    public int cashRequiredForSpeedUpgrade;

    public int cashRequiredForCapcityUpgrade;

    public int numberOfSteps;

    public int currentStepSpeed;
    public int currentStepCapacity;

    private void Start()
    {
        numberOfSteps = currentLorryLevel;
    }

    public void UpgradeCapacity()
    {
        if (capacityDone) return;

        if (cashRequiredForCapcityUpgrade < CashManager.instance.GetCash())
        {
            capacity += capacityIncrementer;

            currentStepCapacity++;

            if (currentStepCapacity >= numberOfSteps + 1)
            {
                capacityDone = true;
            }
            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForCapcityUpgrade);
        }

    }

    public void UpgradeSpeed()
    {
        if (speedDone) return;

        if (cashRequiredForSpeedUpgrade < CashManager.instance.GetCash())
        {

            speedAkaTimeOfTravel -= timeOfTravelDecrementer;

            currentStepSpeed++;

            if (currentStepSpeed >= numberOfSteps + 1)
            {
                speedDone = true;

            }
            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForSpeedUpgrade);
        }

    }

    public void LevelUpCheak()
    {
        if(capacityDone && speedDone)
        {
            currentLorryLevel++;

            if(currentLorryLevel >= LorryDataManager.instance.lorryData.Length)
            {
                currentLorryLevel--;
                return;
            }

            numberOfSteps = currentLorryLevel;
            upgradeLorry?.Invoke();
            LorryUI.updateLorryUI?.Invoke();
            capacityDone = false;
            speedDone = false;
            currentStepSpeed = 0;
            currentStepCapacity = 0;
            LorryUI.updateLorryUI?.Invoke();
           
        }
    }
}
