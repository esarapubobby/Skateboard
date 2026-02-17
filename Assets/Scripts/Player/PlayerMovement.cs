using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float ExtraGravity = 20f;
    [SerializeField] float JumpForce = 10f;
    [SerializeField] float speed = 10f;

    [SerializeField] float turnSpeed = 80f;      
    [SerializeField] float maxTurnAngle = 90f;  
    [SerializeField]GameObject GameOver; 

    Rigidbody rb;
    Vector3 MoveDirection;

    float targetY = 0f;
    bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        MoveDirection = transform.forward;

        int level = PlayerPrefs.GetInt("Level", 0);

        if (level == 0)
        {
           speed = 10f; 
        }
            
        else if (level == 1)
        {
            speed = 12f;
        }
            
        else if (level == 2)
        {
            speed = 14f;
        }
            
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetY -= turnSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetY += turnSpeed * Time.deltaTime;
        }

      
        targetY = Mathf.Clamp(targetY, -maxTurnAngle, maxTurnAngle);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, targetY, 0f);

        MoveDirection = transform.forward;

        rb.AddForce(Vector3.down * ExtraGravity, ForceMode.Acceleration);

        Vector3 finalVelocity = MoveDirection.normalized * speed;
        finalVelocity.y = rb.velocity.y;

        rb.velocity = finalVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
             isGrounded = true;
        }
           
        if (collision.gameObject.tag == "Obstacle")
        {
            GameOver.SetActive(true);
            Time.timeScale=0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
        }
            
    }
}
