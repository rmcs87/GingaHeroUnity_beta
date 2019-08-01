using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject score;

    [SerializeField]
    GameObject mul;

    const int multFactor = 5;
    const int scoreFactor = 1;
    float currentScore = 0;
    float currentMult = 1;
    int hitSequence = 0;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI multText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = score.GetComponent<TextMeshProUGUI>();
        multText = mul.GetComponent<TextMeshProUGUI>();
        EventManager.StartListening(EventName.NoteHitEvent,OnNoteHit);
        EventManager.StartListening(EventName.GuitarErrorEvent, OnGuitarError );
        EventManager.StartListening(EventName.NoteMissEvent, OnNoteMiss);
    }

    private void OnNoteMiss(Hashtable e)
    {
        hitSequence = 0;
        currentMult = 1;
        multText.SetText("MULTI: x" + currentMult);
    }

    private void OnGuitarError(Hashtable e)
    {
        OnNoteMiss(e);
    }

    private void OnNoteHit(Hashtable e)
    {
        hitSequence++;
        if(hitSequence >= multFactor)
        {
            currentMult++;
            hitSequence = 0;
            multText.SetText("MULTI: x" + currentMult);
        }
        currentScore += ( (float)e[EventParamName.FloatHitPoints]) * currentMult;
        scoreText.SetText("SCORE: " + (int)currentScore);
    }

}
