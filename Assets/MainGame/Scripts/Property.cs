using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemManagement;
using System;

public class Property : MonoBehaviour
{
    [SerializeField]
    public int cost;

    public bool isUnlocked = false;

    bool TakeItemBool = false;

    int costCounter = 0;

    
    Vector3 itemSpawnOffset;

    [SerializeField]
    GameObject spawningField;

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    float offsetItemX;

    [SerializeField]
    float offsetItemY;

    [SerializeField]
    float offsetItemZ;

    [SerializeField]
    Vector3 startOffset;

    [SerializeField]
    int numberOfItemsInARow = 0;

    int currentItemsInRowCount;

    [SerializeField]
    float gapBetweenRows;

    [SerializeField]
    float gapBetweenItems;

    [SerializeField]
    int totalNumberOfRows;

    int currentCountOfRows = 0;

    bool isSpawning = false;


    
  
    
    List<ItemsInProperty> itemsInsidePropertyList = new List<ItemsInProperty>();

    

    private void Start()
    {
        
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
                    ItemSpawner();
                }
            }
        }

    }

    public void StopTakingItem()
    {
        if (isUnlocked) return;
        TakeItemBool = false;
    }

    

    private void ItemSpawner()
    {
        isSpawning = true;
       

        float itemHolderSizeY = spawningField.GetComponent<Renderer>().bounds.size.y;
        float itemHolderTop = spawningField.transform.localPosition.y + itemHolderSizeY / 2;

        float itemHolderSizeZ = spawningField.GetComponent<Renderer>().bounds.size.x;
        float itemHolderEdge = itemHolderSizeZ / 2;

        itemSpawnOffset.x += startOffset.x ;
        itemSpawnOffset.y += startOffset.y ;
        itemSpawnOffset.z += startOffset.z ;

  
        var item = Instantiate(itemPrefab);
        item.transform.parent = spawningField.transform.parent;
        item.transform.position = spawningField.transform.position;
        item.transform.localPosition = itemSpawnOffset;
        item.AddComponent<Rigidbody>();
        item.GetComponent<Item>().property = this;
        item.GetComponent<Rigidbody>().useGravity = false;


        //add to list
        ItemsInProperty thisItem = new ItemsInProperty();  //A empty object gets spawned somehow
        Destroy(thisItem.item.gameObject);
        thisItem.item = item;
        thisItem.position = itemSpawnOffset;
        itemsInsidePropertyList.Add(thisItem);

        itemSpawnOffset = new Vector3(itemSpawnOffset.x + offsetItemX + gapBetweenItems, itemSpawnOffset.y, itemSpawnOffset.z);
        
        currentItemsInRowCount++;

        StartCoroutine(Spawner());
    }


    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(0.5f);

        var item = Instantiate(itemPrefab);
        item.transform.parent = spawningField.transform.parent;
        item.transform.position = spawningField.transform.position;
        item.transform.localPosition = itemSpawnOffset;
        item.AddComponent<Rigidbody>();
        item.GetComponent<Item>().property = this;
        item.GetComponent<Rigidbody>().useGravity = false;


        ItemsInProperty thisItem = new ItemsInProperty();
        Destroy(thisItem.item.gameObject);  //A empty object gets spawned somehow
        thisItem.item = item;
        thisItem.position = itemSpawnOffset;
        itemsInsidePropertyList.Add(thisItem);

        itemSpawnOffset = new Vector3(itemSpawnOffset.x + offsetItemX + gapBetweenItems, itemSpawnOffset.y, itemSpawnOffset.z );

        currentItemsInRowCount++;

        if (currentItemsInRowCount >= numberOfItemsInARow)
        {
            itemSpawnOffset = new Vector3(startOffset.x , startOffset.y , itemSpawnOffset.z + gapBetweenRows);
            currentItemsInRowCount = 0;
            currentCountOfRows++;
        }

        if (currentCountOfRows < totalNumberOfRows)
        {
            StartCoroutine(Spawner());

            //StartCoroutine(Tester());
        }

        else
        {
            isSpawning = false;

            OnCollect();
        }
    }

    IEnumerator Tester()
    {
        yield return new WaitForSeconds(5f);
        OnCollect();
    }

    public void OnCollect()
    {
        if (isSpawning) return;

        foreach(var item in itemsInsidePropertyList)
        {
            if(item.item == null)
            {
                StartCoroutine(AfterCollectSpawn(item));
                isSpawning = true;
                break;
            }
        }
    }

    IEnumerator AfterCollectSpawn(ItemsInProperty item)
    {
        yield return new WaitForSeconds(0.5f);

        var itemIns = Instantiate(itemPrefab);
        itemIns.transform.parent = spawningField.transform.parent;
        itemIns.transform.position = spawningField.transform.position;
        itemIns.transform.localPosition = item.position;
        itemIns.AddComponent<Rigidbody>();
        itemIns.GetComponent<Item>().property = this;
        itemIns.GetComponent<Rigidbody>().useGravity = false;


        foreach (var ele in itemsInsidePropertyList)
        {
            if(ele.position == item.position)
            {
                ele.item = itemIns;
            }
        }

        bool isNull = false;

        foreach (var itemMade in itemsInsidePropertyList)
        {
            if (itemMade.item == null)
            {
                StartCoroutine(AfterCollectSpawn(itemMade));
                isNull = true;
                break;
            }
        }

        if (!isNull) isSpawning = false;
    }
}

public class ItemsInProperty
{
    public Vector3 position;
    public GameObject item = new GameObject();
}

