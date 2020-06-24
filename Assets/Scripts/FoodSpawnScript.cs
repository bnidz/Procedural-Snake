using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
 public class FoodSpawnScript : MonoBehaviour
{
    public static FoodSpawnScript Instance;

    public GameObject snekHed;
    public GameObject Foood;
    public int score;

    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {

        //InstantiateFood();   
    }

    public void InstantiateFood()
    {
        score++;
        GameObject foodObu = Instantiate(Foood, new Vector3(snekHed.transform.position.x + Random.Range(1,10), snekHed.transform.position.y, snekHed.transform.position.z + Random.Range(1, 10)), gameObject.transform.rotation);
    }
}

