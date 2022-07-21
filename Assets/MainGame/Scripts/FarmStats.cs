using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmStats : MonoBehaviour
{
    public static FarmStats instance;

    public void Awake()
    {
        instance = this;
    }

   
    public int farmMaxLevel;

    public float fertilizerAkaPumpkinSpawnInterval;
    public int harvesterAkaPackageCount;

    [SerializeField]
    float fertilizerAkaPumpkinSpawnIntervalDecrementer;

    [SerializeField]
    int harvesterAkaPackageCountIncrementer;

    public bool fertilizerDone = false;
    public bool harvesterDone = false;

    public static Action upgradeFarm;

    public int currentFarmLevel;

    public int cashRequiredForFertilizerUpgrade;
    public int cashRequiredForHarvesterUpgrade;

    public int numberOfSteps;

    public int currentStepFertilizer;
    public int currentStepHarvester;

    private void Start()
    {
        numberOfSteps = currentFarmLevel;
    }

    public void UpgradeFertilizer()
    {
        if (fertilizerDone) return;

        if (cashRequiredForFertilizerUpgrade < CashManager.instance.GetCash())
        {

            fertilizerAkaPumpkinSpawnInterval -= fertilizerAkaPumpkinSpawnIntervalDecrementer;

            currentStepFertilizer++;

            if (currentStepFertilizer >= numberOfSteps + 1)
            {
                fertilizerDone = true;
            }

            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForFertilizerUpgrade);
        }
    }

    public void UpgradeHarvester()
    {
        if (harvesterDone) return;

        if (cashRequiredForHarvesterUpgrade < CashManager.instance.GetCash())
        {
            harvesterAkaPackageCount += harvesterAkaPackageCountIncrementer;

            currentStepHarvester++;

            if (currentStepHarvester >= numberOfSteps + 1)
            {
                harvesterDone = true;
            }

            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForHarvesterUpgrade);
        }
    }

    public void LevelUpCheak()
    {
        if(fertilizerDone && harvesterDone)
        {
            currentFarmLevel++;

            if(currentFarmLevel >= farmMaxLevel)
            {
                currentFarmLevel--;
                return;
            }

            numberOfSteps = currentFarmLevel;

            FarmUI.updateFarmUI?.Invoke();

            fertilizerDone = false;
            harvesterDone = false;

            currentStepFertilizer = 0;
            currentStepHarvester = 0;

            FarmUI.updateFarmUI?.Invoke();
        }
    }
}
