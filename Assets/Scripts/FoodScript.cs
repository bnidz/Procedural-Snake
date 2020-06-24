using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodScript : MonoBehaviour
{

 public   bool alreadyCollected = false;
    private GameObject snekHEed;
    private snekmove sm;


    private void Awake()
    {

        snekHEed = GameObject.Find("snekhead");
     sm = snekHEed.GetComponent<snekmove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
        if (alreadyCollected)
            return;
        alreadyCollected = true;

            sm.AddSnekPart();
            FoodSpawnScript.Instance.InstantiateFood();
            Destroy(gameObject);
        }
    }

}
