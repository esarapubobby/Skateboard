using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float ExtraGravity=30f;
    [SerializeField] float JumpForce=8f;
    [SerializeField]float speed=10f;
    Rigidbody rb;
    Vector3 MoveDirection;
    bool isGrounded=true;
    int StepIndex=0;


    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.freezeRotation=true;
        MoveDirection=transform.forward;

    }

    
    void Update()
    {
        if (StepIndex==0 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StepIndex=1;
            MoveDirection=Quaternion.Euler(0,-90f,0)*MoveDirection;
        }
        if (StepIndex==1 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            StepIndex=0;
            MoveDirection=Quaternion.Euler(0,90f,0)*MoveDirection;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)&& isGrounded)
        {
            rb.AddForce(Vector3.up *JumpForce,ForceMode.Impulse);
            isGrounded=false;
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * ExtraGravity, ForceMode.Acceleration);
        Vector3 finalVelocity = MoveDirection.normalized * speed;
        finalVelocity.y = rb.velocity.y;
        rb.velocity = finalVelocity;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded=true;
        }
    }
}
