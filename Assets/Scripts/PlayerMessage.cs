﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMessage : MonoBehaviour
{
    public float speedIncrementer;
    public float maxSpeed;

    public SpriteRenderer greenBubble;
    public SpriteRenderer blueBubble;
    public TextMeshPro text;

    private Rigidbody rb;

    public bool toLeft;

    public Transform startOnL;
    public Transform startOnR;
    private Transform currentStartPosition;

    public bool canMove;

    public List<string> messages = new List<string>();
    public string currentMessage;
    public int messageCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        toLeft = false;
        currentStartPosition = startOnL;
        canMove = true;
        messageCounter = 0;
        currentMessage = messages[messageCounter];
        text.SetText(currentMessage, true);

    }

    void LateUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
        }

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

            if (canMove)
            {
                Debug.Log("input: " + GetInput());
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
                if (rb.velocity.magnitude > maxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * maxSpeed;
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
            if (toLeft)
            {
                greenBubble.gameObject.SetActive(true);
                blueBubble.gameObject.SetActive(false);
            }
            toLeft = false;
            rb.velocity = Vector3.zero;
            currentStartPosition = startOnL;
            
        }
        else if (other.tag == "Phone_R")
        {
            if (!toLeft)
            {
                greenBubble.gameObject.SetActive(false);
                blueBubble.gameObject.SetActive(true);
            }
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

    public void NextMessage()
    {
        if (messageCounter < messages.Count - 1)
        {
            Debug.Log("new mensgage pls");
            messageCounter++;
            currentMessage = messages[messageCounter];
            text.SetText(currentMessage, true);
        }
        else
        {
            Debug.Log("Game completed");
            //PUT END GAME CODE HERE
        }
    }

}
