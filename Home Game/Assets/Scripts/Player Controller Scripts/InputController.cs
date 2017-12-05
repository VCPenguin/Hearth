using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool invertY;

    float previousXButton;
    float previousYButton;
    float previousAButton;
    float previousBButton;
    float previousStartButton;
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
    public float xButtonDown
    {
        get
        {
            if(previousXButton <= 0 && Input.GetAxis("XButton") > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public float yButtonDown
    {
        get
        {
            if (previousYButton <= 0 && Input.GetAxis("YButton") > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public float aButtonDown
    {
        get
        {
            if (previousAButton <= 0 && Input.GetAxis("AButton") > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public float bButtonDown
    {
        get
        {
            if (previousBButton <= 0 && Input.GetAxis("BButton") > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public float startButtonDown
    {
        get
        {
            if (previousStartButton <= 0 && Input.GetAxis("StartButton") > 0)
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
        previousXButton =  Input.GetAxis("XButton");
        previousYButton = Input.GetAxis("YButton");
        previousAButton = Input.GetAxis("AButton");
        previousBButton = Input.GetAxis("BButton");
        previousStartButton = Input.GetAxis("StartButton");
    }
}
