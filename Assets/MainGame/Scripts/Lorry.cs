using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lorry : MonoBehaviour
{
    //public int capacity;
    public int packagesInsideLorry = 0;

    [SerializeField]
    GameObject lorry;

    [SerializeField]
    BoxCollider loadingCollider;

    

    [SerializeField]
    int efficency = 30;

    bool lorryForward, lorryBackward;

    [SerializeField]
    int lorryMoveSpeed;

    [SerializeField]
    GameObject lorryStartPos;

    [SerializeField]
    GameObject lorryEndPos;


    

    public int lorryCount;

    


    private void Start()
    {
        //DisableUI();
        UpgradeLorry();
    }

    private void OnEnable()
    {
        LorryStats.upgradeLorry += UpgradeLorry;
    }

    private void OnDisable()
    {
        LorryStats.upgradeLorry -= UpgradeLorry;
    }


    public void OnFullCapacity()
    {
        Debug.Log("Lorry Full");
    }

    
    public void UpgradeLorry()
    {
        if (LorryStats.instance.currentLorryLevel >= LorryDataManager.instance.lorryData.Length) return;

        var newLorry = Instantiate(LorryDataManager.instance.lorryData[LorryStats.instance.currentLorryLevel].lorryObj);

        newLorry.transform.parent = lorry.transform.parent;
        newLorry.transform.position = lorry.transform.position;
        Destroy(lorry.gameObject);
        lorry = newLorry;
    }

    
    public void CheakIfFull()
    {
        if(packagesInsideLorry >= LorryStats.instance.capacity)
        {
            loadingCollider.enabled = false;
            lorryForward = true;
            StartCoroutine(LorryTripWaitor());
        }
    }

    IEnumerator LorryTripWaitor()
    {
        yield return new WaitForSeconds(LorryStats.instance.speedAkaTimeOfTravel);

        CashManager.instance.AddCash(efficency); //chnage to package cash
        lorryBackward = true;
    }


    private void Update()
    {
        var step = lorryMoveSpeed * Time.deltaTime;
        
        if(lorryForward)
        {
            Vector3 direction = lorry.transform.position - lorryEndPos.transform.position;
            direction = direction.normalized;

            lorry.transform.position -= direction * step;

            if (Mathf.Floor(lorry.transform.localPosition.z) == Mathf.Floor(lorryEndPos.transform.localPosition.z))
            {
                lorryForward = false;
            }
        }

        else if(lorryBackward)
        {
            Vector3 direction = lorry.transform.position - lorryStartPos.transform.position;
            direction = direction.normalized;

            lorry.transform.position -= direction * step;

            if (Math.Ceiling(lorry.transform.localPosition.z) == Math.Ceiling(lorryStartPos.transform.localPosition.z))
            {
                lorryBackward = false;
                packagesInsideLorry = 0;
            }
        }
    }
}
