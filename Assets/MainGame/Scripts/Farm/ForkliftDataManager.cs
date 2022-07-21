using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftDataManager : MonoBehaviour
{
   
    public ForkliftDataItem[] forklifts;

    public static ForkliftDataManager instance;

    private void Awake()
    {
        instance = this;
    }
}

[System.Serializable]
public class ForkliftDataItem
{
    public Vector3 position;
    public GameObject forkliftObj;
}
