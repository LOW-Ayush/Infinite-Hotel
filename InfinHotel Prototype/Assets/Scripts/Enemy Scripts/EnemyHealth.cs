using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //this script will work on every enemy

    public float MaxHealth;
    public float CurrentHealth;
    private bool ready;
    private Sprite deathsprite;

    //sets max health from parent script
    public void SetupHealthManager(float sethealth, Sprite sprite)
    {
        MaxHealth = sethealth;
        CurrentHealth = MaxHealth;

        deathsprite = sprite;
        ready = true;
    }

    public void ReduceHealth(float damage)
    {
        CurrentHealth = CurrentHealth - damage;
    }

    private void FixedUpdate()
    {
        if (ready)
        {
            if (CurrentHealth == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = deathsprite;
                Debug.Log("enemy dead");
                gameObject.BroadcastMessage("Death");
                gameObject.GetComponent<EnemyHealth>().enabled = false;
            }
        }
    }
}
