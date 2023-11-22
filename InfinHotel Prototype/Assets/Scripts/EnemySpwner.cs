using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwner : MonoBehaviour
{
    public static bool playerSpotted;
    public GameObject Enemy;
    //public Vector2 playerLoc;
    void Start()
    {
        playerSpotted = false;
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawns");
        foreach (GameObject spawn in spawns)
        {
            Vector2 location = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
            GameObject enemy = Instantiate(Enemy, location, Quaternion.identity,this.transform);
            enemy.transform.up = spawn.transform.up;
        }
    }
}
