using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues 
{
    public static GlobalValues instance;

    private void Awake()
    {
        instance = this;
    }

    int totalPackagesDelivered;

    int currentPackagesDelivered;

    int totalCashCollected;

    int totalCashSpent;


    public void AddCurrentDeliveredPackages(int amount)
    {
        currentPackagesDelivered += amount;
    }


    public int GetCurrentDeliveredPackages()
    {
        return currentPackagesDelivered;
    }

    public void CurrentPackageComplete()
    {
        currentPackagesDelivered = 0;
    }
}
