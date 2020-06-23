using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snekmove : MonoBehaviour
{

    public List <GameObject> snekparts;
    public float snekspeed = 1f;
    float snektimer;

    //snek add part stuff
    public bool skipRemove = false;
    public GameObject snekpartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        float snektimer = 1 / snekspeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            newSnekpart.transform.localScale *= 1.4f;
            snekparts.Add(newSnekpart);


            snektimer = 1 / snekspeed;
            skipRemove = false;
            //   return;
        }
        if (snektimer <= 0f && !skipRemove)
        {
            GameObject newSnekpart = Instantiate(snekpartPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), gameObject.transform.rotation);
            snekparts.Add(newSnekpart);
            //  Destroy(snekparts[snekparts.Count]);
            Destroy(snekparts[0]);
            snekparts.Remove(snekparts[0]);

            snekparts[0].transform.localScale *= .6f;
            snektimer = 1 / snekspeed;
        }
    }
    public void SnekMove()
    {
        transform.Translate(transform.forward * snekspeed * Time.fixedDeltaTime, Space.World);

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
