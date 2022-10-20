using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{


    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction shoot;
    [Tooltip("Speed at which the ship moves based on controls")] 
    [SerializeField] float controllerSpeed = 30f;
    [Tooltip("How far the ship can move on X-Axis")] 
    [SerializeField] float xRange = 17f;
    [Tooltip("How far the ship can move on Y-Axis")]
    [SerializeField] float yRange = 11f;

    [Header("Rotation based on position")]
    [Tooltip("How much the position on X-Axis affects the pitch")] 
    [SerializeField] float positionPitchFactor = 2f;
    [Tooltip("How much the position on Y-Axis affects the yaw")] 
    [SerializeField] float positionYawFactor = 2f;

    [Header("Rotation based on controls")]
    [Tooltip("How much the controls affect the pitch")] 
    [SerializeField] float controlPitchFactor = 10f;
    [Tooltip("How much the controls affect the roll")] 
    [SerializeField] float controlRollFactor = 20f;
    [SerializeField] GameObject[] lasers;

    float yThrow, xThrow;

    void OnEnable ()
    {
        movement.Enable();
        shoot.Enable();
    }

    void OnDisable ()
    {
        movement.Disable();
        shoot.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessShoot();
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

    void ProcessShoot() {
        if(shoot.ReadValue<float>() > 0.5)
        {
            SetLasers(true);
            
        }
        else {
            SetLasers(false);
        }
    }

    void SetLasers(bool isActive)
    {
        foreach(GameObject laser in lasers) 
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    } 
}

