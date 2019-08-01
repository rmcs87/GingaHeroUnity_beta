using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    PrefabPool NotePool;
    MusicUtil musica;

    const float x0 = -.76f;      //Position of blue note;
    const float xStep = .6f;

    float elapsedtime = 0;

    //float aps = 3.5f;   //Tempo entre notas
    float aps = 0.281f;   //Tempo entre notas

    // Start is called before the first frame update
    void Start()
    {
        NotePool = GameManager.instance.NPool;        
        musica = new MusicUtil();        
    }


    // Update is called once per frame
    void Update()
    {
        if(elapsedtime > aps)
        {
            elapsedtime = 0;
            int[] acorde = musica.getNextNote();
            if(acorde == null)
            {
                EventManager.TriggerEvent(EventName.MusicOvreEvent);
            }
            else
            {
                initAcorde(acorde);
            }            
        }
        else
        {
            elapsedtime += Time.deltaTime;
        }
    }

    void initAcorde(int [] acorde)
    {
        for (int i = 0; i < 4; i++)
        {
            if (acorde[i] == 0)
            {
                continue;
            }else if (acorde[i] == 1)
            {
                GameObject go = NotePool.GetPrefab();
                go.GetComponent<Note>().setNoteColor(i);
                //Coloca na posição inicial correta;
                Vector3 pos = go.transform.position;
                pos.x = x0 + i * xStep;
                pos.y = ScreenUtils.ScreenTop;
                go.transform.position = pos;
            }
            else
            {
                GameObject go = NotePool.GetPrefab();                
                //Coloca na posição inicial correta;
                Vector3 pos = go.transform.position;
                pos.x = x0 + i * xStep;
                pos.y = ScreenUtils.ScreenTop;
                go.transform.position = pos;
                //Barra: escala e reposiciona
                go.GetComponent<Note>().setbarShape(i, acorde[i]);
            }            
        }
    }


}
