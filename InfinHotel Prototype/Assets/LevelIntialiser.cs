using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntialiser : MonoBehaviour
{
    public GameObject MenuScreen;
    public GameObject DeathMenu;


    void Start()
    {
        EscMenu.playerDeath = false;
        EscMenu.isPaused = false;
        Time.timeScale = 1;
        MenuScreen.SetActive(false);
        DeathMenu.SetActive(false);
        BroadcastMessage("SpawnEnemies");
    }
}
