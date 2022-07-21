using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public List<Farm> farms;
    public static FarmManager instance;

    [SerializeField]
    int currentPumpkinCount;

    

    private void Awake()
    {
        instance = this;
    }

    
    public void ResetAllPumpkins()
    {
        currentPumpkinCount++;
        Pumpkin.DestroyAllPumpkins?.Invoke();
        foreach (var farm in farms)
        {
            farm.ResetFarm();
            farm.pumpkinPrefab = PumpkinManager.instance.pumpkins[currentPumpkinCount].pumkin;
        }

        //farms[0].isUnlocked = true;
        //farms[0].spawnCheak();
    }
}
