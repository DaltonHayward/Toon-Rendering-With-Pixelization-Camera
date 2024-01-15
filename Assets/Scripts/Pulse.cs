using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    private bool grow = true;
    [SerializeField]private float maxRange = 4;
    [SerializeField]private float minRange = 1;
    [SerializeField]private float step = .1f;
    void Update()
    {
        if (GetComponent<Light>().range < maxRange && grow) {
            GetComponent<Light>().range += step * Time.deltaTime;
        }
        
        else if (GetComponent<Light>().range > minRange && !grow) {
            GetComponent<Light>().range -= step * Time.deltaTime;
        }

        else {
            grow = !grow;
        }
    }
}
