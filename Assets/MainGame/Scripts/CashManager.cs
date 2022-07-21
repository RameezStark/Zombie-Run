using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;

    int cash = 100;

    private void Awake()
    {
        instance = this;
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int amount)
    {
        cash += amount;

        CashUI.UpdateCashUI?.Invoke();
    }

    public void RemoveCash(int amount)
    {
        if(cash - amount < 0)
        {
            cash = 0;
        }
        else
        {
            cash -= amount;
        }

        CashUI.UpdateCashUI?.Invoke();

    }
}
