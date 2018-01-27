using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleMessage : MonoBehaviour
{

    public List<string> messageStrings = new List<string>();
    public string myMessage;

    public float speed;

    private Rigidbody rb;

    public bool isTop;
    public GameObject spawnController;

    public Canvas canvas;
    public Text text;

    //lifespan is how long this object lives for. tracker helps keeps track of when this needs to happen
    public float lifespan;
    public float lifespanTracker;
    public float timeDisplay;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        string myMessage = messageStrings[Random.Range(0, messageStrings.Count)];
        text = canvas.GetComponentInChildren<Text>();
        text.text = myMessage;
    }

    void Update()
    {
        if (Time.time - lifespanTracker > lifespan)
        {
            Destroy(transform.gameObject);
        }
        timeDisplay = Time.time;
    }

    void FixedUpdate()
    {
        rb.AddForce(-transform.right * speed);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Center")
        {
            spawnController.GetComponent<Spawner>().SpawnNewMessage(isTop);
        }
    }


}
