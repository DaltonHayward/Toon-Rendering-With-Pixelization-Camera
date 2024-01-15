using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]
    GameObject CombinedCamera;

    [SerializeField]
    GameObject SceneCamera;

    Camera OutputCamera;
    Quaternion lockedRotation;

    static float rotationAmount = 250f;
    static float centerOffsetValue = -85f;
    float pixelsPerUnitX = 10f;
    float pixelsPerUnitZ = 5f;
    float cameraOffsetX = 0f;
    float cameraOffsetZ = centerOffsetValue;
    float followX;
    float followZ;
    float snappedX;
    float snappedZ;
    float targetAngle = 0;
    float rotateState = 1;

    void Awake() 
    {
        OutputCamera = GetComponent<Camera>();
        lockedRotation = transform.rotation;
    }

    void LateUpdate() 
    {
        if (targetAngle != 0) 
        {
            RotateCamera(rotationAmount * Time.deltaTime);
        }
        else 
        {
            Snap();
            SnapViewport();
        }
    }

    public void CameraZoom() {}

    public void RotateCameraLeft(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            targetAngle -= 90.0f;
            FixSnap(-1);
        }
    }

    public void RotateCameraRight(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            targetAngle += 90.0f;
            FixSnap(1);
        }
    }

    void RotateCamera(float timeRotationAmount) 
    {
        if (targetAngle > 0) 
        {
            if (targetAngle < timeRotationAmount)
            {
                transform.RotateAround(CombinedCamera.transform.position, Vector3.up, -targetAngle);
                targetAngle = 0;
            }
            else
            {
                transform.RotateAround(CombinedCamera.transform.position, Vector3.up, -timeRotationAmount);
                targetAngle -= timeRotationAmount;
            }
        }
        else if (targetAngle < 0)
        {
            if (targetAngle > -timeRotationAmount)
            {
                transform.RotateAround(CombinedCamera.transform.position, Vector3.up, -targetAngle);
                targetAngle = 0;
            }
            else 
            {
                transform.RotateAround(CombinedCamera.transform.position, Vector3.up, timeRotationAmount);
                targetAngle += timeRotationAmount;
            }
        }
    }

    void FixSnap(int stateChange) 
    {
        rotateState = rotateState - stateChange;
        if (rotateState < 1) 
        {
            rotateState = 4;
        }
        else if (rotateState > 4) 
        {
            rotateState = 1;
        }
        switch (rotateState)
        {
            case 1:
                cameraOffsetX = 0;
                cameraOffsetZ = centerOffsetValue;
                pixelsPerUnitX = 10f;
                pixelsPerUnitZ = 5f;
                lockedRotation = Quaternion.Euler(30, 0, 0);
                break;
            case 2:
                cameraOffsetX = centerOffsetValue;
                cameraOffsetZ = 0;
                pixelsPerUnitX = 5f;
                pixelsPerUnitZ = 10f;
                lockedRotation = Quaternion.Euler(30, 90, 0);
                break;
            case 3:
                cameraOffsetX = 0;
                cameraOffsetZ = -centerOffsetValue;
                pixelsPerUnitX = 10f;
                pixelsPerUnitZ = 5f;
                lockedRotation = Quaternion.Euler(30, 180, 0);
                break;
            case 4:
                cameraOffsetX = -centerOffsetValue;
                cameraOffsetZ = 0;
                pixelsPerUnitX = 5f;
                pixelsPerUnitZ = 10f;
                lockedRotation = Quaternion.Euler(30, 270, 0);
                break;
        }
    }

    void Snap()
    {
        transform.rotation = lockedRotation;

        followX = CombinedCamera.transform.position.x + cameraOffsetX;
        followZ = CombinedCamera.transform.position.z + cameraOffsetZ;

        snappedX = Mathf.Round(followX * pixelsPerUnitX) / pixelsPerUnitX;
        snappedZ = Mathf.Round(followZ * pixelsPerUnitZ) / pixelsPerUnitZ;

        transform.position = new Vector3(snappedX, transform.position.y, snappedZ);
    }


    void SnapViewport()
    {
        SceneCamera.transform.position = new Vector3(followX, transform.position.y, followZ);
    }
}
