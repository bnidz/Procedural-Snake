using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class SnekControls : MonoBehaviour
{
    public GameObject ControlButton1, ControlButton2;

    snekmove smove;
    public bool turnRight;
    public bool turnLeft;

    private bool isInit = false;

    //Game Over stuff
    public GameObject GameOverScreen;
    public Text scoreText;


   //public snekmove sm;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if(isInit)
        {
            smove.l_turn = turnLeft;
            smove.r_turn = turnRight;

        }

    }
    public void initFromSnek(snekmove sm)
    {

        smove = sm;
        isInit = true;
    }

    public void Turn_Right()
    {
        turnRight = true;
   
    }

    public void Turn_Right_Stop()
    {

        turnRight = false;
    }
    public void Turn_Left()
    {
        turnLeft = true;
    }
    public void Turn_Left_Stop()
    {
        turnLeft = false;
    }

    public void ShowButtons()
    {
        ControlButton1.SetActive(true);
        ControlButton2.SetActive(true);
    }
    public void HideButtons()
    {
        ControlButton1.SetActive(false);
        ControlButton2.SetActive(false);
    }

    public void Show_GameOverScreen ()
    {
        GameOverScreen.SetActive(true);
        HideButtons();
        scoreText.text = "Your Score: " + SnekManager.Instance.score;

    }
    public void Hide_GameOverScreen()
    {
        GameOverScreen.SetActive(false);
    }

    public void NewGameButton()
    {
        Hide_GameOverScreen();
        ShowButtons();
        SnekManager.Instance.score = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
