using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]
    Sprite[] colors;

    [SerializeField]
    Sprite[] bars;

    const float noteSpeed = 2f;

    bool scaled = false;

    public bool isBar { get { return scaled; } }

    // Start is called before the first frame update
    void Start()
    {      

    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se chegou ao final, se sim, se remove da tela e avisa que ocorreu um erro;

        //A cada frame anda para esquerda com uma velocidade x (15);
        Vector3 p = gameObject.transform.position;
        p.y -= noteSpeed * Time.deltaTime;
        transform.position = p;
        //Edges Detection
        if ((p.y + GetComponent<SpriteRenderer>().bounds.size.y) < ScreenUtils.ScreenBottom)
        {
            if (GetComponent<Renderer>().enabled && !isBar) {
                //Evento de erro
                EventManager.TriggerEvent(EventName.NoteMissEvent);
            }
            else
            {
                GetComponent<Renderer>().enabled = true;                
            }
            //Retorna ao factory
            GameManager.instance.NPool.ReturnPrefab2Pool(this.gameObject);

        }
    }

    public void setNoteColor(int c)
    {
        if (scaled)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size;
            scaled = false;
        }
            
        this.gameObject.GetComponent<SpriteRenderer>().sprite = colors[c];
    }

    public void setbarShape(int c, int size)
    {
        
        //Time.timeScale = 0;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = bars[c];
        if (!scaled) { 
            Vector3 scale = transform.localScale;
            scale.y *= size;
            transform.localScale = scale;

            GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().bounds.size/size;

            Vector3 pos = transform.position;
            pos.y += GetComponent<SpriteRenderer>().bounds.size.y/2 ;
            transform.position = pos;
            scaled = true;
        }
    }
}
