using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCameraMovement : MonoBehaviour
{
    public enum ThirdPersonCameraType
    {
        Track,
        Follow,
    }
    public ThirdPersonCameraType _ThirdPersonCameraType = ThirdPersonCameraType.Track;
    public Transform Player;
    public float PlayerHeight = 2.0f;

    public Vector3 PositionOffset = new Vector3(0.0f, 2.0f, -2.5f);
    public Vector3 AngleOffset = new Vector3(0.0f, 0.0f, 0.0f);
    [Tooltip("The damping factor to smooth the changes " + "in position and rotation of the camera.")]
    public float Damping = 1.0f;

    void Awake()
    {
        Vector3 targetPos = Player.transform.position;
        targetPos.y += PositionOffset.y;
        transform.LookAt(targetPos);
    }

    void LateUpdate()
      {
        switch (_ThirdPersonCameraType)
        {
        case ThirdPersonCameraType.Track:
            {
                CameraMove_Track();
                break;
            }
        case ThirdPersonCameraType.Follow:
            {
                CameraMove_Follow();
                break;
            }
        }
    }

    void CameraMove_Track()
    {
        Vector3 targetPos = Player.transform.position;
        // We removed PlayerHeight and replaced
        // with the PositionOffset.y
        targetPos.y += PositionOffset.y;
        transform.LookAt(targetPos);
    }

    void CameraMove_Follow(bool allowRotationTracking = true)
    {
        // We apply the initial rotation to the camera.
        Quaternion initialRotation = Quaternion.Euler(AngleOffset);

        // added the following code to allow rotation 
        // tracking of the player
        // so that our camera rotates when the player 
        // rotates and at the same
        // time maintain the initial rotation offset.
        if (allowRotationTracking)
        {
            Quaternion rot = Quaternion.Lerp(transform.rotation, Player.rotation * initialRotation, Time.deltaTime * Damping);
            transform.rotation = rot;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, Damping * Time.deltaTime);
        }

        // Now we calculate the camera transformed axes.
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;
        Vector3 up = transform.rotation * Vector3.up;

        // We then calculate the offset in the 
        // camera's coordinate frame.
        Vector3 targetPos = Player.position;
        Vector3 desiredPosition = targetPos + forward * PositionOffset.z + right * PositionOffset.x + up * PositionOffset.y;

        // Finally, we change the position of the camera, 
        // not directly, but by applying Lerp.
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * Damping);
        transform.position = position;
    }
}
