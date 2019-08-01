using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Mapping Key from Keyboard
    [SerializeField]
    int key;

    [SerializeField]
    Sprite spriteHit;

    [SerializeField]
    Sprite spriteMiss;

    Sprite spriteIdle;

    SpriteRenderer spriteTexture;

    bool buttonPressed = false;
    bool noteColliding = false;
    GameObject collidingWith; 

    // Start is called before the first frame update
    void Start()
    {
        spriteTexture = GetComponent<SpriteRenderer>();
        spriteIdle = spriteTexture.sprite;
    }

    // Update is called once per frame
    void Update()
    {

        if (mouseClickedOnThisPaddle())
        {
            buttonPressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            buttonPressed = false;
            spriteTexture.sprite = spriteIdle;
        }

        if (Input.GetKeyDown(key.ToString()))
        {
            buttonPressed = true;
        }else if (Input.GetKeyUp(key.ToString()))
        {
            buttonPressed = false;
            spriteTexture.sprite = spriteIdle;
        }

        if ((noteColliding && !collidingWith.GetComponent<Note>().isBar && buttonPressed))
        {
            //GameManager.instance.NPool.ReturnPrefab2Pool(collidingWith);
            collidingWith.GetComponent<Renderer>().enabled = false;
            noteColliding = false;
            collidingWith = null;
            //Evento de acerto
            EventManager.TriggerEvent(EventName.NoteHitEvent,
                                        new Hashtable() { { EventParamName.FloatHitPoints, 1f } });
            spriteTexture.sprite = spriteHit;
            buttonPressed = false;
        }else if ( (noteColliding && collidingWith.GetComponent<Note>().isBar && buttonPressed))
        {
            EventManager.TriggerEvent(EventName.NoteHitEvent,
                                        new Hashtable() { { EventParamName.FloatHitPoints, 0.1f } });
            spriteTexture.sprite = spriteHit;
        }else if (buttonPressed)
        {
            EventManager.TriggerEvent(EventName.GuitarErrorEvent);
            spriteTexture.sprite = spriteMiss;
            buttonPressed = false;
        }

     }

    bool mouseClickedOnThisPaddle()
    {
        if (Input.GetMouseButtonDown(0)) { 
            Vector3 mP = Input.mousePosition;
            Vector3 wP = Camera.main.ScreenToWorldPoint(
                                                new Vector3(mP.x, mP.y, Camera.main.nearClipPlane)
                                             );

            //if (GetComponent<BoxCollider2D>().bounds.Contains(wP))
            if (manualColisionDetection(this.gameObject, wP))
            {
                return true;
            }
        }
        return false;
    }

    bool manualColisionDetection(GameObject go, Vector2  point)
    {
        Vector3 goSize = go.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 goPosition = go.transform.position;
        /*print("--------------------"+transform.name+"-------------------------------");
        print("point" + point);
        print("size" + goSize);
        print("pos" + goPosition);  */   
        //Time.timeScale = 0;
        if (point.y > goPosition.y + (goSize.y / 2) )
        {
            return false;
        }
        else if(point.y < goPosition.y - goSize.y / 2 )
        {
            return false;
        }
        else if (point.x > goPosition.x + (goSize.x / 2))
        {
            return false;
        }
        else if (point.x < goPosition.x - goSize.x / 2 )
        {
            return false;
        }
        return true;
    }
    /*
    //Jogando com o Mouse
    //Bug: com o overlaping dos colisores, da problema em pegar o evento
    private void OnMouseDown()
    {
        if (noteColliding)
        {
            GameManager.instance.NPool.ReturnPrefab2Pool(collidingWith);
            noteColliding = false;
        }
        //Evento de acerto
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        noteColliding = true;
        collidingWith = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        noteColliding = false;
        collidingWith = null;
        spriteTexture.sprite = spriteIdle;
        buttonPressed = false;
    }
}
