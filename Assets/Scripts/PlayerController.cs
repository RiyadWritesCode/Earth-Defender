using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controllerSpeed = 30f;
    [SerializeField] float xRange = 17f;
    [SerializeField] float yRange = 11f;
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float controlPitchFactor = 10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = 20f;

    float yThrow, xThrow;

    void OnEnable ()
    {
        movement.Enable();
    }

    void OnDisable ()
    {
        movement.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation() 
    {
        float pitchDueToPosition = transform.localPosition.y * -positionPitchFactor;
        float pitchDueToControl = yThrow * -controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * -controlRollFactor;

        // transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, 1);
    }
    
    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * controllerSpeed * Time.deltaTime;
        float yOffset = yThrow * controllerSpeed * Time.deltaTime;

        float newXPos = xOffset + transform.localPosition.x;
        float newYPos = yOffset + transform.localPosition.y;

        float clampedXPos = Math.Clamp(newXPos, -xRange, xRange);
        float clampedYPos = Math.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

        // float xThrow = Input.GetAxis("Horizontal");
        // float yThrow = Input.GetAxis("Vertical");

        // Debug.Log(xThrow);
        // Debug.Log(yThrow);
    }
}
