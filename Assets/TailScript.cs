using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TailScript : MonoBehaviour
{
    private bool alreadyCollided = false;
    private GameObject gameUI;
    private SnekControls sc;

    private void Start()
    {
        gameUI = GameObject.Find("Game UI");
        sc = gameUI.GetComponent<SnekControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (alreadyCollided)
                return;
                alreadyCollided = true;
            //  Time.timeScale = 0;
            other.gameObject.SetActive(false);
            sc.Show_GameOverScreen();


        }
    }
}
