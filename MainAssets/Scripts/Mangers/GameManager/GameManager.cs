using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;
    //Pool of Notes GameObjects
    PrefabPool NotesPool;
    public PrefabPool NPool { get { return NotesPool; } }
    //Notes Spawner
    GameObject NoteSpawner;
    //Current State of the game
    private GameManagerState currentState;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }        
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }           
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the first state 
        InitGame();
    }

    void InitGame()
    {
        SetState(new GameManagerMenuState(instance));
    }

    public void SetState(GameManagerState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
            
        currentState = state;
        gameObject.name = state.GetType().Name;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }            
    }

    public void InitInGameComponents()
    {
        if (NotesPool == null)
        {
            NotesPool = new PrefabPool(Resources.Load<GameObject>("Note"));
        }
        NoteSpawner = GameObject.Instantiate(Resources.Load<GameObject>("NoteSpawner"));
    }

    public void ClearInGameComponents()
    {
        NotesPool.ClearPool();
        Destroy(NoteSpawner);
    }

    /*
    //Initializes the game for each level.
    void InitGame()
    {       
        NotesPool = new PrefabPool(Resources.Load<GameObject>("Note"));
        NoteSpawner = GameObject.Instantiate(Resources.Load<GameObject>("NoteSpawner"));        
    }*/

    private void Update()
    {
        currentState.Tick();
    }
}
