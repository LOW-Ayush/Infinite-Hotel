using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAttach : MonoBehaviour
{
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0,0,-10);
        transform.position = player.position + offset;
    }
}
