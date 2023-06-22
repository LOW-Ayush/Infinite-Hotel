using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwner : MonoBehaviour
{
    public GameObject Enemy;
    void Start()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawns");
        foreach (GameObject spawn in spawns)
        {
            Vector2 location = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
            GameObject enemy = Instantiate(Enemy, location, Quaternion.identity);
            enemy.transform.up = spawn.transform.up;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
