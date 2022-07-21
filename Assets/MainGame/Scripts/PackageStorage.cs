using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageStorage : MonoBehaviour
{
    [SerializeField]
    GameObject packagePrefab;

    [SerializeField]
    GameObject packageHolder;

    [SerializeField]
    Vector3 packageStartOffset;

    [SerializeField]
    Vector3 packageOffsetValue;

    [SerializeField]
    int packagesInARow;

    [SerializeField]
    int totalNoOfRows;

    

    int currentPackagesInRow = 0;

    int currentRowCountPackage = 0;

    Vector3 packageSpawnOffset = new Vector3(0f, 0f, 0f);

    public static Action AddPackageToForklift;
    public static Action RemovePackageFromForklift;

    List<PackagesDetails> packagesInForkLiftList = new List<PackagesDetails>();

    public static int totalPackagesInForkLift;

    private void OnEnable()
    {
        AddPackageToForklift += AddPackage;
        RemovePackageFromForklift += RemovePackage;
    }

    private void Start()
    {
        packageSpawnOffset = new Vector3(packageStartOffset.x , packageStartOffset.y, packageStartOffset.z);

        //StartCoroutine(tester());
        
    }

    
    public void RemovePackage()
    {
        if(packagesInForkLiftList.Count > 0)
        {
            packageSpawnOffset = packagesInForkLiftList[packagesInForkLiftList.Count - 1].position;

            PackagesDetails packageInForkLift = packagesInForkLiftList[packagesInForkLiftList.Count - 1];
            Destroy(packageInForkLift.package.gameObject);
            packagesInForkLiftList.RemoveAt(packagesInForkLiftList.Count - 1);
            totalPackagesInForkLift = packagesInForkLiftList.Count;

            if(currentPackagesInRow == 0)
            {
                currentPackagesInRow = packagesInARow;

                if (currentRowCountPackage == 0)
                {
                    currentRowCountPackage = totalNoOfRows;
                }

                else
                {
                    currentRowCountPackage--;
                }
            }

            else
            {
                currentPackagesInRow--;
            }

            CapacityUI.UpdateCapacityUI?.Invoke();

        }
    }



    public void AddPackage()
    {
        var package = Instantiate(packagePrefab);
        package.transform.parent = packageHolder.transform;
        package.transform.rotation = packageHolder.transform.rotation;
        package.transform.localPosition = packageSpawnOffset;

        PackagesDetails thisPackage = new PackagesDetails();
        Destroy(thisPackage.package.gameObject);
        thisPackage.package = package;
        thisPackage.position = packageSpawnOffset;
        packagesInForkLiftList.Add(thisPackage);
        totalPackagesInForkLift = packagesInForkLiftList.Count;

        packageSpawnOffset = new Vector3(packageSpawnOffset.x + packageOffsetValue.x, packageSpawnOffset.y, packageSpawnOffset.z);
        currentPackagesInRow++;

        if(currentPackagesInRow >= packagesInARow)
        {
            packageSpawnOffset = new Vector3(packageStartOffset.x, packageSpawnOffset.y, packageSpawnOffset.z + packageOffsetValue.z);
            currentPackagesInRow = 0;
            currentRowCountPackage++;
        }

        if(currentRowCountPackage >= totalNoOfRows)
        {
            packageSpawnOffset = new Vector3(packageStartOffset.x, packageSpawnOffset.y + packageOffsetValue.y, packageStartOffset.z);
            currentPackagesInRow = 0;
            currentRowCountPackage = 0;
        }

        CapacityUI.UpdateCapacityUI?.Invoke();

    }


}
