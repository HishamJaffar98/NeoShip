using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonRocket : MonoBehaviour
{
    [SerializeField] AudioClip flickeringSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayFlickeringSound()
    {
        GetComponent<AudioSource>().PlayOneShot(flickeringSound);
    }
}
