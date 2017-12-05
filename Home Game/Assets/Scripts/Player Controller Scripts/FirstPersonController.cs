using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public GameObject HorizontalTurntable;
    public GameObject VerticalTurntable;
    public GameObject AimingReticle;

    public float VerticleLookTracker;

    public float minAngle;
    public float maxAngle;

    public float xCameraSensitivity;
    public float yCameraSensitivity;

    public float moveSpeed;
    public float jumpForce;

    public bool onGround;
    public float groundDistance;

    InputController inputController;

    public float StepDistance;
    public float StepTracker;

    Rigidbody rb;

    float PreviousX;
    float PreviousY;

    private void Awake()
    {
        //Finding input controller on object
        inputController = GetComponent <InputController>();

        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start ()
    {
        PreviousX = inputController.xMoveInput;
        PreviousY = inputController.yMoveInput;
    }
	
	// Update is called once per frame
	void Update ()
    {


        ////Horizontal Camera Rotation
        //HorizontalTurntable.transform.localRotation *= Quaternion.Euler(0, inputController.xLookInput * xCameraSensitivity * Time.deltaTime, 0);

        ////If the next step is gonna be over the cap, do not proceed. PS: i am god
        //if(VerticleLookTracker + (inputController.yLookInput * yCameraSensitivity * Time.deltaTime * (inputController.invertY ? -1 : 1)) < minAngle || VerticleLookTracker + (inputController.yLookInput * yCameraSensitivity * Time.deltaTime * (inputController.invertY ? -1 : 1)) > maxAngle)
        //{
        //    //Do nothing
        //}
        //else
        //{
        //    //Vertical Camera Rotation
        //    VerticalTurntable.transform.localRotation *= Quaternion.Euler(inputController.yLookInput * yCameraSensitivity * Time.deltaTime * (inputController.invertY ? -1 : 1), 0, 0);
        //    //Tracking verticle turntable looking
        //    VerticleLookTracker += inputController.yLookInput * yCameraSensitivity * Time.deltaTime * (inputController.invertY ? -1 : 1);
        //}

        ////Calculate Movement Vector
        //Vector3 movementVector = (HorizontalTurntable.transform.forward * inputController.yMoveInput) + (HorizontalTurntable.transform.right * inputController.xMoveInput);
        ////Normalizing the movement vector and apply to object
        //transform.position += movementVector.normalized * moveSpeed * Time.deltaTime;

        ////Character Jumping
        //if (inputController.jumpButtomDown && onGround)
        //    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        //Character Footsteps
        if (onGround && GetComponent<Rigidbody>().velocity.magnitude >= 1)
        {
            StepTracker += Time.deltaTime;

            if (StepTracker > StepDistance)
            {
                PlayerController.instance.GetComponent<AudioSource>().Play();
                StepTracker = 0;
                //play noise reset tracker;
            }
        }

        ////Ground checking
        //Ray downRay = new Ray(HorizontalTurntable.transform.position, HorizontalTurntable.gameObject.transform.up * -1);
        //RaycastHit hit = new RaycastHit();

        //if(Physics.Raycast(downRay, out hit, groundDistance))
        //{
        //    onGround = true;
        //}
        //else
        //{
        //    onGround = false;
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(PlayerController.instance.sceneController.Paused == false)
        {
            //Horizontal Camera Rotation
            HorizontalTurntable.transform.localRotation *= Quaternion.Euler(0, inputController.xLookInput * xCameraSensitivity, 0);

            //If the next step is gonna be over the cap, do not proceed. PS: i am god
            if (VerticleLookTracker + (inputController.yLookInput * yCameraSensitivity * (inputController.invertY ? -1 : 1)) < minAngle || VerticleLookTracker + (inputController.yLookInput * yCameraSensitivity * (inputController.invertY ? -1 : 1)) > maxAngle)
            {
                //Do nothing
            }
            else
            {
                //Vertical Camera Rotation
                VerticalTurntable.transform.localRotation *= Quaternion.Euler(inputController.yLookInput * yCameraSensitivity * (inputController.invertY ? -1 : 1), 0, 0);
                //Tracking verticle turntable looking
                VerticleLookTracker += inputController.yLookInput * yCameraSensitivity * (inputController.invertY ? -1 : 1);
            }

            //Calculate Movement Vector
            Vector3 movementVector = (HorizontalTurntable.transform.forward * inputController.yMoveInput) + (HorizontalTurntable.transform.right * inputController.xMoveInput);

            //Normalizing the movement vector and apply to object
            //transform.position += movementVector.normalized * moveSpeed;

            //Attemptingrigid body physics add
            //GetComponent<Rigidbody>().AddForce(movementVector.normalized * moveSpeed);



            rb.velocity = new Vector3(movementVector.normalized.x * moveSpeed, rb.velocity.y, movementVector.normalized.z * moveSpeed);

            //Character Jumping
            if (inputController.jumpButtomDown > 0 && onGround)
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            ////Character Footsteps
            //if (movementVector.magnitude > 0)
            //{
            //    StepTracker += Time.deltaTime;

            //    if (StepTracker > StepDistance)
            //    {
            //        PlayerController.instance.GetComponent<AudioSource>().Play();
            //        StepTracker = 0;
            //        //play noise reset tracker;
            //    }
            //}

            //Debug upkey checks

            //Forward stopping
            if (onGround)
            {
                if (inputController.yMoveInput <= 0 && PreviousY > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    Debug.Log("Stopped forward");
                }
                else if (inputController.yMoveInput >= 0 && PreviousY < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    Debug.Log("Stopped backward");
                }
                else if (inputController.xMoveInput <= 0 && PreviousX > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    Debug.Log("Stopped right");
                }
                else if (inputController.xMoveInput >= 0 && PreviousX < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    Debug.Log("Stopped left");
                }
            }



            //Ground checking
            Ray downRay = new Ray(HorizontalTurntable.transform.position, HorizontalTurntable.gameObject.transform.up * -1);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(downRay, out hit, groundDistance))
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }


            PreviousX = inputController.xMoveInput;
            PreviousY = inputController.yMoveInput;
        }

        
    }


}
