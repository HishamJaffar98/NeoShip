using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip[] buttonSounds;
   
    public void OnHover()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(buttonSounds[0]);
    }

    public void OnClick()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(buttonSounds[1]);
    }
}
