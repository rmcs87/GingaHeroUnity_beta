using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMenuState : GameManagerState
{

    public GameManagerMenuState(GameManager gm) : base(gm)
    {

    }

    public override void Tick()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnStateEnter()
    {
        EventManager.StartListening(EventName.GameStartedEvent, GameStartedEventListener);
    }

    public override void OnStateExit()
    {
        EventManager.StopListening(EventName.GameStartedEvent, GameStartedEventListener);
    }

    public void GameStartedEventListener(Hashtable eventParams)
    {        
        gm.SetState(new GameManagerInGameState(gm));
    }

}