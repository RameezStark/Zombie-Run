using UnityEngine;

public class LorryDataManager : MonoBehaviour
{
    public LorryData[] lorryData;

    public static LorryDataManager instance;

    private void Awake()
    {
        instance = this;
    }
}


[System.Serializable]
public class LorryData
{
    public int position;
    public GameObject lorryObj;
}
