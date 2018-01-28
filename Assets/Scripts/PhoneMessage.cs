using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneMessage : MonoBehaviour
{
    public SpriteRenderer greenBubble;
    public SpriteRenderer blueBubble;
    public Canvas canvas;
    public Text text;

    public bool isGreen;

    public string myMessage;

    void Start()
    {
        text = canvas.GetComponentInChildren<Text>();
        text.text = myMessage;
    }

    void Update()
    {
        text.text = myMessage;

        if (isGreen)
        {
            greenBubble.gameObject.SetActive(true);
            blueBubble.gameObject.SetActive(false);
        }
        else
        {
            greenBubble.gameObject.SetActive(false);
            blueBubble.gameObject.SetActive(true);
        }
    }

}
