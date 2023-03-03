using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    public Transform cameraTarget;
    public PlayerController playerController;
    public CharacterController controller;
    private Animator animator;
    public Transform cam; 
    public float speed = 6f;

    private void Awake()
    {
        controller= GetComponent<CharacterController>();

    }


    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public void Update()
    {

        FollowPlayerCameraTarget();
        Rotation();

    }

    private void Rotation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
       


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y  ;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
            

        }
       
    }
    private void FollowPlayerCameraTarget()
    {
        transform.position = playerController.cameraTarget.position;
    }
}
