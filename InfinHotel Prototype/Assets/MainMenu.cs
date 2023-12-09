using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        EscMenu.isPaused = false;
    }

    public void StartGame()
    {
        //generate starting room
        Debug.Log("Not ready yet");
    }

    public void DemoRoom()
    {
        //open demo room
        SceneManager.LoadScene("TestScene");
    }

    public void Lab()
    {
        //open testing lab
        SceneManager.LoadScene("Lab");
    }
}
