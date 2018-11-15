using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //Normal movement + gravity
    private CharacterController movementController;
    private Vector3 moveDirectionVector;
    private readonly float runningMovementSpeed = 10.0f;
    private float movementSpeed;
    private float gravityAmount;


    //Mouse movement
    private GameObject cameraObject;
    private readonly float horizontalSensitivity = 2.0f;
    private readonly float verticalSensitivity = 2.0f;
    private float horizontalAngle;
    private float verticalAngle;


    //Jump
    private bool firstJumpDone = false;
    private bool secondJumpDone = false;
    private readonly float jumpHeight = 1.02f;

    //Jetpack
    private bool usingJetpack;
    private bool spacePressedFromSecondJump;
    private float jetpackChargeTime;
    private readonly float jetpackMaxCharge = 1.25f;
    private readonly float jetpackMaxForceAmount = 0.15f;
    private bool jetpackRanOut;

    void Start ()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //QualitySettings.SetQualityLevel(0, true); //FOR LAPTOP

        cameraObject = GameObject.FindGameObjectWithTag("PlayerCamera");
        movementController = gameObject.GetComponent<CharacterController>();
        jetpackChargeTime = jetpackMaxCharge;
        movementSpeed = runningMovementSpeed;
	}
	
	void Update ()
    {
        //Normal movement + gravity
        Vector2 playerInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (movementController.isGrounded == true && gravityAmount < 0)
        {
            gravityAmount = 0;
            if (firstJumpDone == true || secondJumpDone == true)
            {
                firstJumpDone = false;
                secondJumpDone = false;
            }
        }
        else if (movementController.isGrounded == false && usingJetpack == false)
        {
            gravityAmount = gravityAmount + Physics.gravity.y /5 * Time.deltaTime;
        }

        moveDirectionVector = gameObject.transform.right * playerInputVector.x + gameObject.transform.forward * playerInputVector.y;
        moveDirectionVector = moveDirectionVector + new Vector3(0, gravityAmount, 0);

        movementController.Move(moveDirectionVector * Time.deltaTime * movementSpeed);


        //Mouse movement
        horizontalAngle = horizontalAngle - horizontalSensitivity * Input.GetAxisRaw("Mouse Y");
        verticalAngle = verticalAngle + verticalSensitivity * Input.GetAxisRaw("Mouse X");

        gameObject.transform.eulerAngles = new Vector3(0, verticalAngle, 0); 
        cameraObject.transform.localRotation = Quaternion.Euler(horizontalAngle, 0, 0);


        //Jump
        if(Input.GetButtonDown("Jump"))
        {
            if(firstJumpDone==false)
            {
                firstJumpDone = true;
                gravityAmount = jumpHeight;
            }
            else if(secondJumpDone==false)
            {
                secondJumpDone = true;
                spacePressedFromSecondJump = true;
                gravityAmount = jumpHeight;
            }

        }


        //Jetpack
        if(Input.GetButton("Jump"))
        {
            if (firstJumpDone == true && secondJumpDone == true && spacePressedFromSecondJump == false)
            {
                if(jetpackChargeTime>0 && jetpackRanOut==false)
                {
                    usingJetpack = true;
                    if(gravityAmount<0)
                    {
                        gravityAmount = gravityAmount  + Time.deltaTime * 2;
                    }
                    else if (gravityAmount < jetpackMaxForceAmount)
                    {
                        gravityAmount = gravityAmount + Time.deltaTime;
                    }
                    else
                    {
                        gravityAmount = gravityAmount - Time.deltaTime;
                    }
                    
                    jetpackChargeTime = jetpackChargeTime - Time.deltaTime;
                    if (jetpackChargeTime <= 0)
                    {
                        usingJetpack = false;
                        jetpackRanOut = true;
                    }
                }
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (spacePressedFromSecondJump == true)
            {
                spacePressedFromSecondJump = false;
            }
            usingJetpack = false;
            jetpackRanOut = false;
        }
        if (jetpackChargeTime < jetpackMaxCharge && usingJetpack == false)
        {
            jetpackChargeTime = jetpackChargeTime + Time.deltaTime/2;
        }
    }
}
