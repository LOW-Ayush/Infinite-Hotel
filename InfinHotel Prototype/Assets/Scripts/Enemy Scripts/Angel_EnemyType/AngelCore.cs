using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelCore : MonoBehaviour
{
    public string coreSize;

    private EnemyHealth health;
    public Sprite deathSprite;

    public float visioncone;
    public float visionrange;
    void Start()
    {
        //spawner will dictate size
        CoreSize(coreSize);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    //determine values by core size
    private void CoreSize(string size)
    {
        AngelBehaviour behav = gameObject.GetComponent<AngelBehaviour>();
        switch (size)
        {
            case "small":
                health.SetupHealthManager(1,deathSprite);
                behav.SetVisionCone(45, 1);
                break;

            case "medium":
                health.SetupHealthManager(Random.Range(2, 3), deathSprite);
                behav.SetVisionCone(90, 1.5f);
                break;

            case "large":
                health.SetupHealthManager(Random.Range(3, 4), deathSprite);
                behav.SetVisionCone(180, 2);
                break;

            case null:
                Debug.Log("Error: No core size declared for Angel enemy instance: " + gameObject.name);
                break;
        }
    }

    public void Death()
    {
        gameObject.GetComponent<AngelCore>().enabled = false;
        gameObject.SetActive(false);
    }
}
