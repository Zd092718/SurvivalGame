using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private bool invertCamera;
    private float camCurXRot;
    private Vector2 mouseDelta;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);

        //Invert camera if option is selected
        if (invertCamera)
        {
            cameraContainer.localEulerAngles = new Vector3(camCurXRot, 0, 0);
        }
        else
        {
            cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        }

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();

    }
}
