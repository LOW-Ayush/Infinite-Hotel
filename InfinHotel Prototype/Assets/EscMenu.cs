using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    static public bool isPaused;
    public GameObject gamemenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            gamemenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            gamemenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
