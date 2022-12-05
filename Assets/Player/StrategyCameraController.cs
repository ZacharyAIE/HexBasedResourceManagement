using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// WRITTEN BY ZACHARY RIMMER
// With help from https://gamedev-resources.com/make-a-configurable-camera-with-the-new-unity-input-system/#Adding_rotation_behavior
// and https://www.youtube.com/watch?v=rnqF6S7PfFA

namespace Assets.Camera.Scripts
{

    public class StrategyCameraController : MonoBehaviour
    {
        public UnityEngine.Camera cam;

        [Header("Speed Settings")]
        [Range(0.01f, 10)] public float movementSpeed;
        [Range(0.01f, 20)] public float acceleratedMovementSpeed;
        [Range(0.01f, 10)] public float smoothRate;
        [Range(0.01f, 10)] public float keyboardRotationSpeed;
        [Range(0.01f, 10)] public float mouseRotationSpeed;
        public Vector3 zoomSpeed;

        // Conditions
        private bool moveIsHeld;
        private bool rotateIsHeld;
        private bool accelerateIsHeld; // Camera "Sprint"
        private bool mouseRotateHeld; // USED FOR MOUSE-BASED ROTATION OF THE CAMERA

        // Inputs
        private Vector3 moveInput;
        private Vector3 rotateInput;
        private Vector2 mouseDelta;

        // Position and Rotation Buffers
        private Vector3 targetPosition;
        private Vector3 targetZoom;
        private Quaternion targetRotation;

        private void Start()
        {
            targetPosition = transform.position;
            targetZoom = cam.transform.localPosition;
            targetRotation = transform.rotation;
        }

        ////// EVENTS TO BE TRIGGERED VIA PLAYER INPUT //////
        public void OnCamMove(InputAction.CallbackContext ctx)
        {
            HandleCamMove(ctx);
        }
        public void OnCamRotate(InputAction.CallbackContext ctx)
        {
            KeyboardCamRotation(ctx);
        }
        public void OnMouseRotate(InputAction.CallbackContext ctx)
        {
            HandleMouseRotationInput(ctx);
        }
        public void OnMouseLock(InputAction.CallbackContext ctx)
        {
            mouseRotateHeld = ctx.ReadValue<float>() == 1; // New interesting way of evaluation...?
        }
        public void OnScrollZoom(InputAction.CallbackContext ctx)
        {
            HandleScrollZoomInput(ctx);
        }

        public void OnAccelerate(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                accelerateIsHeld = true;
            }
            // EXECUTES ON BUTTON RELEASE
            if (ctx.canceled)
            {
                accelerateIsHeld = false;
            }
        }

        ////// UPDATE VALUES IN HERE //////
        private void HandleScrollZoomInput(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (ctx.ReadValue<Vector2>().y > 0 && targetZoom.y > 30)
                    targetZoom += zoomSpeed;
                if (ctx.ReadValue<Vector2>().y < 0 && targetZoom.y < 70)
                    targetZoom -= zoomSpeed;

                //Vector3.ClampMagnitude(targetZoom, 76);
                Mathf.Clamp(targetZoom.y, 30, 76);

            }
        }
        private void HandleCamMove(InputAction.CallbackContext ctx)
        {
            // EXECUTES ON BUTTON PRESS
            if (ctx.performed)
            {
                moveIsHeld = true;
                moveInput = ctx.ReadValue<Vector2>();
            }
            // EXECUTES ON BUTTON RELEASE
            if (ctx.canceled)
            {
                moveIsHeld = false;
                moveInput = Vector2.zero;
            }
        }

        private void HandleMouseRotationInput(InputAction.CallbackContext ctx)
        {
            mouseDelta = mouseRotateHeld ? ctx.ReadValue<Vector2>() : Vector2.zero;
        }

        private void KeyboardCamRotation(InputAction.CallbackContext ctx)
        {
            // EXECUTES ON BUTTON PRESS
            if (ctx.performed)
            {
                rotateIsHeld = true;
                rotateInput = ctx.ReadValue<Vector2>();

                // Left Input
                if (rotateInput.x < 0)
                {
                    keyboardRotationSpeed = -keyboardRotationSpeed;
                }

                // Right Input
                else if (rotateInput.x > 0)
                {
                    if (keyboardRotationSpeed <= 0)
                    {
                        keyboardRotationSpeed = -keyboardRotationSpeed;
                    }
                }
            }
            // EXECUTES ON BUTTON RELEASE
            if (ctx.canceled)
            {
                rotateIsHeld = false;

                if (keyboardRotationSpeed <= 0)
                {
                    keyboardRotationSpeed = -keyboardRotationSpeed;
                }
            }
        }


        private void Update()
        {
            // Update the buffers here.
            if (moveIsHeld)
                targetPosition += (transform.forward * moveInput.y + transform.right * moveInput.x) * Time.deltaTime * movementSpeed;

            if (moveIsHeld && accelerateIsHeld)
                targetPosition += (transform.forward * moveInput.y + transform.right * moveInput.x) * Time.deltaTime * acceleratedMovementSpeed;

            if (rotateIsHeld)
                targetRotation *= Quaternion.Euler(Vector3.up * keyboardRotationSpeed);

            if (mouseRotateHeld)
                targetRotation *= Quaternion.AngleAxis(-mouseDelta.x * Time.deltaTime * mouseRotationSpeed, Vector3.up);
        }

        // Apply the buffers and any post-processing.
        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothRate);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothRate);
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(targetZoom.x, Mathf.Clamp(targetZoom.y, 1, 70), targetZoom.z), Time.deltaTime * smoothRate);

        }
    }
}
