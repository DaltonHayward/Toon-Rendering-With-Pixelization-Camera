using UnityEngine;
using System.Collections;
 
public class SunRotation : MonoBehaviour
{
 
    [HideInInspector]
    public GameObject sun;
    [HideInInspector]
    public Light sunLight;
 
    [Range(0, 24)]
    public float timeOfDay = 12;
 
    public float secondsPerMinute = 60;
    [HideInInspector]
    public float secondsPerHour;
    [HideInInspector]
    public float secondsPerDay;
 
    public float timeMultiplier = 1;
 
    void Start()
    {
        sun = gameObject;
        sunLight = gameObject.GetComponent<Light>();
    }
 
    // Update is called once per frame
    void Update()
    {
        SunUpdate();
    }
 
    public void SunUpdate()
    {
        //30,-30,0 = sunrise
        //90,-30,0 = High noon
        //180,-30,0 = sunset
        //-90,-30,0 = Midnight
 
        //sun.transform.localRotation = Quaternion.Euler((timeOfDay / 24) * 360 - 0, -30, 0);
        sun.transform.localEulerAngles = new Vector3(30, Time.time * timeMultiplier, 0);
    }
}
