using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    public float speedIncrementer;
    public float maxSpeed;

    public Canvas canvas;
    public Text text;

    private Rigidbody rb;

    public bool toLeft;

    public Transform startOnL;
    public Transform startOnR;
    private Transform currentStartPosition;

    public bool canMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        text = canvas.GetComponentInChildren<Text>();
        toLeft = false;
        currentStartPosition = startOnL;
        canMove = true;
    }

    void LateUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (GetInput())
            {
                if (toLeft)
                {
                    rb.AddForce(-transform.right * speedIncrementer, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(transform.right * speedIncrementer, ForceMode.Impulse);
                }
            }
            else
            {
                if (rb.velocity.magnitude > speedIncrementer)
                {
                    rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude - speedIncrementer);
                }
                else
                {
                    if (GetInputDown())
                    {
                        canMove = true;
                    }
                    rb.velocity = Vector3.zero;
                }
            }
        }
        else
        {
            if (GetInputDown())
            {
                canMove = true;
            }
            rb.velocity = Vector3.zero;
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }

    public bool GetInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return true;
        }
        else if (Input.GetMouseButton(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetInputDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Phone Collisions
        if (other.tag == "Phone_L")
        {
            toLeft = false;
            rb.velocity = Vector3.zero;
            currentStartPosition = startOnL;
        }
        else if (other.tag == "Phone_R")
        {
            toLeft = true;
            rb.velocity = Vector3.zero;
            currentStartPosition = startOnR;
        }

        //Obstacle Collisions
        if (other.tag == "Obstacle")
        {
            transform.position = currentStartPosition.position;
            canMove = false;
        }

    }

}
