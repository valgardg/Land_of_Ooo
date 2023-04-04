using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] public GameObject orientationObject;
    [SerializeField] public float speed = 3.0f;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterMover script requires a CharacterController component on the same GameObject.");
            enabled = false;
            return;
        }

        if (orientationObject == null)
        {
            Debug.LogError("Orientation GameObject is not assigned.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Debug.Log($"horizontal input: {horizontalInput}");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        Vector3 orientationForward = Vector3.Scale(orientationObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 orientationRight = Vector3.Scale(orientationObject.transform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (orientationForward * inputDirection.z + orientationRight * inputDirection.x) * speed;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
