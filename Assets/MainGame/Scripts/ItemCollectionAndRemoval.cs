using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagement
{
    public class ItemCollectionAndRemoval : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            

            
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.other.GetComponent<Property>() != null)
            {
                var property = collision.other.GetComponent<Property>();
                property.StopTakingItem();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Property>() != null)
            {
                var property = other.GetComponent<Property>();
                if (property.isUnlocked) return;
                
                property.TakeItem();
            }

            else if(other.GetComponent<Unlocker>() != null)
            {
                var unlocker = other.GetComponent<Unlocker>();

                unlocker.TakeItem();
            }

            else if (other.GetComponent<Item>() != null)
            {
                if (ItemManager.instance.getCount() < PlayerStats.instance.playerItemMaxCapacity)
                {
                    Debug.Log("XXX");
                    ItemManager.instance.Add(1);
                    Property thisProperty = other.GetComponent<Item>().property;
                    Destroy(other.gameObject);

                    if (thisProperty != null)
                    {
                        thisProperty.OnCollect();
                    }
                }

                else
                {
                    Debug.Log("Storage Full");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Unlocker>() != null)
            {
                var unlocker = other.GetComponent<Unlocker>();

                unlocker.StopTakingItem();
            }
        }
    }
}
