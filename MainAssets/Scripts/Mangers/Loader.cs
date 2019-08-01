using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;          //GameManager prefab to instantiate.

    [SerializeField]
    GameObject eventManager;          //GameManager prefab to instantiate.


    void Awake()
    {
        ScreenUtils.Initialize();

        if (EventManager.instance == null)
        {
            //Instantiate EventManager prefab
            Instantiate(eventManager);
        }

        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)
        {
            //Instantiate gameManager prefab
            Instantiate(gameManager);
        }        
        
    }
}
