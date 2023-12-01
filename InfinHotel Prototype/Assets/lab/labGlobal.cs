using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labGlobal : MonoBehaviour
{
    public GameObject Target;
    private Vector2 _pointSeen;
    public int Index;
    private float[] arry;
    public float closest;

    void Start()
    {
        
    }


    void Update()
    {
        _pointSeen = Target.transform.position;

        int iter = 0;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        float[] dists = new float[objs.Length];
        foreach (GameObject obj in objs)
        {
            dists[iter] = Vector2.Distance(obj.transform.position, _pointSeen);
            iter++;
        }
        Index = ProximityCheck(dists);
        closest = arry[Index];

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(Index + " " + closest);
        }
    }

    public int ProximityCheck(float[] array)
    {
        float value = float.PositiveInfinity;
        int index = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < value)
            {
                index = i;
                value = array[i];
            }
        }
        arry = array;
        return index;
    }
}
