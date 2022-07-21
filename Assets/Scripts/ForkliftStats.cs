using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForkliftStats : MonoBehaviour
{
    public static ForkliftStats instance;

    [SerializeField]
    GameObject currentForkLift;

    /*[SerializeField]
    ForkliftUI forkliftUIPopup;*/

    private void Awake()
    {
        instance = this;
    }

    public float speed;

    public float collectSpeed;

    public float capacity;

    public int currentForkliftLevel;

    public bool speedDone;

    public bool capacityDone;

    public bool collectSpeedDone;

    [SerializeField]
    float forkliftSpeedIncrementer;

    [SerializeField]
    float forkliftCapacityIncrementer;

    [SerializeField]
    float forkliftCollectSpeedDecrementer;

    public int numberOfSteps;

    public int currentStepforkliftSpeed;
    public int currentStepforkliftCapacity;
    public int currentStepForkliftCollectSpeed;

    public int cashRequiredForSpeedUpgrade;
    public int cashRequiredForCapacityUpgrade;
    public int cashRequiredForCollectSpeed;

    private void Start()
    {
        numberOfSteps = currentForkliftLevel;

        UpgradeForklift();
    }

    public void UpgradeForklift()
    {
        var newForkLift = Instantiate(ForkliftDataManager.instance.forklifts[currentForkliftLevel].forkliftObj);
        newForkLift.transform.parent = currentForkLift.transform.parent;
        Debug.Log("Pos" + ForkliftDataManager.instance.forklifts[currentForkliftLevel].position);
        newForkLift.transform.localPosition = ForkliftDataManager.instance.forklifts[currentForkliftLevel].position;
        newForkLift.transform.rotation = currentForkLift.transform.rotation;
        Destroy(currentForkLift.gameObject);
        currentForkLift = newForkLift;
    }

    public void UpgradeCapacity()
    {
        if (capacityDone) return;


        if (cashRequiredForCapacityUpgrade < CashManager.instance.GetCash())
        {

            capacity += forkliftCapacityIncrementer;

            currentStepforkliftCapacity++;

            if (currentStepforkliftCapacity >= numberOfSteps + 1)
            {
                capacityDone = true;
            }

            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForCapacityUpgrade);

            CapacityUI.UpdateCapacityUI?.Invoke();
        }
    }

    

    public void UpgradeSpeed()
    {
        if (speedDone) return;

        if (cashRequiredForSpeedUpgrade < CashManager.instance.GetCash())
        {

            speed += forkliftSpeedIncrementer;

            currentStepforkliftSpeed++;

            if (currentStepforkliftSpeed >= numberOfSteps + 1)
            {
                speedDone = true;
            }

            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForSpeedUpgrade);
        }
    }

    

    public void UpgradeCollectSpeed()
    {
        if (collectSpeedDone) return;

        if (cashRequiredForCollectSpeed < CashManager.instance.GetCash())
        {

            collectSpeed -= forkliftCollectSpeedDecrementer;

            currentStepForkliftCollectSpeed++;

            if (currentStepForkliftCollectSpeed >= numberOfSteps + 1)
            {
                collectSpeedDone = true;
            }

            LevelUpCheak();

            CashManager.instance.RemoveCash(cashRequiredForCollectSpeed);
        }
    }


    private void LevelUpCheak()
    {
        if(capacityDone && speedDone && collectSpeedDone)
        {
            currentForkliftLevel++;

            if(currentForkliftLevel >= ForkliftDataManager.instance.forklifts.Length)
            {
                currentForkliftLevel--;
                return;
            }

            numberOfSteps = currentForkliftLevel;
            UpgradeForklift();

            //Ui updated in UI

            speedDone = false;
            capacityDone = false;
            collectSpeedDone = false;

            currentStepforkliftSpeed = 0;
            currentStepforkliftCapacity = 0;
            currentStepForkliftCollectSpeed = 0;

            //ui updated in UI

        }
    }
   

}
