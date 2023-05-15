using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;


    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private float groundDistance = 0.4f;
    private float crouchHeight = 0.5f;
    private float standingHeight = 3.8f;
    private float timeToCrouch = 0.25f;
    private float crouchSpeed = 6f;
    public Transform groundCheck;
    public GameObject arm;
    public GameObject arm2;
    public LayerMask groundMask;
    private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    private Vector3 StandingCenter = new Vector3(0, 0, 0);
    public Vector3 velocity;
    private bool isCrouching;
    private bool duringCrouchingAnimation;
    private bool canCrouch = true;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchingAnimation && controller.isGrounded;
    private bool isGrounded;
    private bool isMoving = false;
    private KeyCode crouchKey = KeyCode.LeftControl;

    private void HandleCrouch()
    {
        if(shouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    private IEnumerator CrouchStand()
    {
        duringCrouchingAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching ? StandingCenter : crouchingCenter;
        Vector3 currentCenter = controller.center;

        while(timeElapsed < timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching; // isCrouching = not isCrouching

        if(isCrouching == true)
        {
            speed = crouchSpeed;
        }

        else
        {
            speed = 12f;
        }

        duringCrouchingAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity* Time.deltaTime);


        if (Input.GetKeyDown("w"))
        {
            isMoving = true;
        }

        if (Input.GetKeyUp("w"))
        {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) & isMoving == true & isCrouching == false)
        {
            speed = 20f;
        }

        else if(isCrouching == false)
        {
            speed = 12f;
        }

        if(isMoving == true)
        {

           arm.transform.localRotation = Quaternion.Euler(-111.882f, 96.085f, -162.151f);

           arm2.transform.localRotation = Quaternion.Euler(-79.614f, 68.729f, -122.824f);
           arm2.transform.localPosition = new Vector3(0.119f, -0.479f, 0.3f);

            //arm.transform.rotation *= Quaternion.Euler(0, 90f, 0);
            //arm.transform.rotation *= Quaternion.Euler(0, 0, -162.151f);
            //arm.transform.Rotate(-111.882f, 90f, -162.151f);
        }

        else
        {
            arm.transform.localRotation = Quaternion.Euler(-83.664f, 0,0);

            arm2.transform.localRotation = Quaternion.Euler(-79.614f, 155.51f, -122.823f);
            arm2.transform.localPosition = new Vector3(-0.087f, -0.284f, 0.64f);
        }


        if(canCrouch)
        {
            HandleCrouch();
        }
    }
}
