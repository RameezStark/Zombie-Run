using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemManagement;
using System;

public class Unlocker : MonoBehaviour
{
    [SerializeField]
    BoxCollider blockCollider;

    [SerializeField]
    int cost;


    [SerializeField]
    GameObject areaToBeUnlocked;

    int costCounter;

    bool isUnlocked;

    bool TakeItemBool;


    private void Awake()
    {
        if (isUnlocked)
            UnlockNewArea();
    }
    public void TakeItem()
    {
        if (isUnlocked) return;
        StartCoroutine(TakingItem());
        TakeItemBool = true;
    }

    IEnumerator TakingItem()
    {
        yield return new WaitForSeconds(1f);
        if (ItemManager.instance.getCount() > 0 && costCounter < cost)
        {
            costCounter++;
            ItemManager.instance.Remove(1);
            if (TakeItemBool)
            {
                if (costCounter < cost)
                {
                    StartCoroutine(TakingItem());
                }

                else if (costCounter == cost)
                {
                    isUnlocked = true;
                    UnlockNewArea();
                }
            }
        }

    }

    private void UnlockNewArea()
    {
        areaToBeUnlocked.SetActive(true);
        Destroy(blockCollider.gameObject);
        this.gameObject.SetActive(false);
    }

    public void StopTakingItem()
    {
        if (isUnlocked) return;
        TakeItemBool = false;
    }
}
