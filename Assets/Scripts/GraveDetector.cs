using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveDetector : MonoBehaviour
{
    PlayerZombieBox playerZombieBoxObj;

    [SerializeField]
    Button disposeBtn;
    private void Awake()
    {
        playerZombieBoxObj = GetComponent<PlayerZombieBox>();

        disposeBtn.onClick.RemoveAllListeners();
        disposeBtn.onClick.AddListener(() => DisposeZombies());
        disposeBtn.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.other.GetComponent<Grave>() != null)
        {
            var grave = collision.other.GetComponent<Grave>();

            if (!grave.isFull)
            {
                if (playerZombieBoxObj.zombieList.Count != 0)
                {
                    var zombie = playerZombieBoxObj.zombieList[playerZombieBoxObj.zombieList.Count - 1];
                    playerZombieBoxObj.zombieList.RemoveAt(playerZombieBoxObj.zombieList.Count - 1);
                    Destroy(zombie.gameObject);
                    grave.isFull = true;
                    grave.startZombieWalk();
                    //Debug.Log(grave.isFull);
                }
            }
        }

        else if (collision.other.GetComponent<Disposal>() != null)
        {
            //var disposal = collision.other.GetComponent<Disposal>();
            disposeBtn.gameObject.SetActive(true);
        }

    }

    public void DisposeZombies()
    {
        if(playerZombieBoxObj.zombieList.Count == 0)
        {
            Debug.Log("No zombies Left");
        }

        foreach (var zombie in playerZombieBoxObj.zombieList)
        {
            //add incomplete zombie contition here
            Destroy(zombie.gameObject);
        }

        playerZombieBoxObj.zombieList.Clear();
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.other.GetComponent<Disposal>() != null)
        {
            disposeBtn.gameObject.SetActive(false);
        }
    }
}
