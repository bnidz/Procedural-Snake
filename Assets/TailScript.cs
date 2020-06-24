using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailScript : MonoBehaviour
{
    private bool alreadyCollided;
    private SnekControls sc;

    private void Start()
    {
        sc = FindObjectOfType<SnekControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (alreadyCollided)
                return;
                alreadyCollided = true;
            sc.Show_GameOverScreen();

            Time.timeScale = 0;

        }
    }
}
