using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public float sensetivity;

    public float rotationOnX;

    public float rotationOnY;

    //new Method
    private PlayerControl controls;

    private Vector2 mouseLook;

    private float xRotation = 0f;

    private float yRotation = 0f;
    // Start is called before the first frame update
    private void Awake()
    {
        controls = new PlayerControl();
        
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseLook = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * sensetivity * Time.deltaTime;
        float mouseY = mouseLook.y * sensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89.9f, 89.9f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);

        ////Taking mouse input
        //float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensetivity;
        //float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensetivity;

        ////Rotate camera up and down
        //rotationOnX -= mouseY;
        //rotationOnX = Mathf.Clamp(rotationOnX, -89f, 89f);
        //transform.localEulerAngles = new Vector3(rotationOnX, 0, 0);

        ////Rotate left and right
        ////transform.Rotate(Vector3.up * mouseX);

        ////my method
        //rotationOnY += mouseX;

        //transform.localEulerAngles = new Vector3(rotationOnX, rotationOnY, 0f);

        //locks and frees the mouse if game is puased
        //if (PauseMenu.gameIsPaused)
        //{
        //    FreeMouse();
        //}
        //else
        //{
        //    LockMouse();
        //}
    }

    //public void LockMouse()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //public void FreeMouse()
    //{
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.Confined;
    //}

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
