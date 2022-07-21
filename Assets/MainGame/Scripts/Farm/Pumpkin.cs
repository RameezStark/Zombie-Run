using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public static Action DestroyAllPumpkins;

    private void OnEnable()
    {
        DestroyAllPumpkins += DestroyPumpkin;
    }

    private void OnDisable()
    {
        DestroyAllPumpkins -= DestroyPumpkin;
    }
    public void DestroyPumpkin()
    {
        Destroy(this.gameObject);
    }
}
