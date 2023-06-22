using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertLvl : MonoBehaviour
{
    [SerializeField] private GunmanScrp Gunman_Scrp;

    public bool AlreadyAlerted;
    private SpriteRenderer icon;
    private float timer;
    public int spriteNum;

    public Sprite Idle;
    public Sprite Suspicious;
    public Sprite Alert;
    public int Mode;
    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<SpriteRenderer>();
        Idle = Resources.Load<Sprite>("AlertStatus/IdleMarker");
        Suspicious = Resources.Load<Sprite>("AlertStatus/SuspiciousMarker");
        Alert = Resources.Load<Sprite>("AlertStatus/AlertMarker");
        AlreadyAlerted = false;

    }
    private void Update()
    {
        AlertMode(Mode);
        GunmanScrp Alertset = GetComponentInParent<GunmanScrp>();
        Mode = Alertset.AlertLvl;
    }
    public void AlertMode(int mode)
    {
        transform.up = Vector3.up;
        spriteNum = mode;
        switch (mode)
        {
            case 1:
                icon.sprite = Idle;
                break;
            case 2:
                bool sus = true;
                while (sus == true)
                {
                    icon.sprite = Suspicious;
                    sus = false;
                }
                break;
            case 3:
                icon.sprite = Alert;
                timer += Time.deltaTime;
                while (timer < 2 && !AlreadyAlerted)
                {
                    Debug.Log("Alert!");
                    icon.sprite = Alert;
                    AlreadyAlerted = true;
                }
                break;
            default:
                Debug.Log("default");
                icon.sprite = null;
                break;
        }
    }
}
