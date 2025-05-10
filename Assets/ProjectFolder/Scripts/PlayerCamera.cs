using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform orientation;
    public bool canMoveCamera = true;

    public float xSensitivity = 1f;
    public float ySensitivity = 1f;

    float xRotation;
    float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMoveCamera)
        {
            float MouseInputX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
            float MouseInputY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

            yRotation += MouseInputX;
            xRotation -= MouseInputY;
            xRotation = Mathf.Clamp(xRotation, -75f, 52f);
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}
