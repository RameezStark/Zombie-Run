using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityUI : MonoBehaviour
{
    [SerializeField]
    Text capacityText;

    public static Action UpdateCapacityUI;
    public static Action CapacityUIfull;

    Color textColor;
    int counter = 0;
    bool fullEffectOn = false;

    private void OnEnable()
    {
        UpdateCapacityUI += RefreshUI;
        CapacityUIfull += CapacityFull;
    }

    private void OnDisable()
    {
        UpdateCapacityUI -= RefreshUI;
        CapacityUIfull -= CapacityFull;
    }

    private void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        capacityText.text = PackageStorage.totalPackagesInForkLift.ToString() + "/" + ForkliftStats.instance.capacity.ToString();
    }

    public void CapacityFull()
    {
        if (fullEffectOn) return;
        textColor = capacityText.color;
        StartCoroutine(capacityFullWaitor());
    }

    IEnumerator capacityFullWaitor()
    {
        fullEffectOn = true;
        capacityText.color = Color.red;
        capacityText.fontSize += 15;
        capacityText.fontStyle = FontStyle.Bold;

        yield return new WaitForSeconds(0.3f);

        capacityText.color = textColor;
        capacityText.fontSize -= 15;
        capacityText.fontStyle = FontStyle.Normal;
        fullEffectOn = false;



    }
}
