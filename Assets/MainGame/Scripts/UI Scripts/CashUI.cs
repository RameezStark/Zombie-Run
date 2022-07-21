using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashUI : MonoBehaviour
{
    [SerializeField]
    Text cashTxt;

    public static Action UpdateCashUI;


    private void Start()
    {
        UpdateCash();
    }
    private void OnEnable()
    {
        UpdateCashUI += UpdateCash;
        
    }

    private void OnDisable()
    {
        UpdateCashUI -= UpdateCash;
    }

    public void UpdateCash()
    {
        cashTxt.text = CashManager.instance.GetCash().ToString();
    }
}
