using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerMessage : MonoBehaviour
{
    public float speedIncrementer;
    public float currentSpeed;
    public float maxSpeed;

    public SpriteRenderer greenBubble;
    public SpriteRenderer blueBubble;
    public Canvas canvas;
    public Text text;

    private Rigidbody rb;

    public bool toLeft;

    public Transform startOnL;
    public Transform startOnR;
    private Transform currentStartPosition;

    public bool canMove;

    public List<string> messages = new List<string>();
    public string currentMessage;
    public int messageCounter;

    public bool isFading;

    public Animator animator;

    public AudioClip leftPhone;
    public AudioClip rightPhone;
    public AudioClip vibrate;
    public AudioSource audioSource;

    public GameObject fadeController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        toLeft = false;
        currentStartPosition = startOnL;
        canMove = true;
        messageCounter = 0;
        currentMessage = messages[messageCounter];
        text = canvas.GetComponentInChildren<Text>();
        text.text = currentMessage;
        currentSpeed = maxSpeed;
        animator = this.GetComponent<Animator>();
        isFading = false;
        audioSource = GetComponent<AudioSource>();


    }

    void LateUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
        }
        if (isFading)
        {
            rb.velocity = Vector3.zero;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            NextMessage();
        }
    }

    void FixedUpdate()
    {
        if (!isFading)
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
                if (rb.velocity.magnitude > currentSpeed)
                {
                    rb.velocity = rb.velocity.normalized * currentSpeed;
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
        else
        {
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
                animator.SetTrigger("FadeToGreen");
            }
            toLeft = false;
            rb.velocity = Vector3.zero;
            currentStartPosition = startOnL;

        }
        else if (other.tag == "Phone_R")
        {
            if (!toLeft)
            {
                animator.SetTrigger("FadeToBlue");
            }
            toLeft = true;
            rb.velocity = Vector3.zero;
            currentStartPosition = startOnR;

        }

        //Obstacle Collisions
        if (other.tag == "Obstacle")
        {
            //transform.position = currentStartPosition.position;
            //canMove = false;
            audioSource.clip = vibrate;
            audioSource.Play();
            if (toLeft)
            {
                animator.SetTrigger("FadeBackToBlue");
            }
            else
            {
                animator.SetTrigger("FadeBackToGreen");
            }
        }

    }

    public void NextMessage()
    {

        fadeController.GetComponent<MainSceneAudioFades>().UpdatePhone(currentMessage);
        if (messageCounter < messages.Count - 1)
        {
            Debug.Log("new mensgage pls");
            messageCounter++;
            currentMessage = messages[messageCounter];
            text.text = currentMessage;
            transform.position = currentStartPosition.position;
            if (toLeft)
            {
                greenBubble.gameObject.SetActive(false);
                blueBubble.gameObject.SetActive(true);
                audioSource.clip = leftPhone;
                audioSource.Play();
            }
            else
            {
                greenBubble.gameObject.SetActive(true);
                blueBubble.gameObject.SetActive(false);
                audioSource.clip = rightPhone;
                audioSource.Play();
            }
        }
        else
        {
            if (toLeft)
            {
                audioSource.clip = leftPhone;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = rightPhone;
                audioSource.Play();
            }

            Debug.Log("Game completed");
            fadeController.GetComponent<MainSceneAudioFades>().InitiateEnd();
            gameObject.SetActive(false);
        }
    }

    public void StartFade()
    {
        currentSpeed = 0;
        isFading = true;
    }

    public void EndFade()
    {
        currentSpeed = maxSpeed;
        isFading = false;
    }

    public void BackToStart()
    {
        transform.position = currentStartPosition.position;
    }

}
