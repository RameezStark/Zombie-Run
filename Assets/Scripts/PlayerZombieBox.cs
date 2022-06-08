using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZombieBox : MonoBehaviour
{
    [SerializeField]
    GameObject zombiePrefab;

    [SerializeField]
    GameObject zombieHolder;

    [SerializeField]
    int zombieTotalCount = 7;

    [SerializeField]
    int numberZombiesPerStack = 3;

    Vector3 zombieOffset;

    [SerializeField]
    float offsetZombieX = 1f;

    [SerializeField]
    float offsetZombieY = 1f;

    //GameObject zombie;

    [SerializeField]
    float gapBetweenZombieStacks;

    Bounds zombieBounds;

    public List<GameObject> zombieList = new List<GameObject>();


    private void Awake()
    {

    }
    private void Start()
    {
        FillZombieBox();

    }

    public void FillZombieBox()
    {
        float zombieHolderSizeY = zombieHolder.GetComponent<Renderer>().bounds.size.y;
        float zombieHolderTop = zombieHolder.transform.localPosition.y + zombieHolderSizeY / 2;

        float zombieHolderSizeZ = zombieHolder.GetComponent<Renderer>().bounds.size.z;
        float zombieHolderEdge = zombieHolderSizeZ / 2;

        zombieOffset.x += offsetZombieX;
        zombieOffset.y += zombieHolderTop;
        zombieOffset.z += zombieHolderEdge;

        int stackCounter = 0;
        //zombieArray = new GameObject[zombieTotalCount];
        for (int i = 0; i < zombieTotalCount; i++)
        {
            var zombie = Instantiate(zombiePrefab, zombieHolder.transform.parent.transform);
            /*var rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = -90;
            zombie.transform.rotation = Quaternion.Euler(rotationVector);*/
            zombie.transform.position = zombieHolder.transform.position;


            if (i == 0) //first stack
            {
                GetBounds(zombie);
                zombieOffset.z -= (zombieBounds.size.z / 2) + gapBetweenZombieStacks;
            }

            zombie.transform.localPosition += zombieOffset;
            zombieOffset = new Vector3(zombieOffset.x, zombieOffset.y + offsetZombieY, zombieOffset.z);


            //zombie.transform.localScale.y

            stackCounter++;

            if (stackCounter >= numberZombiesPerStack)  //Next Stack
            {
                zombieOffset = new Vector3(zombieOffset.x, zombieHolderTop, zombieOffset.z - (zombieBounds.size.z + gapBetweenZombieStacks));
                stackCounter = 0;
            }

            zombieList.Add(zombie.gameObject);
        }
    }

    public void GetBounds(GameObject zombie)
    {
        zombieBounds = zombie.GetComponent<BoxCollider>().bounds;

        //zombieBounds.size.x = offsetZombieX;
    }

    
}
