using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("Input for the new manager system for movement")]
    [SerializeField] InputAction movement;
    [Tooltip("Input for the new manager system for fire")]
    [SerializeField] InputAction fire;

    [Header("Array for lasers")]
    [SerializeField] GameObject[] lasers;

    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 15f;
    [SerializeField] float yRange = 10f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float positionPitchFactor = -2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    float horizontalThrow;
    float verticalThrow;

    private void OnEnable()
    {
        fire.Enable();
        movement.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
        movement.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5f)
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }


    private void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emmisionModule = laser.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isActive;
        }
    }

    private void ProcessTranslation()
    {
        horizontalThrow = movement.ReadValue<Vector2>().x;
        verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float rawXpos = transform.localPosition.x + xOffset;
        float clampedXpos = Mathf.Clamp(rawXpos, -xRange, xRange);

        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;
        float rawYpos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(rawYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
