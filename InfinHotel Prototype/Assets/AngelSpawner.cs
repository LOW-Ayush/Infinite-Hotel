using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelSpawner : MonoBehaviour
{
    public GameObject Angel;
    public int SizeDeterminer;
    public bool testOn;
    void Start()
    {
        
    }

    //only for testing purposes
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V) && testOn)
        {
            GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawns");
            foreach (GameObject spawn in spawns)
            {
                Vector2 location = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
                GameObject enemy = Instantiate(Angel, location, Quaternion.identity, this.transform);
                enemy.transform.up = spawn.transform.up;

                switch (SizeDeterminer)
                {
                    case 1:
                        enemy.BroadcastMessage("CoreSize", "small");
                        break;

                    case 2:
                        enemy.BroadcastMessage("CoreSize", "small");
                        break;

                    case 3:
                        enemy.BroadcastMessage("CoreSize", "small");
                        break;

                    default:
                        enemy.BroadcastMessage("CoreSize", null);
                        break;
                }
            }
        }
    }
}
