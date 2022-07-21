using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageCollectorAndDispenser : MonoBehaviour
{
    
    bool farmCollect = false;

    bool isLoadingLorry = false;

    bool isLoadingWarehouse = false;

    bool isUnloadingWarehouse = false;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Farm>() != null)
        {
            if (other.GetComponent<Farm>().isUnlocked)
            {
                if (farmCollect) return;

                farmCollect = true;
                StartCoroutine(CollectPackageFromFarm(other.GetComponent<Farm>()));
                //other.GetComponent<Farm>().EnableUI();
            }

            else
            {
                other.GetComponent<Farm>().OnUnlock();
            }
        }

        else if (other.GetComponent<Lorry>() != null)
        {
            if (isLoadingLorry) return;

            Lorry thisLorry = other.GetComponent<Lorry>();
            //thisLorry.EnableUI();
            LoadPackageToLorry(thisLorry);
        }

        else if(other.GetComponent<WarehouseLoad>() != null)
        {
            if (isLoadingWarehouse) return;

            WareHouseManager warehouse = other.GetComponent<WarehouseLoad>().warehouse;
            isLoadingWarehouse = true;
            StartCoroutine(LoadToWarehouse(warehouse));
        }

        else if(other.GetComponent<WarehouseUnload>() != null)
        {
            if (isUnloadingWarehouse) return;

            WareHouseManager warehouse = other.GetComponent<WarehouseUnload>().warehouse;
            isUnloadingWarehouse = true;
            StartCoroutine(UnloadToWarehouse(warehouse));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Farm>() != null)
        {
            farmCollect = false;
            //other.GetComponent<Farm>().DisableUI();
        }

        else if (other.GetComponent<Lorry>() != null)
        {
            isLoadingLorry = false;
            //other.GetComponent<Lorry>().DisableUI();
        }

        else if (other.GetComponent<WarehouseLoad>() != null)
        {
            
            isLoadingWarehouse = false;
            
        }

        else if (other.GetComponent<WarehouseUnload>() != null)
        {
            
            isUnloadingWarehouse = false;
            
        }
    }

    IEnumerator CollectPackageFromFarm(Farm farm)
    {
        yield return new WaitForSeconds(ForkliftStats.instance.collectSpeed);

        if (farm.totalPackagesInFarm > 0)
        {
            if (PackageStorage.totalPackagesInForkLift < ForkliftStats.instance.capacity)
            {
                if (farmCollect)
                {
                    farm.GetPackage();
                    PackageStorage.AddPackageToForklift.Invoke();
                    StartCoroutine(CollectPackageFromFarm(farm));
                }
            }
            
            else
            {
                Debug.Log("forklift Full");
                CapacityUI.CapacityUIfull?.Invoke();
            }
        }
    }

    private void LoadPackageToLorry(Lorry thisLorry)
    {
        isLoadingLorry = true;
        StartCoroutine(LoadingPackageToLorry(thisLorry));
    }

    IEnumerator LoadingPackageToLorry(Lorry thisLorry)
    {
        yield return new WaitForSeconds(ForkliftStats.instance.collectSpeed);

        if (isLoadingLorry)
        {
            if (PackageStorage.totalPackagesInForkLift > 0)
            {
                if (thisLorry.packagesInsideLorry < LorryStats.instance.capacity)
                {
                    this.GetComponent<PackageStorage>().RemovePackage();
                    thisLorry.packagesInsideLorry++;
                    thisLorry.CheakIfFull();
                    StartCoroutine(LoadingPackageToLorry(thisLorry));
                }

                else
                {
                    thisLorry.OnFullCapacity();
                    thisLorry.CheakIfFull();
                }
            }
        }

    }

    IEnumerator LoadToWarehouse(WareHouseManager wareHouseManager)
    {
        yield return new WaitForSeconds(ForkliftStats.instance.collectSpeed);

        if (isLoadingWarehouse)
        {
            if (PackageStorage.totalPackagesInForkLift > 0)
            {
                this.GetComponent<PackageStorage>().RemovePackage();
                wareHouseManager.totalPackagesInWareHouse++;
                StartCoroutine(LoadToWarehouse(wareHouseManager));
            }
        }
    }

    IEnumerator UnloadToWarehouse(WareHouseManager wareHouseManager) //add forklift Limit
    {
        yield return new WaitForSeconds(ForkliftStats.instance.collectSpeed);

        if (isUnloadingWarehouse)
        {
            if (wareHouseManager.totalPackagesInWareHouse > 0)
            {
                if (PackageStorage.totalPackagesInForkLift < ForkliftStats.instance.capacity)
                {
                    this.GetComponent<PackageStorage>().AddPackage();
                    wareHouseManager.totalPackagesInWareHouse--;
                    StartCoroutine(UnloadToWarehouse(wareHouseManager));
                }
            }
        }
    }
}
