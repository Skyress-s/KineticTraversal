using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float mouseSensetivity;

    public float rotationOnX;

    public float rotationOnY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Taking mouse input
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensetivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensetivity;

        //Rotate camera up and down
        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
        transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);

        //Rotate left and right
        //transform.Rotate(Vector3.up * mouseX);

        //my method
        rotationOnY += mouseX;

        transform.localEulerAngles = new Vector3(rotationOnX, rotationOnY, 0f);


    }
}
