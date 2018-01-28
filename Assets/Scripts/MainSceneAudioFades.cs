using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneAudioFades : MonoBehaviour
{

    public float audioVolume;
    public AudioSource audioToFade;
    public Image panel;
    public Color color;
    public Animator animator;

    public PhoneMessage phoneRMessageBottom;
    public PhoneMessage phoneRMessageTop;
    public PhoneMessage phoneLMessageBottom;
    public PhoneMessage phoneLMessageTop;

    void Start()
    {
        audioToFade = GetComponent<AudioSource>();
        color = panel.color;
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        audioToFade.volume = audioVolume;

        color.a = ((-audioVolume * 4))+1;
        //Debug.Log(((-audioVolume * 4))+1);
        panel.color = color;


        Debug.Log(phoneRMessageTop);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void InitiateEnd()
    {
        //animator.SetTrigger("ENDGAME");
        ToMainMenu();
    }

    public void UpdatePhone(string lastText)
    {
        phoneLMessageBottom = GameObject.FindGameObjectWithTag("PhoneLBot").GetComponent<PhoneMessage>();
        phoneLMessageTop = GameObject.FindGameObjectWithTag("PhoneLTop").GetComponent<PhoneMessage>();
        phoneRMessageBottom = GameObject.FindGameObjectWithTag("PhoneRBot").GetComponent<PhoneMessage>();
        phoneRMessageTop = GameObject.FindGameObjectWithTag("PhoneRTop").GetComponent<PhoneMessage>();





        phoneRMessageTop.myMessage = phoneRMessageBottom.myMessage;
        phoneRMessageBottom.myMessage = lastText;
        if (phoneRMessageTop.isGreen)
        {
            phoneRMessageTop.isGreen = false;
            phoneRMessageBottom.isGreen = true;
        }
        else
        {
            phoneRMessageTop.isGreen = true;
            phoneRMessageBottom.isGreen = false;
        }

        phoneLMessageTop.myMessage = phoneLMessageBottom.myMessage;
        phoneLMessageBottom.myMessage = lastText;
        if (phoneLMessageTop.isGreen)
        {
            phoneLMessageTop.isGreen = false;
            phoneLMessageBottom.isGreen = true;
        }       
        else    
        {       
            phoneLMessageTop.isGreen = true;
            phoneLMessageBottom.isGreen = false;
        }
    }



}
