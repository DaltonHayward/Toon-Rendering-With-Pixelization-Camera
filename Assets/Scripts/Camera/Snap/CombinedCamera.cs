using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombinedCamera : MonoBehaviour
{
    [SerializeField]
    GameObject Character_Reference;

    Quaternion lockedRotation;

    float snapDistance = 0.075f;
    float lerpSpeed = 5f;

    void Awake() 
    {
        transform.parent = null;
        lockedRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = lockedRotation;
        transform.position = Character_Reference.transform.position;
    }

    void FixedUpdate()
    {
        CameraLagDOTWEEN();
    }

    void CameraLagLerp() 
    {
        float t = Time.deltaTime * lerpSpeed;
        transform.position = Vector3.Lerp(transform.position, Character_Reference.transform.position, t);

        if (Vector3.Distance(transform.position, Character_Reference.transform.position) < snapDistance)
        {
            transform.position = Character_Reference.transform.position;
        }
    }

    void CameraLagDOTWEEN() 
    {
        Tween myTween;
        if (Vector3.Distance(transform.position, Character_Reference.transform.position) < snapDistance)
        {
            myTween = transform.DOMove(Character_Reference.transform.position, .25f);
        }

        myTween = transform.DOMove(Character_Reference.transform.position, 1f);
    }
}
