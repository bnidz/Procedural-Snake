using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//IMPLEMENT UI DISABLE AND ENABE FOR ONSCREEN SNEK CONTROLS

public class SnekManager : MonoBehaviour
{
    public static SnekManager Instance;

    public SnekControls snekCTRL;
    public GameObject snekHed;
    public GameObject Foood;
    public int score = 0;

    private snekmove sm;

    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        DontDestroyOnLoad(this);
        sm = snekHed.GetComponent<snekmove>();
        //GameUI = GameObject.Find("Game UI");
    }

    private void Start()
    {
     HideControls();
     //InstantiateFood();
      
    }

    public void InstantiateFood()
    {
        score++;
        GameObject foodObu = Instantiate(Foood, new Vector3(snekHed.transform.position.x + Random.Range(1,10),
        snekHed.transform.position.y, snekHed.transform.position.z + Random.Range(1, 10)), gameObject.transform.rotation);
        // ShowControls();
    }

    public void HideControls()
    {
        snekCTRL.HideButtons();

    }
    public void ShowControls()
    {

        snekCTRL.ShowButtons();

    }

}

