using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerInGameState : GameManagerState
{
    //The score is being calculated here and in HUD. A problem to be solved.
    const int multFactor = 5;
    const int scoreFactor = 1;
    float currentScore = 0;
    float currentMult = 1;
    int hitSequence = 0;
    int totalHits = 0;
    bool paused = false;

    public GameManagerInGameState(GameManager gm) : base(gm)
    {
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            MenuManager.GoToMenu(MenuName.Pause);
            paused = true;
        }
    }

    public override void OnStateEnter()
    {
        EventManager.StartListening(EventName.GameOverEvent, GameOverEventListener);
        EventManager.StartListening(EventName.MusicOvreEvent, MusicOverEventListener);
        EventManager.StartListening(EventName.NoteHitEvent, OnNoteHit);
        EventManager.StartListening(EventName.GuitarErrorEvent, OnGuitarError);
        EventManager.StartListening(EventName.NoteMissEvent, OnNoteMiss);
        EventManager.StartListening(EventName.UnPausedEvent, UnPause);

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void UnPause(Hashtable e)
    {
        paused = false;
    }

    private void MusicOverEventListener(Hashtable e)
    {

        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("EndGameMenu"));            
        go.GetComponent<EndGame>().setEndingValues((int)currentScore, totalHits);
    }

    public override void OnStateExit()
    {
        gm.ClearInGameComponents();
        EventManager.StopListening(EventName.GameOverEvent, GameOverEventListener);
        EventManager.StopListening(EventName.MusicOvreEvent, MusicOverEventListener);
        EventManager.StopListening(EventName.NoteHitEvent, OnNoteHit);
        EventManager.StopListening(EventName.GuitarErrorEvent, OnGuitarError);
        EventManager.StopListening(EventName.NoteMissEvent, OnNoteMiss);
        EventManager.StopListening(EventName.UnPausedEvent, UnPause);
    }

    public void GameOverEventListener(Hashtable eventParams)
    {        
        gm.SetState(new GameManagerMenuState(gm));
    }

    //Ao carregar a cena do jogo inicializa os components.
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        gm.InitInGameComponents();
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    //Score Functions
    private void OnNoteMiss(Hashtable e)
    {
        hitSequence = 0;
        currentMult = 1;        
    }

    private void OnGuitarError(Hashtable e)
    {
        OnNoteMiss(e);
    }

    private void OnNoteHit(Hashtable e)
    {
        hitSequence++;
        totalHits++;
        if (hitSequence >= multFactor)
        {
            currentMult++;
            hitSequence = 0;
        }
        currentScore += ((float)e[EventParamName.FloatHitPoints]) * currentMult;
    }
}
