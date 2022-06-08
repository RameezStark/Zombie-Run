using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public bool isFull = false;

    [SerializeField]
    GameObject walkingZombie;

    [SerializeField]
    float zombieSpeed = 2f;

    [SerializeField]
    GameObject graveBox;

    [SerializeField]
    GameObject graveClosed;

    bool isWalking = false;
    private void Awake()
    {
        walkingZombie.SetActive(false);
        graveClosed.SetActive(false);
    }

    public void startZombieWalk()
    {
        walkingZombie.SetActive(true);
        isWalking = true;
    }

    private void Update()
    {
        if (!isWalking) return;
        var step = zombieSpeed * Time.deltaTime;
        walkingZombie.transform.position = Vector3.MoveTowards(walkingZombie.transform.position,graveBox.transform.position, step);
        walkingZombie.GetComponent<Animator>().SetBool("Walking", true);

        if (walkingZombie.transform.position == graveBox.transform.position)
        {
            Destroy(walkingZombie);
            graveBox.transform.parent.gameObject.SetActive(false);
            graveClosed.SetActive(true);
            isWalking = false;
        }

    }

}
