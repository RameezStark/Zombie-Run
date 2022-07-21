using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemManagement;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    #region GeneralVariables

    public bool isUnlocked = false;

    [SerializeField]
    GameObject lockedFarm;

    [SerializeField]
    GameObject unlockedFarm;

    bool TakeItemBool = false;
    bool isSpawning = false;
    int costCounter = 0;

    [SerializeField]
    int cost;

    [SerializeField]
    int costToBuyFarm;

    #endregion

    #region PumpkinVariables

    
    public GameObject pumpkinPrefab;

    [SerializeField]
    GameObject farmField;

    [SerializeField]
    Vector3 pumpkinStartOffset;

    [SerializeField]
    Vector3 pumpkinOffsetValue;

    Vector3 pumpkinSpawnOffset;

    [SerializeField]
    int numberOfPumpkinsInARow;

    [SerializeField]
    int totalNumberOfPumpkinRows;

    [SerializeField]
    float gapBetweenRowsPumpkins;

    int currentpumpkinsInRow = 0;

    int currentCountOfPumpkinRows = 0;

    #endregion

    #region HarvesterVariables

    [SerializeField]
    GameObject harverster;

    [SerializeField]
    GameObject harvesterStartPos;

    [SerializeField]
    GameObject harversterEndPos;

    [SerializeField]
    int harvesterSpeed;

    bool harversterForward = false;
    bool harversterBack = false;

    #endregion

    #region PackageVariables

    [SerializeField]
    Vector3 packageStartOffset;

    [SerializeField]
    Vector3 packageOffsetValue;

    Vector3 packageSpawnOffset;

    [SerializeField]
    GameObject packagePrefab;

    [SerializeField]
    GameObject packageHolder;

    [SerializeField]
    int packagesInOneRow;

    [SerializeField]
    int totalNoOfRowsPackages;

    int currentPackagesInaRow = 0;
    int currentRowCountPackages = 0;

    List<PackagesDetails> packagesInFarmList = new List<PackagesDetails>();

    public int totalPackagesInFarm;

    #endregion

    #region FarmStatVariables

    [SerializeField]
    int sprinkler = 1;
    [SerializeField]
    int cropCultivator = 1;
    [SerializeField]
    int packagesPerCultivation = 1;

    #endregion

    #region UIVariables

    
    

    #endregion

    private void Start()
    {
        spawnCheak();
        packageSpawnOffset.x = packageStartOffset.x;
        packageSpawnOffset.y = packageStartOffset.y;
        packageSpawnOffset.z = packageStartOffset.z;
        
        
    }

    public void OnUnlock()
    {
        if (isUnlocked) return;

        if(CashManager.instance.GetCash() > costToBuyFarm)
        {
            CashManager.instance.RemoveCash(costToBuyFarm);
            isUnlocked = true;
            spawnCheak();
            lockedFarm.SetActive(false);
            unlockedFarm.SetActive(true);
        }
        
    }

    public void spawnCheak()
    {
        if (!isUnlocked)
        {
            lockedFarm.SetActive(true);
            unlockedFarm.SetActive(false);
            return;
        }

        lockedFarm.SetActive(false);
        unlockedFarm.SetActive(true);
        PumpkinSpawner();


    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(50f);

        PumpkinSpawner();
        StartCoroutine(StartSpawn());
    }

    

    private void PumpkinSpawner()
    {
        if (!isUnlocked) return;
        //if (isSpawning) return;

        isSpawning = true;
        currentpumpkinsInRow = 0;
        currentCountOfPumpkinRows = 0;
        pumpkinSpawnOffset.x = pumpkinStartOffset.x;
        pumpkinSpawnOffset.y = pumpkinStartOffset.y;
        pumpkinSpawnOffset.z = pumpkinStartOffset.z;

        var pumpkin = Instantiate(pumpkinPrefab);
        pumpkin.transform.parent = farmField.transform;
        pumpkin.transform.localPosition = pumpkinSpawnOffset;
        pumpkin.AddComponent<Pumpkin>();

        pumpkinSpawnOffset = new Vector3(pumpkinSpawnOffset.x + pumpkinOffsetValue.x, pumpkinSpawnOffset.y + pumpkinOffsetValue.y, pumpkinSpawnOffset.z + pumpkinOffsetValue.z);

        currentpumpkinsInRow++;

        StartCoroutine(PumpkinSpawn());
    }

    IEnumerator PumpkinSpawn()
    {
        yield return new WaitForSeconds(FarmStats.instance.fertilizerAkaPumpkinSpawnInterval);

        var pumpkin = Instantiate(pumpkinPrefab);
        pumpkin.transform.parent = farmField.transform;
        pumpkin.transform.localPosition = pumpkinSpawnOffset;
        pumpkin.AddComponent<Pumpkin>();

        pumpkinSpawnOffset = new Vector3(pumpkinSpawnOffset.x + pumpkinOffsetValue.x, pumpkinSpawnOffset.y + pumpkinOffsetValue.y, pumpkinSpawnOffset.z + pumpkinOffsetValue.z);

        currentpumpkinsInRow++;

        if(currentpumpkinsInRow >= numberOfPumpkinsInARow)
        {
            pumpkinSpawnOffset = new Vector3(pumpkinSpawnOffset.x + gapBetweenRowsPumpkins, pumpkinStartOffset.y, pumpkinStartOffset.z);
            currentpumpkinsInRow = 0;
            currentCountOfPumpkinRows++;
        }

        if(currentCountOfPumpkinRows < totalNumberOfPumpkinRows)
        {
            StartCoroutine(PumpkinSpawn());
        }

        else
        {
            isSpawning = false;
            CollectPumpkins();
        }
    }

    public void CollectPumpkins()
    {
        harversterForward = true;                
    }

    private void FixedUpdate()
    {
        var step = harvesterSpeed * Time.deltaTime;

        if(harversterForward)
        {
            Vector3 direction = harverster.transform.position - harversterEndPos.transform.position;

            direction = direction.normalized;

            harverster.transform.position -= direction * step;

            if ( Mathf.Floor(harverster.transform.localPosition.x) == Mathf.Floor(harversterEndPos.transform.localPosition.x))
            {
                harversterForward = false;
                harversterBack = true;
            }

        }

        else if (harversterBack)
        {
            Vector3 direction = harverster.transform.position - harvesterStartPos.transform.position;

            direction = direction.normalized;

            harverster.transform.position -= direction * step;

            if (Mathf.Floor(harverster.transform.localPosition.x) == Mathf.Floor(harvesterStartPos.transform.localPosition.x))
            {
                harversterForward = false;
                harversterBack = false;
                PumpkinSpawner();

                for (int i = 1; i <= FarmStats.instance.harvesterAkaPackageCount; i++)
                {
                    PackageSpawner();
                }
            }
        }
    }

    private void PackageSpawner()
    {
        if (!isUnlocked) return;

        var package = Instantiate(packagePrefab);
        package.transform.parent = packageHolder.transform;
        package.transform.localPosition = packageSpawnOffset;

        PackagesDetails thisPackage = new PackagesDetails();
        Destroy(thisPackage.package.gameObject);
        thisPackage.package = package;
        thisPackage.position = packageSpawnOffset;
        packagesInFarmList.Add(thisPackage);
        totalPackagesInFarm = packagesInFarmList.Count;

        packageSpawnOffset = new Vector3(packageSpawnOffset.x, packageSpawnOffset.y, packageSpawnOffset.z + packageOffsetValue.z);
        currentPackagesInaRow++;

        if (currentPackagesInaRow >= packagesInOneRow)
        {
            packageSpawnOffset = new Vector3(packageSpawnOffset.x + packageOffsetValue.x, packageSpawnOffset.y,packageStartOffset.z);
            currentPackagesInaRow = 0;
            currentRowCountPackages++;

        }

        if(currentRowCountPackages >= totalNoOfRowsPackages)
        {
            packageSpawnOffset = new Vector3(packageStartOffset.x, packageSpawnOffset.y + packageOffsetValue.y, packageStartOffset.z);
            currentPackagesInaRow = 0;
            currentRowCountPackages = 0;
        }


       // StartCoroutine(Tester());

        //GetPackage();

    }

   /* IEnumerator Tester()
    {
        yield return new WaitForSeconds(10f);

        GetPackage();
        
    }*/

    public void GetPackage()
    {
        if(packagesInFarmList.Count > 0)
        {
           
            packageSpawnOffset = packagesInFarmList[packagesInFarmList.Count - 1].position;
            
            PackagesDetails packagesInFarm = packagesInFarmList[packagesInFarmList.Count - 1];
            Destroy(packagesInFarm.package.gameObject);
            packagesInFarmList.RemoveAt(packagesInFarmList.Count - 1);
            totalPackagesInFarm = packagesInFarmList.Count;
            

        }

        
        else
        {
            Debug.Log("Farm empty");
        }

        


    }

    public void ResetFarm()
    {
        //isUnlocked = false;
        harversterForward = false;
        harversterBack = false;
        harverster.transform.position = harvesterStartPos.transform.position;
        spawnCheak();
    }

}

public class PackagesDetails
{
    public Vector3 position;
    public GameObject package = new GameObject();
}
