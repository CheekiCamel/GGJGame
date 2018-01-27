﻿using System.Collections;
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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        text = canvas.GetComponentInChildren<Text>();
        toLeft = false;
        currentStartPosition = startOnL;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
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
                rb.velocity = Vector3.zero;
            }
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
//        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
        }

    }

}
