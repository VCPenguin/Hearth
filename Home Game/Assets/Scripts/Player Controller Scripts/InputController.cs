﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool invertY;

    public float xLookInput
    {
        get
        {
            return Input.GetAxis("Mouse X");
        }
    }
    public float yLookInput
    {
        get
        {
            return Input.GetAxis("Mouse Y");
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
    public bool jumpButtomDown
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
    public bool grabButtonDown
    {
        get
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    public bool glueButtonDown
    {
        get
        {
            return Input.GetMouseButtonDown(1);
        }
    }

    public bool unstickButtonDown
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Alpha2);
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
}
