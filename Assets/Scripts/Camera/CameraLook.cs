using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sensetivity;

    public float rotationOnX;

    public float rotationOnY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Taking mouse input
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensetivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensetivity;

        //Rotate camera up and down
        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -89f, 89f);
        transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);

        //Rotate left and right
        //transform.Rotate(Vector3.up * mouseX);

        //my method
        rotationOnY += mouseX;

        transform.localEulerAngles = new Vector3(rotationOnX, rotationOnY, 0f);

        //locks and frees the mouse if game is puased
        if (PauseMenu.gameIsPaused)
        {
            FreeMouse();
        }
        else
        {
            LockMouse();
        }
    }

    public void LockMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FreeMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
