using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    
    public GameObject targetobject;
    public float rotatespeed = 10.0f;
    public float ScrollSpeed = 6.0f;
    public float zoomspeed = 0.3f;
    private float _scrollInput;

    void Start()
    {
        transform.LookAt(targetobject.transform.position);
    }
 
    void Update () {
        //Rotate the Entire Camera around the relevant Object
        transform.RotateAround(targetobject.transform.position, new Vector3(0.0f, -1.0f, 0.0f), 20 * Time.deltaTime * rotatespeed);
 

        
        if(_scrollInput > 0  && Camera.main.orthographicSize == 30f) 
        {
            Debug.Log(_scrollInput);
            if(_zooming == null)
                _zooming = StartCoroutine(ZoomTo(Camera.main.orthographicSize - ScrollSpeed, zoomspeed));
            //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Camera.main.orthographicSize - ScrollSpeed, zoomspeed);
        }

        if(_scrollInput < 0 && Camera.main.orthographicSize == 30f-ScrollSpeed) 
        {
            if(_zooming == null)
                _zooming = StartCoroutine(ZoomTo(Camera.main.orthographicSize + ScrollSpeed, zoomspeed));
            //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Camera.main.orthographicSize + ScrollSpeed, zoomspeed);
        }
        
        //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed, Time.deltaTime * zoomspeed);
     
    }

    public void OnScroll(InputAction.CallbackContext context) 
    {
        _scrollInput = context.ReadValue<Vector2>().normalized.y;
        
        Debug.Log(_scrollInput);
    } 

    // Store a reference to our coroutine so we can interrupt it if needed.
    Coroutine _zooming;

// "IEnumerator" marks this as a routine that can pause & automatically 
// resume at a later time (say, advancing a little each frame)
    IEnumerator ZoomTo(float newFov, float duration) {

    // Remember where we started from, so we can smoothly control the curve.
        float originalFov = Camera.main.orthographicSize;
        float speed = 1f/duration;
        float t = 1.0f;

        // Loop multiple times until we've used up our full duration.
        do {
        // Blend one more step toward our destination value.
        t = Mathf.Clamp01(t - speed * Time.deltaTime);
        Camera.main.orthographicSize = Mathf.Lerp(newFov, originalFov, t*t);

        // Return control to the main game thread for one frame, 
        // then resume next frame.
        yield return null;
        } while(t > 0f);

        // We've finished our work, so clear the stored coroutine to signal this.
        _zooming = null;
    }  
} 