using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class snekmove : MonoBehaviour
{

  //  private GameObject OnscreenSnekControls;

    public List <GameObject> snekparts;
    public float snekspeed = 10f;
    public float turnPower = 2f;
    float snektimer;

    //snek add part stuff
    public bool skipRemove = false;
    public GameObject snekpartPrefab;

    public float snekScale = 0.1f;

    public bool generateSnek;
    public int startingLenght = 8;

    private GameObject gameUI;
    private SnekControls snekCTRL;



    //UI detection
    private bool IsPointerOverUIObject()
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        if (results.Count > 0) return results[0].gameObject.tag == "excludeUiTouch"; else return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        float snektimer = snekScale / snekspeed;
        generateSnek = true;
        gameUI = GameObject.Find("Game UI");

        snekCTRL = gameUI.GetComponent<SnekControls>();
        snekCTRL.initFromSnek(this);

        SnekManager.Instance.InstantiateFood();

    }

    public bool r_turn;
    public bool l_turn;

    //private void EnableControls()
    //{
    //    SnekManager.Instance.ShowControls();
    //}

    private void snakeInit()
    {
        if (snekparts.Count < startingLenght)
        {
            AddSnekPart();
            if (snekparts.Count >= startingLenght)
                generateSnek = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if(generateSnek)
            {
            snakeInit();
            }

        debug_snekMove();
        SnekBase();
        if (l_turn)
            gameObject.transform.Rotate(0f, -turnPower, 0f, Space.World);
        if (r_turn)
            gameObject.transform.Rotate(0f, turnPower, 0f, Space.World);


    }

    public void Left_turn()
    {
      if(l_turn)
            gameObject.transform.Rotate(0f, -2f, 0f, Space.World);

    }

    public void Right_turn()
    {
       if(r_turn)
            gameObject.transform.Rotate(0f, 2f, 0f, Space.World);

    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddSnekPart();
        }
    }

    public void AddSnekPart()
    {
        skipRemove = true;
    }

    public void SnekBase()
    {
        Debug.Log(snektimer);  
        snektimer -= Time.deltaTime;

        if (snektimer <= 0f && skipRemove)
        {
            GameObject newSnekpart = Instantiate(snekpartPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);
            newSnekpart.transform.localScale = gameObject.transform.localScale;
         //  newSnekpart.transform.parent = gameObject.transform;
            newSnekpart.transform.localScale *= 1.4f;
            snekparts.Add(newSnekpart);


            snektimer = snekScale / snekspeed;
            skipRemove = false;
            //   return;
        }
        if (snektimer <= 0f && !skipRemove)
        {
            GameObject newSnekpart = Instantiate(snekpartPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);
            //   newSnekpart.transform.parent = gameObject.transform;
            newSnekpart.transform.localScale = gameObject.transform.localScale;
            snekparts.Add(newSnekpart);
            //  Destroy(snekparts[snekparts.Count]);
            Destroy(snekparts[0]);
            snekparts.Remove(snekparts[0]);

            snekparts[0].transform.localScale *= .6f;
            snektimer = snekScale / snekspeed;
        }
    }
    public void debug_snekMove()
    {
        gameObject.transform.Translate(transform.forward * snekspeed * Time.fixedDeltaTime, Space.World);

        if (Input.GetKey("d"))
        {
            gameObject.transform.Rotate(0f, 2f, 0f, Space.World);
        }
        if (Input.GetKey("a"))
        {
            gameObject.transform.Rotate(0f, -2f, 0f, Space.World);
        }
    }
}
