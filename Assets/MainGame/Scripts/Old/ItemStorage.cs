using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagement
{
    public class ItemStorage : MonoBehaviour
    {
        [SerializeField]
        GameObject itemPrefab;

        [SerializeField]
        GameObject itemHolder;

        [SerializeField]
        int itemTotalCount = 7;

       /* [SerializeField]
        int numberItemPerStack = 3;*/

        Vector3 itemOffset = new Vector3(0f,0f,0f);

        [SerializeField]
        float offsetItemX = 1f;

        [SerializeField]
        float offsetItemY = 1f;

        [SerializeField]
        Vector3 startOffset;

        

        //GameObject zombie;

        [SerializeField]
        float gapBetweenItemStacks;

        Bounds ItemBounds;

        public List<GameObject> itemList = new List<GameObject>();

        //actions

        public static Action AddToStorage;
        public static Action RemoveFromStorage;

        private void Awake()
        {

        }

        private void OnEnable()
        {
            AddToStorage += AddItem;
            RemoveFromStorage += RemoveItem;
        }

        private void OnDisable()
        {
            AddToStorage -= AddItem;
            RemoveFromStorage += RemoveItem;
        }
        private void Start()
        {
            FillItemBox();

        }

        public void FillItemBox()
        {
           /* float itemHolderSizeY = itemHolder.GetComponent<Renderer>().bounds.size.y;
            float itemHolderTop = itemHolder.transform.localPosition.y + itemHolderSizeY / 2;

            float itemHolderSizeZ = itemHolder.GetComponent<Renderer>().bounds.size.z;
            float itemHolderEdge = itemHolderSizeZ / 2;*/

            //itemOffset.x += offsetItemX;
            //itemOffset.y = itemHolderTop;
            //itemOffset.z += itemHolderEdge;
            itemOffset = new Vector3(startOffset.x, startOffset.y, startOffset.z);



            int stackCounter = 0;
            
            /*for (int i = 0; i < itemTotalCount; i++)
            {
                var item = Instantiate(itemPrefab);
                item.transform.parent = itemHolder.transform.parent;
               
                item.transform.position = itemHolder.transform.position;


                if (i == 0) //first stack
                {
                    *//*GetBounds(item);
                    itemOffset.z -= (ItemBounds.size.z / 2) + gapBetweenItemStacks;*//*
                    itemOffset = new Vector3(startOffset.x, startOffset.y, startOffset.z);
                }

                item.transform.position = itemOffset;
                itemOffset = new Vector3(itemOffset.x, itemOffset.y + offsetItemY, itemOffset.z);


                

                itemList.Add(item.gameObject);
            }*/
        }

        public void GetBounds(GameObject item)
        {
            ItemBounds = item.GetComponent<BoxCollider>().bounds;

            
        }

        public void AddItem()
        {
            var item = Instantiate(itemPrefab);
            item.transform.parent = itemHolder.transform.parent;
            item.transform.position = itemHolder.transform.position;
            item.transform.rotation = itemHolder.transform.rotation;
            item.transform.localPosition += itemOffset;
            //item.GetComponent<BoxCollider>().isTrigger = false;
            item.AddComponent<Rigidbody>().isKinematic = true;
            //item.GetComponent<Rigidbody>().useGravity = false;

            itemList.Add(item.gameObject);
            itemOffset = new Vector3(itemOffset.x, itemOffset.y + offsetItemY, itemOffset.z);
            
        }

        public void RemoveItem()
        {
            if (itemList.Count <= 0) return;

            if (itemList[itemList.Count - 1] != null)
            {
                var item = itemList[itemList.Count - 1];
                itemList.RemoveAt(itemList.Count - 1);
                Destroy(item.gameObject);
                itemOffset = new Vector3(itemOffset.x, itemOffset.y - offsetItemY, itemOffset.z);
            }
        }

        public void RemoveAllItems()
        {
            foreach (var item in itemList)
            {
                Destroy(item.gameObject);
            }
            itemList.Clear();
        }

        
    }
}
