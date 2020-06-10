using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    [Header("Character Movement")]
    [Tooltip("Character default movement speed")]
    [SerializeField]
    private float defaultSpeed = 6.0f;
    [SerializeField]
    [Tooltip("Character walking movement speed")]
    private float walkSpeed = 1.5f;
    [SerializeField]
    [Tooltip("Character jump force")]
    private float jumpSpeed = 8.0f;
    [SerializeField]
    [Tooltip("Gravity force on character. Real world gravity 9.81")]
    private float gravity = 20.0f;

    [Header("Character Attributes")]
    [Tooltip("Character default height")]
    [SerializeField]
    private float characterHeight = 2f;
    [Tooltip("Character crouching height")]
    [SerializeField]
    private float characterHeightCrouching = 1.5f;

    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float speed;
    private bool isWalking = false;
    private bool isCrouching = false;

    private float horizontalMovement;
    private float verticalMovement;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        speed = defaultSpeed;

        characterController.height = characterHeight;
    }

    private void Update()
    {
        GetInput();
        CheckCrouch();

        if (characterController.isGrounded)
            GroundedMovement();
        else
            NotGroundedMovement();


        characterController.Move(moveDirection * Time.deltaTime);

    }

    private void GetInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    private void CheckCrouch()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            characterController.height = characterHeight;
            isCrouching = false;
        }
    }

    private void GroundedMovement()
    {
        //Crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.height = characterHeightCrouching;
            isCrouching = true;
        }
        //Walk
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if(isWalking || isCrouching)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = defaultSpeed;
        }

        moveDirection = transform.right * horizontalMovement + transform.forward * verticalMovement;        

        if (moveDirection.magnitude > 1)
        {
            moveDirection = moveDirection.normalized;
        }
        moveDirection *= speed;

        if (Input.GetButton("Jump"))
        {
            moveDirection.y = jumpSpeed;
        }
    }

    private void NotGroundedMovement()
    {
        if(isWalking || isCrouching)
        {
            speed = walkSpeed;
        }
        else
        {
            speed = defaultSpeed;
        }

        moveDirection = (transform.right * horizontalMovement * speed) +
            new Vector3(0f, moveDirection.y - gravity * Time.deltaTime, 0f) + (transform.forward * verticalMovement * speed);

    }
}
