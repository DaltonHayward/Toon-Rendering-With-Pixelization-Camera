using UnityEngine;
using System.Collections;
 
public class MoonRotation : MonoBehaviour
{
 
    [HideInInspector]
    public GameObject moon;
    [HideInInspector]
    public Light moonLight;
 
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
        moon = gameObject;
        moonLight = gameObject.GetComponent<Light>();
    }
 
    // Update is called once per frame
    void Update()
    {
        MoonUpdate();
    }
 
    public void MoonUpdate()
    {
        //30,-30,0 = sunrise
        //90,-30,0 = High noon
        //180,-30,0 = sunset
        //-90,-30,0 = Midnight
 
        //sun.transform.localRotation = Quaternion.Euler((timeOfDay / 24) * 360 - 0, -30, 0);
        moon.transform.localEulerAngles = new Vector3(Time.time * -timeMultiplier, -30, 0);
    }
}
