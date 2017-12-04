using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool invertY;

    float previousFire;
    //float previousGrab;

    public float xLookInput
    {
        get
        {
            return Input.GetAxis("LookX");
        }
    }
    public float yLookInput
    {
        get
        {
            return Input.GetAxis("LookY");
        }
    }
    public float xMoveInput
    {
        get
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float yMoveInput
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }
    public float jumpButtomDown
    {
        get
        {
            return Input.GetAxis("Jump");
        }
    }
    public float grabButtonDown
    {
        get
        {
            if(previousFire <= 0 && Input.GetAxis("Fire1") > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public bool glueButtonDown
    {
        get
        {
            return false;
            //return Input.GetMouseButtonDown(1);
        }
    }

    public bool unstickButtonDown
    {
        get
        {
            return false;
            //return Input.GetKeyDown(KeyCode.Alpha2);
        }
    }

    // Use this for initialization
    void Start ()
    {
        LockAndHideCursor();
    }

    void LockAndHideCursor()
    {
        //Locking and hiding the cursor by default
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        previousFire =  Input.GetAxis("Fire1");
    }
}
