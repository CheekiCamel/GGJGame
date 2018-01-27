using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class MainSceneAudioFades : MonoBehaviour
{

    public float audioVolume;
    public AudioSource audioToFade;
    public Image panel;
    public Color color;
    public Animator animator;

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
        Debug.Log(((-audioVolume * 4))+1);
        panel.color = color;

    }

    public void ToMainMenu()
    {
        EditorSceneManager.LoadScene("MainMenu");
    }

    public void InitiateEnd()
    {
        animator.SetTrigger("ENDGAME");
    }

}
