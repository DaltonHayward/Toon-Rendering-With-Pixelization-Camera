using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float _rotate;
    private Vector2 _movementInput;

    public float PlayerSpeed;
    public float RotationSpeed;
  
    public void OnMove(InputAction.CallbackContext context) 
    {
        _movementInput = context.ReadValue<Vector2>();
        Debug.Log(_movementInput);
    } 

    public void OnLook(InputAction.CallbackContext context) 
    {
        _rotate = context.ReadValue<Vector2>().x;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    public void MovePlayer() {
        // change movement based on current look direction
        Vector3 movement = new Vector3(_movementInput.x, 0f, _movementInput.y);

        movement = transform.forward * movement.z + transform.right * movement.x;
        
        transform.Translate(movement * PlayerSpeed * Time.deltaTime, Space.World);
    }

    void RotatePlayer() {
        transform.Rotate(Vector3.up * _rotate  * RotationSpeed * Time.deltaTime);
    }
}
