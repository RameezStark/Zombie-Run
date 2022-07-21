using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagement
{
    public class ItemManager : MonoBehaviour
    {
        int item;

        public static ItemManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void Add(int amount)
        {
            item += amount;
            for (int i = 0; i < amount; i++)
            {
                ItemStorage.AddToStorage.Invoke();
            }
        }

        public void Remove(int amount)
        {
            if ((item - amount) > 0)
            {
                item -= amount;
            }

            else
            {
                item = 0;
            }

            for (int i = 0; i < amount; i++)
            {
                ItemStorage.RemoveFromStorage.Invoke();
            }
        }

        public int getCount()
        {
            return item;
        }
    }
}
