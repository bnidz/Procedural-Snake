using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snekmove : MonoBehaviour
{

    public List <GameObject> snekparts;
    public float snekspeed = 10f;
    float snektimer;

    //snek add part stuff
    public bool skipRemove = false;
    public GameObject snekpartPrefab;

    public float snekScale = 0.1f;

    public bool generateSnek;
    public int startingLenght = 8;

    // Start is called before the first frame update
    void Start()
    {
        float snektimer = snekScale / snekspeed;
        generateSnek = true;
    }

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

        SnekMove();
        SnekBase();
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
    public void SnekMove()
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
